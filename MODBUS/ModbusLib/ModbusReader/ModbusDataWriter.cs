using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using VotGES;

namespace ModbusLib
{
	public class ModbusDataWriter
	{
		public DateTime CurrentDate { get; protected set; }
		public TextWriter CurrentWriter { get; protected set; }
		public ModbusInitDataArray InitArray { get; protected set; }
		public List<string> Headers { get; protected set; }
		public string HeaderStr { get; protected set; }
		public RWModeEnum RWMode { get; protected set; }
		public bool FirstRun { get; protected set; }

		public static string GetDir(String path,ModbusInitDataArray InitArray, RWModeEnum RWMode, DateTime date) {
			string dirName=String.Format("{0}\\{1}\\{2}\\{3}",path,InitArray.ID,RWMode.ToString(),date.ToString("yyyy_MM_dd"));
			return dirName;
		}

		public static String GetFileName(String path, ModbusInitDataArray InitArray, RWModeEnum RWMode, DateTime date, bool createDir) {
			string dirName=GetDir(path, InitArray, RWMode, date);
			if (createDir) {
				Directory.CreateDirectory(dirName);
			}
			string fileName=String.Format("{0}\\data_{1}.csv",dirName,date.ToString("HH_mm"));
			return fileName;
		}

		public static DateTime GetFileDate(DateTime date, RWModeEnum RWMode, bool correctTime=true) {
			int min=date.Minute;
			if (RWMode == RWModeEnum.hh) {
				min = min < 30 ? 0 : 30;
			}
			DateTime dt=new DateTime(date.Year, date.Month, date.Day, date.Hour, min, 0);
			dt = dt.AddHours(correctTime ? -Settings.single.HoursDiff : 0);
			return dt;
		}
				
		protected void getWriter(DateTime date) {
			DateTime dt=GetFileDate(DateTime.Now, RWMode);
			if (dt != CurrentDate) {
				try {					
					CurrentWriter.Close();
				} catch (Exception) { }
				CurrentDate = dt;

				string fileName=GetFileName(Settings.single.DataPath, InitArray, RWMode, CurrentDate, true);
				
				bool newFile=!File.Exists(fileName);				
				CurrentWriter=new StreamWriter(fileName,true);
				if (newFile) {
					initHeaders();
					HeaderStr = String.Format("{0};{1}", CurrentDate.ToString("dd.MM.yyyy HH:mm:ss"), String.Join(";", Headers));
					CurrentWriter.WriteLine(HeaderStr);
				} else if (FirstRun) {
					File.Copy(fileName, "temp.csv", true);
					TextReader Reader = new StreamReader(File.Open("temp.csv", FileMode.Open, FileAccess.Read, FileShare.Read));
					string first=Reader.ReadLine();
					Reader.Close();
					File.Delete("temp.csv");
					string[]hs=first.Split(';');					
					if (hs.Length > 1) {
						Headers.Clear();
						for (int i=1; i < hs.Length; i++) {
							Headers.Add(hs[i]);
						}
					}
					FirstRun = false;
				}
			}
		}

		protected void initHeaders() {
			Headers = new List<string>();
			foreach (ModbusInitData data in InitArray.Data) {
				if (data.ID.Contains("_FLAG") || !String.IsNullOrEmpty(data.Name)) {
					Headers.Add(data.ID);
				}
			}
		}

		public ModbusDataWriter(ModbusInitDataArray arr, RWModeEnum mode = RWModeEnum.hh) {
			InitArray = arr;
			Headers = new List<string>();
			RWMode = mode;
			FirstRun = true;
		}

		public void writeData( SortedList<string, double> ResultData) {
			try {
				getWriter(DateTime.Now);
				double val;
				List<double> values=new List<double>();

				foreach (string header in Headers) {
					if (ResultData.ContainsKey(header)) {
						val = ResultData[header];						
						values.Add(val);
					} else {						
						values.Add(Double.NaN);
					}

				}

				string valueStr=String.Format("{0};{1}", DateTime.Now.AddHours(-Settings.single.HoursDiff).ToString("dd.MM.yyyy HH:mm:ss"), String.Join(";", values));
				CurrentWriter.WriteLine(valueStr);
				CurrentWriter.Flush();
			} catch (Exception e) {
				Logger.Error("Ошибка при записи строки в файл");
				Logger.Error(e.Message);
			}
		}
	}
}
