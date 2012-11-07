using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;
using VotGES.Piramida.Report;
using VotGES.Piramida;
using System.Data.SqlClient;

namespace ClearDB
{
	public class Vaht
	{
		public static void WriteVaht(DateTime dateStart, DateTime dateEnd) {
			dateStart = dateStart.Date.AddHours(-6);
			dateEnd = dateEnd.Date.AddHours(-6);
			Logger.Info(String.Format("{0} - {1}", dateStart, dateEnd));
			int[]itemsP=new int[] { 1, 52, 42, 25, 5, 14 };
			List<PiramidaEnrty> dataP=PiramidaAccess.GetDataFromDB(dateStart, dateEnd, 0, 2, 12, itemsP.ToList(), true, true, "P3000");
			SortedList<DateTime, SortedList <int,double>> data=new SortedList<DateTime, SortedList<int, double>>();
			foreach (PiramidaEnrty rec in dataP) {
				DateTime date=rec.Date.AddMinutes(-30);
				if (date.Hour >= 18 || date.Hour<6) {
					date = new DateTime(date.Year, date.Month, date.Day, 6, 0, 0).AddDays(1);
				} else if (date.Hour >= 6 && date.Hour < 18) {
					date = new DateTime(date.Year, date.Month, date.Day, 18, 0, 0);
				}
				if (!data.ContainsKey(date)) {
					data.Add(date, new SortedList<int, double>());
				}
				if (!data[date].ContainsKey(rec.Item)) {
					data[date].Add(rec.Item, 0);
				}
				data[date][rec.Item] += rec.Value0/2;
			}


			List<string> insertsStrings=new List<string>();
			List<string> dates=new List<string>();
			foreach (DateTime date in data.Keys) {
				foreach (int item in data[date].Keys) {
					insertsStrings.Add(String.Format(DBClass.InsertInfoFormat, 312, 0, item, data[date][item], 2,
						date.ToString(DBClass.DateFormat), DateTime.Now.ToString(DBClass.DateFormat), DBSettings.getSeason(date)));
				}
				dates.Add(String.Format("'{0}'", date.ToString(DBClass.DateFormat)));
			}
			List<string>ins=new List<string>();

			string delStr=String.Format("DELETE FROM DATA WHERE OBJECT=0 AND OBJTYPE=2 AND PARNUMBER=312 and DATA_DATE >= {0} and data_date<={1}", dates.First(), dates.Last());

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
				} finally {
					try { transact.Connection.Close(); } catch { }
				}
			}
		}
	}
}

