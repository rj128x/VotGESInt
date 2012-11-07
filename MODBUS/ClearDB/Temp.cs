using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;
using System.Data.SqlClient;
using VotGES.Piramida;
using VotGES.Piramida.Report;

namespace ClearDB
{
	public class Temp
	{
		public static void WriteTemp(DateTime dateStart, DateTime dateEnd) {
			Logger.Info(String.Format("{0} - {1}", dateStart, dateEnd));
			FullReport report=new FullReport(dateStart.Date, dateEnd.Date, IntervalReportEnum.day);
			List<string> need=new List<string>();
			need.Add(PiramidaRecords.Water_Temp.Key);
			report.InitNeedData(need);
			report.ReadData();

			List<string> insertsStrings=new List<string>();
			List<string> dates=new List<string>();
			foreach (DateTime date in report.Dates) {
				insertsStrings.Add(String.Format(DBClass.InsertInfoFormat,26,1,373,report[date,PiramidaRecords.Water_Temp.Key],2,
					date.ToString(DBClass.DateFormat), DateTime.Now.ToString(DBClass.DateFormat), DBSettings.getSeason(date)));
				dates.Add(String.Format("'{0}'",date.ToString(DBClass.DateFormat)));
			}
			List<string>ins=new List<string>();

			string delStr=String.Format("DELETE FROM DATA WHERE OBJECT=1 AND OBJTYPE=2 AND PARNUMBER=26 and item=373 and DATA_DATE IN ({0})", String.Join(",", dates));
			
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
