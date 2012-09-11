using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using VotGES.Piramida;
using VotGES.XMLSer;

namespace ModbusLib
{
	public class Settings
	{
		protected static Settings settings;
		private string logPath;
		public string LogPath {
			get { return logPath; }
			set { logPath = value; }
		}

		private string dataPath;
		public string DataPath {
			get { return dataPath; }
			set { dataPath = value; }
		}
		
		public static Settings single {
			get {
				return settings;
			}
		}

		private int hoursDiff;
		public int HoursDiff {
			get { return hoursDiff; }
			set { hoursDiff = value; }
		}

		public List<String> InitFiles;

		private string dbDateFormat;
		public string DBDateFormat {
			get { return dbDateFormat; }
			set { dbDateFormat = value; }
		}

		private string initCalcFile;
		public string InitCalcFile {
			get { return initCalcFile; }
			set { initCalcFile = value; }
		}

		static Settings() {			
			NFIPoint = new CultureInfo("ru-RU").NumberFormat;
			NFIPoint.NumberDecimalSeparator = ".";
		}
		public static NumberFormatInfo NFIPoint;

		public static void init() {
			System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
			ci.NumberFormat.NumberDecimalSeparator = ".";
			ci.NumberFormat.NumberGroupSeparator = "";

			System.Threading.Thread.CurrentThread.CurrentCulture = ci;
			System.Threading.Thread.CurrentThread.CurrentUICulture = ci;	
						

			Settings settings=XMLSer<Settings>.fromXML("Data\\Settings.xml");
			Settings.settings = settings;
		}
	}
}
