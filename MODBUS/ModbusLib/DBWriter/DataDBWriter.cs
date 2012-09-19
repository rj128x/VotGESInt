﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using VotGES.Piramida;
using System.Data.SqlClient;
using VotGES;

namespace ModbusLib
{
	public class DataDBRecord
	{
		public int Header{get; set;}
		public double Min{get; set;} 
		public double Max{get; set;} 
		public double Avg {get; set;}
		public double Eq {get; set;}
		public double Count { get;  set; }
		public SortedList<DateTime, double> DiffVals { get; set; }

		public DataDBRecord(int header) {
			this.Header = header;
			Min = 10e10;
			Max = -10e10;
			Avg = 0;
			Count = 0;
			DiffVals = new SortedList<DateTime, double>();
		}
	}

	public class DataDBWriter
	{		
		public string FileName {get;protected set;}
		public TextReader Reader {get;protected set;}
		public List<int> Headers {get;protected set;}
		public SortedList<int, DataDBRecord> Data {get; set;}
		public List<DateTime> Dates {get;protected set;}
		public DateTime Date {get; set;}
		public ModbusInitDataArray InitArray {get;protected set;}
		
		public DataDBWriter(ModbusInitDataArray initArray) {
			InitArray = initArray;
			Headers = new List<int>();
			Dates = new List<DateTime>();
			Data = new SortedList<int, DataDBRecord>();
		}

		public bool init(string fileName) {
			FileName = fileName;
			Headers.Clear();
			Dates.Clear();
			Data.Clear();

			return File.Exists(fileName);
		}

		public void ReadAll() {
			try {
				Reader = new StreamReader(System.IO.File.OpenRead(FileName));
				readHeader();
				readData();
				foreach (DataDBRecord rec in Data.Values) {
					if (rec.Count > 0) {
						rec.Avg = rec.Avg / rec.Count;
					}
				}
			} catch (Exception e) {
				Logger.Error("Ошибка при чтении данных");
				Logger.Error(e.ToString());
			} finally {
				Reader.Close();
			}
		}

		protected void readHeader() {
			string headerStr=Reader.ReadLine();
			string[] headersArr=headerStr.Split(';');
			bool isFirst=true;
			foreach (string header in headersArr) {
				if (!isFirst) {
					int val=Convert.ToInt32(header);
					Headers.Add(val);
					Data.Add(val, new DataDBRecord(val));
				} else {
					Date = DateTime.Parse(header);
				}
				isFirst = false;
			}
		}

		protected void readData() {
			string valsStr;
			DateTime lastDate;
			while ((valsStr = Reader.ReadLine()) != null) {
				string[]valsArr=valsStr.Split(';');
				bool isFirst=true;
				lastDate=DateTime.Now;
				int index=0;
				foreach (string valStr in valsArr) {
					if (!isFirst) {
						double val=0;
						try {
							val = Convert.ToDouble(valStr);
						} catch (Exception) {
							val = Convert.ToDouble(valStr.Replace(",", "."), Settings.NFIPoint);
						}
						try {
							if (!Double.IsNaN(val)) {
								int header=Headers[index];
								Data[header].Avg += val;
								if (Data[header].Min > val) {
									Data[header].Min = val;
								}
								if (Data[header].Max < val) {
									Data[header].Max = val;
								}
								Data[header].Eq = val;
								Data[header].Count++;
								if (InitArray.FullData[header].WriteToDBDiff) {
									if (Data[header].DiffVals.Count == 0 || 
										Math.Abs(Data[header].DiffVals.Last().Value- val)>InitArray.FullData[header].Diff) {
											Data[header].DiffVals.Add(lastDate, val);
									}
								}
							}
						}catch {
							Logger.Error("Ошибка при чтении строки файла "+FileName);
						}
						index++;
					} else {
						lastDate = DateTime.Parse(valStr);
						Dates.Add(lastDate);
					}
					isFirst = false;
				}
			}
		}

