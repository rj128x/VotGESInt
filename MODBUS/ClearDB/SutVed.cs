using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using VotGES.Piramida;
using VotGES;

namespace ClearDB
{
	public class SutVedRecord
	{
		public DateTime Date { get; set; }
		public SortedList<int, double> Data { get; set; }
	}
	public class SutVed
	{
		public string[] monthNames={"янв","фев","мар","апр","май","июн","июл","авг","сент","окт","ноя","дек"};
		public string FileName { get; set; }
		public int Year { get; set; }
		public XmlDocument Document { get; set; }
		public List<SutVedRecord> Data { get; set; }

		public SutVed(String fileName) {
			this.FileName = fileName;
		}

		public void ReadData() {
			this.Year = getFileNameYear(FileName);
			Data = new List<SutVedRecord>();
			File.Copy(FileName, "temp", true);
			TextReader Reader = new StreamReader(File.Open("temp", FileMode.Open, FileAccess.Read, FileShare.Read));

			//TextReader Reader = new StreamReader(File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.Read));
			string content=Reader.ReadToEnd();
			Reader.Close();
			File.Delete("temp");
			content=content.Replace("ss:","");
			content=content.Replace("x:","");
			content=content.Replace("xmlns=","xxx=");
			content=content.Replace("xmlns:","xxx_");
			content=content.Replace("html:","");
			int start=content.IndexOf("<Worksheet ");
			int end=content.LastIndexOf("</Worksheet>")+12;
			content=content.Substring(start,end-start);
			content="<root>"+content+"</root>";
			Document=new XmlDocument();
			Document.LoadXml(content);			
		}

		public static void ProcessFolder(string path,string pathTo) {
			DirectoryInfo dir=new DirectoryInfo(path);
			FileInfo[] files=dir.GetFiles();
			foreach (FileInfo file in files) {
				Logger.Info(file.FullName);
				bool ok=ProcessFile(file.FullName);
				if (ok) {
					Logger.Info("===ok");
					file.CopyTo(pathTo + "/" + file.Name, true);
					file.Delete();
				} else {
					Logger.Info("===error");
				}
			}
		}

		public static bool ProcessFile(string fileName) {
			SutVed sutVed=new SutVed(fileName);
			bool ok=false;
			try {
				sutVed.ReadData();
				sutVed.ProcessWorksheets();
				ok = sutVed.WriteData();
			} catch (Exception e) {
				Logger.Error("Ошибка при записи суточной ведомости ");
				Logger.Error(e.ToString());
			}
			return ok;			
		}

		protected int getFileNameYear(string fileName) {
			try {
				fileName = fileName.Substring(fileName.IndexOf("Сут"));
				Logger.Info(fileName);
				int res=-1;
				FileInfo info=new FileInfo(FileName);
				for (int year=1961; year <= 2050; year++) {
					if (info.Name.Contains(year.ToString())) {
						res = year;
					}
				}
				return res;
			} catch {
				return -1;
			}
		}

		protected int getMonthIndex(string sheetName) {
			try {
				int month=1;
				foreach (string monthName in monthNames) {
					if (sheetName.ToLower().Contains(monthName.ToLower())) {
						return month;
					}
					month++;
				}
				return -1;
			} catch {
				return -1;
			}
		}

		protected int getMonthYear(string sheetName) {
			try {
				int month=getMonthIndex(sheetName);
				string monthName=monthNames[month - 1];
				sheetName = sheetName.Replace(monthName, "");
				int year=-1;
				bool ok=Int32.TryParse(sheetName, out year);				
				if (ok){
					year = year < 61 ? 2000 + year : 1900 + year;
				}
				Logger.Info("==Year=" + Year);
				return ok ? year : -1;
			} catch {
				return -1;
			}
		}

		protected void ProcessWorksheets() {
			XmlNodeList nodes=Document.SelectNodes("root/Worksheet");
			foreach (XmlNode node in nodes) {
				ProcessWS(node);
			}
		}

