using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VotGES.Piramida;
using VotGES;
using VotGES.Rashod;

namespace ClearDB
{


	class Program
	{

		protected static DateTime getDate(string ds, bool minutes = false) {
			DateTime date;
			bool ok=DateTime.TryParse(ds, out date);
			if (!ok) {
				int hh=0;
				ok = Int32.TryParse(ds, out hh);
				DateTime now=DateTime.Now;
				date = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
				date = !minutes ? date.AddHours(-hh) : date.AddMinutes(-hh).AddMinutes(DateTime.Now.Minute);
			}
			return date;
		}


		static void Main(string[] args) {
			DBSettings.init();
			Settings.init();
			DBClass.DateFormat = Settings.single.DBDateFormat;

			string ds=args[0];
			string de=args[1];
			string task=args[2];
			string nameLog=task;

			Logger.InitFileLogger(Settings.single.LogPath, nameLog);

			DateTime dateStart=getDate(ds, task == "copy4");
			DateTime dateEnd=getDate(de, task == "copy4");

			Logger.Info("======================================================================================");
			Logger.Info(task);
			if ((new string[] { "copy4", "copy12", "copy212","copy204" }).Contains(task)) {
				Logger.Info("Find lastDate");
				List<int> pn4=(new int[] { 4 }).ToList();
				List<int> pn12=(new int[] { 12 }).ToList();
				List<int> pn212=(new int[] { 212 }).ToList();
				List<int> pn204=(new int[] { 204 }).ToList();
				List<int> pn=pn12;
				DateTime dts=dateStart;
				int minutes=30;
				string db="P3000";
				switch (task) {
					case "copy4":
						pn = pn4;
						dts = dateStart.AddHours(-6);
						minutes = 1;
						db = "PMin";
						break;
					case "copy204":
						pn = pn204;
						dts = dateStart.AddDays(-1);
						minutes = 1;
						db = "PMin";
						break;
					case "copy12":
						pn = pn12;
						dts = dateStart.AddDays(-1);
						minutes = 30;
						db = "P3000";
						break;
					case "copy212":
						pn = pn212;
						dts = dateStart.AddDays(-1);
						db = "P3000";
						minutes = 30;
						break;
				}
				DateTime dt=CopyData.getLastDate(dts, dateStart, pn, db, minutes);
				if (dt < dateStart) {
					dateStart = dt;
				}
			}

			DateTime date=dateStart.AddMinutes(0);						

			if (task == "sutVed") {
				SutVed.ProcessFolder(Settings.single.SutVedPath, Settings.single.SutVedPathTo);
			} else {
				double hh=24;
				while (date <= dateEnd) {
					switch (task) {
						case "clear3000":
							hh = 24;
							ClearDB.Clear(date, date.AddHours(hh), "P3000");
							break;						
						case "clearMin":
							hh = 24;
							ClearDB.Clear(date, date.AddHours(hh), "PMin");
							break;
						case "optim":
							hh = 24;
							Water.CalcOptim(date, date.AddHours(hh));
							break;
						case "rewriteWater":
							hh = 24;
							Water.rewriteWater(date, date.AddHours(hh));
							break;
						case "temp":
							hh = 240;
							Temp.WriteTemp(date, date.AddHours(hh));
							break;
						case "vaht":
							hh = 240;
							Vaht.WriteVaht(date, date.AddHours(hh));
							break;
						case "copy12":
							hh = 4;
							CopyData.WriteCopy(date, date.AddHours(hh), (new int[] { 12 }).ToList(), "P3000");
							break;
						case "copy212":
							hh = 24;
							CopyData.WriteCopy(date, date.AddHours(hh), (new int[] { 212 }).ToList(), "P3000");
							break;
						case "copy4":
							hh = 1.0 / 6.0;
							CopyData.WriteCopy(date, date.AddHours(hh), (new int[] { 4 }).ToList(), "PMin");
							break;
						case "copy204":
							hh = 4;
							CopyData.WriteCopy(date, date.AddHours(hh), (new int[] { 204 }).ToList(), "PMin");
							break;
					}
					date = date.AddHours(hh);
				}
			}
		}







	}
}
