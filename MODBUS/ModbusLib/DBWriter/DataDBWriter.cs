using System;
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
		public string Header { get; set; }
		public double Min { get; set; }
		public double Max { get; set; }
		public double Avg { get; set; }
		public double AvgMin { get; set; }
		public double Eq { get; set; }
		public double Count { get; set; }
		public SortedList<DateTime, double> DiffVals { get; set; }
		public SortedList<DateTime, double> Vals { get; set; }
		public ModbusInitData Init;

		public DataDBRecord(string header, ModbusInitData init) {
			this.Header = header;
			this.Init = init;
			Min = 10e10;
			Max = -10e10;
			Avg = 0;
			Count = 0;
			DiffVals = new SortedList<DateTime, double>();
			Vals = new SortedList<DateTime, double>();
		}

		public void AddVal(DateTime date, double val) {
			if (!Vals.ContainsKey(date)) {
				Vals.Add(date, val);
			}
		}

		public void ProcessVals() {
			double avg=0;
			double avgMin=0;
			double cnt=0;
			double cntMin=0;
			if (Vals.Count > 0) {
				int currentMinute=Vals.First().Key.Minute;
				foreach (KeyValuePair<DateTime,double>de in Vals) {
					double val=de.Value;
					AvgMin += val;
					if (Min > val) {
						Min = val;
					}
					if (Max < val) {
						Max = val;
					}
					Eq = val;
					Count++;
					if (Init.WriteToDBDiff) {
						if (DiffVals.Count == 0 ||
							Math.Abs(DiffVals.Last().Value - val) >= Init.Diff) {
							DiffVals.Add(de.Key, val);
						}
					}

					int minute=de.Key.Minute;
					if (currentMinute != minute) {
						int diffMin=Math.Abs(currentMinute - minute);
						avg += (avgMin / cntMin) * diffMin;
						cnt += diffMin;

						
						cntMin = 0;
						avgMin = 0;
						currentMinute = minute;
					}
					avgMin += val;
					cntMin++;
				}
				if (cnt > 0) {
					this.Avg = avg / cnt;
				}
				if (Count > 0) {
					AvgMin = AvgMin / Count;
				}
			}

		}
	}

	public class DataDBWriter
	{
		public List<String> FileNames { get; protected set; }
		public string FileName { get; protected set; }
		public TextReader Reader { get; protected set; }
		public List<string> Headers { get; protected set; }
		public SortedList<string, DataDBRecord> Data { get; set; }
		public DateTime Date { get; set; }
		public ModbusInitDataArray InitArray { get; protected set; }

		public DataDBWriter(ModbusInitDataArray initArray) {
			InitArray = initArray;
			Headers = new List<string>();
			Data = new SortedList<string, DataDBRecord>();
		}

		public bool init(List<string> fileNames) {
			FileNames = fileNames;
			Data.Clear();
			bool ok=false;

			foreach (string fileName in FileNames) {
				ok = ok || File.Exists(fileName);
			}
			return ok;
		}

		public void ReadAll() {
			foreach (string fileName in FileNames) {
				if (!File.Exists(fileName))
					continue;
				FileName = fileName;
				try {
					Reader = new StreamReader(System.IO.File.OpenRead(FileName));
					readHeader();
					readData();
				} catch (Exception e) {
					Logger.Error("Ошибка при чтении данных");
					Logger.Error(e.Message);
				} finally {
					Reader.Close();
				}
			}

			foreach (DataDBRecord rec in Data.Values) {
				rec.ProcessVals();
			}
		}

		protected void readHeader() {
			Headers.Clear();
			string headerStr=Reader.ReadLine();
			string[] headersArr=headerStr.Split(';');
			bool isFirst=true;
			foreach (string header in headersArr) {
				if (!isFirst) {
					Headers.Add(header);
					if (InitArray.FullData.ContainsKey(header)) {
						ModbusInitData init=InitArray.FullData[header];
						if (init.WriteToDBDiff || init.WriteToDBHH || init.WriteToDBMin) {
							if (!Data.ContainsKey(header)) {
								Data.Add(header, new DataDBRecord(header, init));
							}
						}
					}
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
				lastDate = DateTime.Now;
				int index=0;
				foreach (string valStr in valsArr) {
					if (!isFirst) {
						double val=Double.NaN;
						try {
							val = Convert.ToDouble(valStr);
						} catch (Exception) {
							val = Convert.ToDouble(valStr.Replace(",", "."), Settings.NFIPoint);
						}
						try {
							if (!Double.IsNaN(val)) {
								string header=Headers[index];
								if (Data.ContainsKey(header)) {
									Data[header].AddVal(lastDate, val);
								}
							}
						} catch {
							Logger.Error("Ошибка при чтении строки файла " + FileName);
						}
						index++;
					} else {
						lastDate = DateTime.Parse(valStr);
					}
					isFirst = false;
				}
			}
		}

		public void writeData(RWModeEnum mode) {
			SqlConnection con=null;
			SqlDataReader reader=null;
			SortedList<string,List<string>> inserts=new SortedList<string, List<string>>();
			SortedList<string,List<string>> deletes=new SortedList<string, List<string>>();
			string insertIntoHeader="INSERT INTO Data (parnumber,object,item,value0,value1,valueMin,valueMax,valueEq,objtype,data_date,rcvstamp,season)";			
			string frmt="SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, '{9}', '{10}', {11}";
			string frmDel="(parnumber={0} and object={1} and objType={2} and item={3} and data_date='{4}')";
			string frmDelAll="(parnumber={0} and object={1} and objType={2} and item={3} and data_date>='{4}' and data_date<='{5}')";
			string df=Settings.single.DBDateFormat;
			foreach (DataDBRecord rec in Data.Values) {
				ModbusInitData init=InitArray.FullData[rec.Header];
				if (init.WriteToDBHH || init.WriteToDBMin || init.WriteToDBDiff) {
					if (init.WriteToDBMin && mode == RWModeEnum.min) {
						string insert=String.Format(frmt, init.ParNumberMin, init.Obj, init.Item, rec.AvgMin,0, rec.Min, rec.Max, rec.Eq, init.ObjType,
							Date.AddMinutes(1).ToString(df), DateTime.Now.ToString(df), DBSettings.getSeason(Date.AddMinutes(1)));
						string delete=String.Format(frmDel, init.ParNumberMin, init.Obj, init.ObjType, init.Item, Date.AddMinutes(1).ToString(df));
						//string delete=String.Format(frmDelAll, init.ParNumberMin, init.Obj, init.ObjType, init.Item, Date.ToString(df), Date.AddMinutes(1).ToString(df));
						if (!inserts.ContainsKey(init.DBNameMin)) {
							inserts.Add(init.DBNameMin, new List<string>());
						}
						if (!deletes.ContainsKey(init.DBNameMin)) {
							deletes.Add(init.DBNameMin, new List<string>());
						}

						inserts[init.DBNameMin].Add(insert);
						deletes[init.DBNameMin].Add(delete);
					}

					if (init.WriteToDBHH && mode == RWModeEnum.hh) {
						string insert=String.Format(frmt, init.ParNumberHH, init.Obj, init.Item, rec.Avg,0, rec.Min, rec.Max, rec.Eq, init.ObjType,
							Date.AddMinutes(30).ToString(df), DateTime.Now.ToString(df), DBSettings.getSeason(Date.AddMinutes(30)));
						string delete=String.Format(frmDel, init.ParNumberHH, init.Obj, init.ObjType, init.Item, Date.AddMinutes(30).ToString(df));
						//string delete=String.Format(frmDelAll, init.ParNumberHH, init.Obj, init.ObjType, init.Item, Date.ToString(df), Date.AddMinutes(30).ToString(df));
						if (!inserts.ContainsKey(init.DBNameHH)) {
							inserts.Add(init.DBNameHH, new List<string>());
						}
						if (!deletes.ContainsKey(init.DBNameHH)) {
							deletes.Add(init.DBNameHH, new List<string>());
						}

						inserts[init.DBNameHH].Add(insert);
						deletes[init.DBNameHH].Add(delete);
					}

					
					if (init.WriteToDBDiff && mode == RWModeEnum.hh) {						
						double lastVal=Double.NaN;
						DateTime lastDate=DateTime.Now;
						try {
							string select=String.Format(
								"SELECT TOP 1 data_date,VALUE0 FROM DATA WHERE ParNumber={0} and Object={1} and ObjType={2} and Item={3} and Data_date<'{4}' order by DATA_DATE desc",
								init.ParNumberDiff, init.Obj, init.ObjType, init.Item, rec.DiffVals.First().Key.ToString(df));
							con = PiramidaAccess.getConnection(init.DBNameDiff);
							con.Open();
							SqlCommand command=null;
							command = con.CreateCommand();
							command.CommandText = select;
							reader=command.ExecuteReader();
							if (reader.Read()) {
								lastDate = Convert.ToDateTime(reader[0]);
								lastVal = Convert.ToInt32(reader[1]);
							}
							//lastVal = (double)command.ExecuteScalar();							
						} catch {
						} finally {
							try { reader.Close(); } catch { }
							try { con.Close(); } catch { }
						}
						if (!Double.IsNaN(lastVal)) {
							if (Math.Abs(lastVal - rec.DiffVals.First().Value) < init.Diff) {
								rec.DiffVals.RemoveAt(0);
							}
						}
						
						double timeChange;
						DateTime prevDate=Double.IsNaN(lastVal) ? (rec.DiffVals.Count > 0 ? rec.DiffVals.First().Key : DateTime.Now) : lastDate;
						
						string delete=String.Format(frmDelAll, init.ParNumberDiff, init.Obj, init.ObjType, init.Item,
							Date.ToString(df), Date.AddMinutes(30).ToString(df));
						if (!deletes.ContainsKey(init.DBNameDiff)) {
							deletes.Add(init.DBNameDiff, new List<string>());
						}						
						deletes[init.DBNameDiff].Add(delete);
						
						foreach (KeyValuePair<DateTime,double>diff in rec.DiffVals) {
							timeChange = (diff.Key.Ticks - prevDate.Ticks) / (10000000.0*60.0);
							string insert=String.Format(frmt, init.ParNumberDiff, init.Obj, init.Item, diff.Value, timeChange, diff.Value, diff.Value, diff.Value, init.ObjType,
								diff.Key.ToString(df), DateTime.Now.ToString(df), DBSettings.getSeason(diff.Key));

							if (!inserts.ContainsKey(init.DBNameDiff)) {
								inserts.Add(init.DBNameDiff, new List<string>());								
							}
							inserts[init.DBNameDiff].Add(insert);
							prevDate = diff.Key;
							
						}
					}
				}
			}

			SortedList<string,SqlConnection> connections=new SortedList<string, SqlConnection>();
			SortedList<string,SqlTransaction> transactions=new SortedList<string, SqlTransaction>();
			foreach (KeyValuePair<string,List<string>> de in deletes) {
				SqlConnection conect = PiramidaAccess.getConnection(de.Key);
				conect.Open();
				SqlTransaction transact=conect.BeginTransaction("Start_" + de.Key);				
				connections.Add(de.Key, conect);
				transactions.Add(de.Key, transact);
			}

			foreach (KeyValuePair<string,List<string>> de in deletes) {
				con = connections[de.Key];
				List<string> qDels=new List<string>();
				for (int i=0; i < de.Value.Count; i++) {
					qDels.Add(de.Value[i]);
					if ((i + 1) % 20 == 0 || i == de.Value.Count - 1) {
						string deletesSQL=String.Join(" OR ", qDels);
						string deleteSQL=String.Format("{0}\n{1}", "DELETE from DATA where", deletesSQL);
						try {
							SqlCommand command=null;
							command = con.CreateCommand();
							command.CommandText = deleteSQL;
							command.Transaction = transactions[de.Key];
							command.ExecuteNonQuery();
							
						} catch (Exception e) {
							Logger.Error("Ошибка в запросе " + e);
							Logger.Info(deleteSQL);
						} finally {
							//try { con.Close(); } catch { }
						}
						qDels = new List<string>();
					}
				}
			}

			foreach (KeyValuePair<string,List<string>> de in inserts) {
				con = connections[de.Key];
				List<string> qInserts=new List<string>();
				for (int i=0; i < de.Value.Count; i++) {
					qInserts.Add(de.Value[i]);
					if ((i + 1) % 20 == 0 || i == de.Value.Count - 1) {
						string insertsSQL=String.Join("\nUNION ALL\n", qInserts);
						string insertSQL=String.Format("{0}\n{1}", insertIntoHeader, insertsSQL);
						try {
							//con.Open();
							SqlCommand command=null;
							command = con.CreateCommand();
							command.CommandText = insertSQL;
							command.Transaction = transactions[de.Key];
							command.ExecuteNonQuery();
						} catch (Exception e) {
							Logger.Error("Ошибка в запросе " + e);
							Logger.Info(insertSQL);
						} finally {
							//try { con.Close(); } catch { }
						}
						qInserts = new List<string>();
					}
				}
			}

			foreach (KeyValuePair<string,SqlConnection> de in connections) {
				try {
					transactions[de.Key].Commit();					
				} catch { } finally {
					try { de.Value.Close(); } catch { }
				}
			}
		}
	}
}