		protected void ProcessWS(XmlNode WS) {
			string sheetName=WS.Attributes["Name"].Value;
			int sheetYear=getMonthYear(sheetName);
			int sheetMonth=getMonthIndex(sheetName);
			Logger.Info("==Month=" + sheetMonth);
			if (this.Year != sheetYear) {
				throw new Exception("Не совпадает год ведомости и листа");
			}
			XmlNodeList nodes=WS.SelectNodes("Table/Row");
			int index=0;
			foreach (XmlNode node in nodes) {
				SutVedRecord record=ProcessRow(node,index,sheetMonth);
				if (record != null) {
					Data.Add(record);
				}
				index++;
			}
		}

		protected double getVal(XmlNodeList cells, int index) {
			string val=cells.Item(index).InnerText;
			val = string.IsNullOrEmpty(val) ? "0" : val;
			val=val.Trim();
			
			double res=0;
			try {
				res = Double.Parse(val.Replace(',','.'));
			} catch {				
			}
			return res;
		}

		protected SutVedRecord ProcessRow(XmlNode row, int index, int month) {
			XmlNodeList cells=row.SelectNodes("Cell/Data");
			//Logger.Info(cells.Count.ToString());
			double vb=getVal(cells, 1);
			double day=getVal(cells, 0);
			if (vb < 80 || vb > 100 || day < 1 || day > 31 || index > 45) {
				return null;
			}

			try {
				SutVedRecord record=new SutVedRecord();
				DateTime date=new DateTime(Year, month, (int)day);
				record.Date = date;
				record.Data = new SortedList<int, double>();
				Logger.Info("=="+date.ToString());
				int code=1;
				cells = row.SelectNodes("Cell");
				foreach (XmlNode cell in cells) {
					int merge=0;
					try {
						merge = Int32.Parse(cell.Attributes["MergeAcross"].InnerText);
					} catch { }

					if (code != 1 && code<=27) {						
						XmlNodeList DataList=cell.SelectNodes("Data");
						double val=0;
						if (DataList.Count > 0) {
							val = getVal(DataList, 0);
						}
						record.Data.Add(code, val);
						Logger.Info("=====item " + code + "=" + val.ToString());
					}
					code += merge;
					code++;
				}
				return record;
			} catch (Exception e) {
				Logger.Info(e.ToString());
				return null; 
			}
		}

		protected bool WriteData() {
			List<string> insertsStrings=new List<string>();
			List<string> dates=new List<string>();
			if (Data.Count == 0) {
				return false;
			}
			foreach (SutVedRecord record in Data) {
				foreach (KeyValuePair<int,double> de in record.Data) {
					insertsStrings.Add(String.Format(DBClass.InsertInfoFormat, 26, 7, de.Key, de.Value, 2,
						record.Date.AddDays(1).ToString(DBClass.DateFormat), DateTime.Now.ToString(DBClass.DateFormat), DBSettings.getSeason(record.Date.AddDays(1))));
				}
				dates.Add(String.Format("'{0}'", record.Date.AddDays(1).ToString(DBClass.DateFormat)));
			}

			string delStr=String.Format("DELETE FROM DATA WHERE OBJECT=7 AND OBJTYPE=2 AND PARNUMBER=26 and DATA_DATE IN ({0})", String.Join(",", dates));

			SqlConnection con = PiramidaAccess.getConnection("P3000");
			con.Open();
			SqlTransaction transact=con.BeginTransaction();
			if (dates.Count > 0) {
				DBClass.Run(delStr, transact);
				DBClass.AddData(insertsStrings, DBClass.InsertIntoHeader, transact);

				try {
					transact.Commit();
				} catch (Exception e) {
					Logger.Info(e.ToString());
					return false;
				} finally {
					try { transact.Connection.Close(); } catch { }					
				}
			}
			return true;
		}

		
	}
}
