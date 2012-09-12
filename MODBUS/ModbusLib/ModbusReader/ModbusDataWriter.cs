using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ModbusLib
{
	public class ModbusDataWriter
	{
		public DateTime CurrentDate { get; protected set; }
		public TextWriter CurrentWriter { get; protected set; }
		public ModbusInitDataArray InitArray { get; protected set; }
		public List<int> Headers { get; protected set; }
		public string HeaderStr { get; protected set; }
		public RWModeEnum RWMode { get; protected set; }

		public static string GetDir(ModbusInitDataArray InitArray, RWModeEnum RWMode, DateTime date) {
			string dirName=String.Format("{0}\\{1}\\{2}\\{3}",Settings.single.DataPath,InitArray.ID,RWMode.ToString(),date.ToString("yyyy_MM_dd"));
			return dirName;
		}

		public static String GetFileName(ModbusInitDataArray InitArray, RWModeEnum RWMode, DateTime date,bool createDir) {
			string dirName=GetDir(InitArray, RWMode, date);
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
			dt = dt.AddHours(correctTime ? Settings.single.HoursDiff : 0);
			return dt;
		}
				
		protected void getWriter(DateTime date) {
			DateTime dt=GetFileDate(DateTime.Now, RWMode);
			if (dt != CurrentDate) {
				try {					
					CurrentWriter.Close();
				} catch (Exception) { }
				CurrentDate = dt;

				string fileName=GetFileName(InitArray, RWMode, CurrentDate, true);
				
				HeaderStr = String.Format("{0};{1}", CurrentDate.ToString("dd.MM.yyyy HH:mm:ss"), String.Join(";", Headers));
				bool newFile=!File.Exists(fileName);				
				CurrentWriter=new StreamWriter(fileName,true);
				if (newFile) {
					CurrentWriter.WriteLine(HeaderStr);
				}
			}
		}

		public ModbusDataWriter(ModbusInitDataArray arr, RWModeEnum mode = RWModeEnum.hh) {
			InitArray = arr;
			Headers = new List<int>();
			foreach (ModbusInitData data in arr.Data) {
				if (!data.Name.Contains("_FLAG") && !String.IsNullOrEmpty(data.Name)) {
					Headers.Add(data.Addr);
				}
			}
			RWMode = mode;
		}

		public void writeData( SortedList<int, double> ResultData) {
			getWriter(DateTime.Now);
			double val;
			//string nm;
			List<double> values=new List<double>();
			foreach (KeyValuePair<int,double> de in ResultData) {				
				if (Headers.Contains(de.Key)) {
					val=de.Value;
					/*nm = InitArray.FullData.ContainsKey(de.Key + 1) ? InitArray.FullData[de.Key + 1].Name : "";
					if (nm.Contains("_FLAG") && ResultData[de.Key + 1] != 0)
						val = Double.NaN;*/
					values.Add(val);
				}
			}
			string valueStr=String.Format("{0};{1}",DateTime.Now.AddHours(-2).ToString("dd.MM.yyyy HH:mm:ss"), String.Join(";", values));
			CurrentWriter.WriteLine(valueStr);
			CurrentWriter.Flush();
		}
	}
}
