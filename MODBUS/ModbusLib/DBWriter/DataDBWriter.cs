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
		private int header;
		public int Header {
			get { return header; }
			set { header = value; }
		}

		private double min;
		public double Min {
			get { return min; }
			set { min = value; }
		}

		private double max;
		public double Max {
			get { return max; }
			set { max = value; }
		}

		private double avg;
		public double Avg {
			get { return avg; }
			set { avg = value; }
		}

		private double eq;
		public double Eq {
			get { return eq; }
			set { eq = value; }
		}


		private double count;
		public double Count {
			get { return count; }
			set { count = value; }
		}

		public DataDBRecord(int header) {
			this.Header = header;
			Min = 10e10;
			Max = -10e10;
			Avg = 0;
			Count = 0;
		}
	}

	public class DataDBWriter
	{
		private string fileName;
		public string FileName {
			get { return fileName; }
			set { fileName = value; }
		}

		private TextReader reader;
		public TextReader Reader {
			get { return reader; }
			set { reader = value; }
		}

		private List<int> headers;
		public List<int> Headers {
			get { return headers; }
			set { headers = value; }
		}

		private SortedList<int,DataDBRecord> data;
		public SortedList<int, DataDBRecord> Data {
			get { return data; }
			set { data = value; }
		}

		private List<DateTime> dates;
		public List<DateTime> Dates {
			get { return dates; }
			set { dates = value; }
		}

		private DateTime date;
		public DateTime Date {
			get { return date; }
			set { date = value; }
		}

		private ModbusInitDataArray initArray;
		public ModbusInitDataArray InitArray {
			get { return initArray; }
			set { initArray = value; }
		}


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
				reader = new StreamReader(fileName);
				readHeader();
				readData();
				foreach (DataDBRecord rec in Data.Values) {
					if (rec.Count > 0) {
						rec.Avg = rec.Avg / rec.Count;
					}
				}
			} catch {
				Logger.Error("Ошибка при чтении данных");
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
			while ((valsStr = Reader.ReadLine()) != null) {
				string[]valsArr=valsStr.Split(';');
				bool isFirst=true;
				int index=0;
				foreach (string valStr in valsArr) {
					if (!isFirst) {
						double val=0;
						try {
							val = Convert.ToDouble(valStr);
						} catch (Exception e) {
							val = Convert.ToDouble(valStr.Replace(",", "."), Settings.NFIPoint);
						}
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
						}
						index++;
					} else {
						Dates.Add(DateTime.Parse(valStr));
					}
					isFirst = false;
				}
			}

		}

		public void writeData(RWModeEnum mode) {
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
				}
			}
			SqlConnection con;

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