		public void writeData(RWModeEnum mode) {
			SqlConnection con=null;
			SortedList<string,List<string>> inserts=new SortedList<string, List<string>>();
			SortedList<string,List<string>> deletes=new SortedList<string, List<string>>();
			string insertIntoHeader="INSERT INTO Data (parnumber,object,item,value0,valueMin,valueMax,valueEq,objtype,data_date,rcvstamp,season)";
			string frmt="SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, '{8}', '{9}', {10}";
			string frmDel="(parnumber={0} and object={1} and objType={2} and item={3} and data_date='{4}')";
			string df=Settings.single.DBDateFormat;
			foreach (DataDBRecord rec in Data.Values) {
				ModbusInitData init=InitArray.FullData[rec.Header];
				if (init.WriteToDBHH || init.WriteToDBMin) {
					if (init.WriteToDBMin && mode == RWModeEnum.min) {
						string insert=String.Format(frmt, init.ParNumberMin, init.Obj, init.Item, rec.Avg, rec.Min, rec.Max, rec.Eq, init.ObjType,
							Date.AddMinutes(1).ToString(df), DateTime.Now.ToString(df), 0);
						string delete=String.Format(frmDel, init.ParNumberMin, init.Obj, init.ObjType, init.Item, Date.AddMinutes(1).ToString(df));
						if (!inserts.ContainsKey(init.DBNameMin)) {
							inserts.Add(init.DBNameMin, new List<string>());
							deletes.Add(init.DBNameMin, new List<string>());
						}

						inserts[init.DBNameMin].Add(insert);
						deletes[init.DBNameMin].Add(delete);
					}

					if (init.WriteToDBHH && mode == RWModeEnum.hh) {
						string insert=String.Format(frmt, init.ParNumberHH, init.Obj, init.Item, rec.Avg, rec.Min, rec.Max, rec.Eq, init.ObjType,
							Date.AddMinutes(30).ToString(df), DateTime.Now.ToString(df), 0);
						string delete=String.Format(frmDel, init.ParNumberHH, init.Obj, init.ObjType, init.Item, Date.AddMinutes(30).ToString(df));
						if (!inserts.ContainsKey(init.DBNameHH)) {
							inserts.Add(init.DBNameHH, new List<string>());
							deletes.Add(init.DBNameHH, new List<string>());
						}
						inserts[init.DBNameHH].Add(insert);
						deletes[init.DBNameHH].Add(delete);
					}

					if (init.WriteToDBDiff && mode == RWModeEnum.hh) {
						double lastVal=Double.NaN;
						try {
							string select=String.Format(
								"SELECT TOP 1 VALUE0 FROM DATA WHERE ParNumber={0} and Object={1} and ObjType={2} and Item={3} and Data_date<'{4}' order by DATA_DATE desc", 
								init.ParNumberDiff, init.Obj, init.ObjType, init.Item,rec.DiffVals.First().Key.ToString(df));
							con = PiramidaAccess.getConnection(init.DBNameDiff);
							con.Open();
							SqlCommand command=null;
							command = con.CreateCommand();
							command.CommandText = select;
							lastVal=(double)command.ExecuteScalar();
						}catch{
						} finally {
							try { con.Close(); } catch { }
						}						
						if (!Double.IsNaN(lastVal)) {							
							if (Math.Abs(lastVal - rec.DiffVals.First().Value) < init.Diff) {
								rec.DiffVals.RemoveAt(0);
							}
						}
						foreach (KeyValuePair<DateTime,double>diff in rec.DiffVals) {
							string insert=String.Format(frmt, init.ParNumberDiff, init.Obj, init.Item, diff.Value, diff.Value, diff.Value, diff.Value, init.ObjType,
								diff.Key.ToString(df), DateTime.Now.ToString(df), 0);
							string delete=String.Format(frmDel, init.ParNumberDiff, init.Obj, init.ObjType, init.Item, diff.Key.ToString(df));
							if (!inserts.ContainsKey(init.DBNameDiff)) {
								inserts.Add(init.DBNameDiff, new List<string>());
								deletes.Add(init.DBNameDiff, new List<string>());
							}
							inserts[init.DBNameDiff].Add(insert);
							deletes[init.DBNameDiff].Add(delete);
						}
					}
				}
			}
			

			foreach (KeyValuePair<string,List<string>> de in deletes) {
				con = PiramidaAccess.getConnection(de.Key);
				List<string> qDels=new List<string>();
				for (int i=0; i < de.Value.Count; i++) {
					qDels.Add(de.Value[i]);
					if ((i + 1) % 20 == 0 || i == de.Value.Count - 1) {
						string deletesSQL=String.Join(" OR ", qDels);
						string deleteSQL=String.Format("{0}\n{1}", "DELETE from DATA where", deletesSQL);
						try {
							con.Open();
							SqlCommand command=null;
							command = con.CreateCommand();
							command.CommandText = deleteSQL;

							command.ExecuteNonQuery();
						} catch (Exception e) {
							Logger.Error("Ошибка в запросе " + e);
							Logger.Info(deleteSQL);
						} finally {
							try { con.Close(); } catch { }
						}
						qDels = new List<string>();
					}
				}

			}

			foreach (KeyValuePair<string,List<string>> de in inserts) {
				con = PiramidaAccess.getConnection(de.Key);
				List<string> qInserts=new List<string>();
				for (int i=0; i < de.Value.Count; i++) {
					qInserts.Add(de.Value[i]);
					if ((i + 1) % 20 == 0 || i == de.Value.Count - 1) {
						string insertsSQL=String.Join("\nUNION ALL\n", qInserts);
						string insertSQL=String.Format("{0}\n{1}", insertIntoHeader, insertsSQL);
						try {
							con.Open();
							SqlCommand command=null;
							command = con.CreateCommand();
							command.CommandText = insertSQL;
							command.ExecuteNonQuery();
						} catch (Exception e) {
							Logger.Error("Ошибка в запросе " + e);
							Logger.Info(insertSQL);
						} finally {
							try { con.Close(); } catch { }
						}
						qInserts = new List<string>();
					}
				}


			}
		}
	}
}
