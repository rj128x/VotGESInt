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

		protected static DateTime getDate(string ds) {
			DateTime date;
			bool ok=DateTime.TryParse(ds, out date);
			if (!ok) {
				int hh=0;
				ok = Int32.TryParse(ds, out hh);
				DateTime now=DateTime.Now;
				date = new DateTime(now.Year, now.Month, now.Day, now.Hour,0,0);
				date = date.AddHours(-hh);
			}
			return date;
		}
		static void Main(string[] args) {
			DBSettings.init();
			
			string ds=args[0];
			string de=args[1];
			string task=args[2];
			string pathLog=args[3];

			Logger.InitFileLogger(pathLog, "task");

			DateTime dateStart=getDate(ds);
			DateTime dateEnd=getDate(de);


			System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
			ci.NumberFormat.NumberDecimalSeparator = ".";
			ci.NumberFormat.NumberGroupSeparator = "";

			System.Threading.Thread.CurrentThread.CurrentCulture = ci;
			System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

			DateTime date=dateStart.AddMinutes(0);
			Logger.Info(task);
			int hh=24;
			while (date <= dateEnd) {								
				switch (task) {
					case "clear":
						hh = 24;
						ClearDB.Clear(date,date.AddHours(hh));
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
				}
				date = date.AddHours(hh);
			}
		}

		

		


		
	}
}
