using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using VotGES.Piramida;
using VotGES.XMLSer;

namespace VotGES.Piramida
{
	public class DBSettings
	{
		protected static DBSettings settings;
				
		public static DBSettings single {
			get {
				return settings;
			}
		}
				
		public List<DBInfo> Databases;

		private SortedList<string,DBInfo> dbInfoList;
		[System.Xml.Serialization.XmlIgnore]
		public SortedList<string, DBInfo> DBInfoList {
			get { return dbInfoList; }
			set { dbInfoList = value; }
		}

		static DBSettings() {
			
		}

		public static void init(string filename=null) {
			if (filename == null) {
				filename="Data\\DBSettings.xml";
			}
			DBSettings settings=XMLSer<DBSettings>.fromXML(filename);
			DBSettings.settings = settings;
			settings.DBInfoList = new SortedList<string, DBInfo>();
			foreach (DBInfo dbInfo in settings.Databases) {
				settings.DBInfoList.Add(dbInfo.ID, dbInfo);
			}
		}

		public static int getSeason(DateTime date) {
			int summer=date.IsDaylightSavingTime() ? 1 : 0;
			int season=date.Year * 2;
			if (date.IsDaylightSavingTime()) {
				season++;
			} else if (date.Month>6) {
				season += 2;
			}
			return season;
		}
	}
}
