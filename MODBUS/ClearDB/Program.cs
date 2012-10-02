using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VotGES.Piramida;
using VotGES;

namespace ClearDB
{
	class Program
	{
		static void Main(string[] args) {
			DBSettings.init();
			Logger.InitFileLogger("c:\\clearDB","clear");
			DateTime dateStart=new DateTime(2011, 1, 1);
			DateTime dateEnd=new DateTime(2012, 10, 1);
			
			DateTime date=dateStart.AddMinutes(0);
			while (date <= dateEnd) {
				Clear(date, date.AddHours(24));
				date = date.AddHours(24);
			}
		}

		public static void run(string com,string name="") {
			SqlConnection con=null;
			try {
				Logger.Info("=="+name);
				con = PiramidaAccess.getConnection("P3000");
				con.Open();
				
				SqlCommand command=con.CreateCommand();
				command.CommandType = System.Data.CommandType.Text;
				command.CommandText = com;
				command.CommandTimeout = 60;
				command.ExecuteNonQuery();

				Logger.Info("--finish");
			} catch (Exception e) {
				Logger.Info(e.Message);
			} finally {
				try { con.Close(); } catch { }
			}
		}

		public static void Clear(DateTime dateStart, DateTime dateEnd) {
			Logger.Info(String.Format("{0} - {1}", dateStart, dateEnd));
			String com1=String.Format("DELETE FROM DATA WHERE (parnumber=4 or parnumber=204) and DATA_DATE>='{0}' AND DATA_DATE<='{1}'", 
					dateStart.ToString("yyyy-MM-dd HH:mm:ss"), dateEnd.ToString("yyyy-MM-dd HH:mm:ss"));
			run(com1,"4");
			String com2=String.Format("DELETE FROM DATA WHERE parnumber in (24,26,34,36,46,101,204,213,312,10012,10024,10026,10034,10036,20012,20024,20026,20034,20036) and object<>7 and DATA_DATE>='{0}' AND DATA_DATE<='{1}'",
				dateStart.ToString("yyyy-MM-dd HH:mm:ss"), dateEnd.ToString("yyyy-MM-dd HH:mm:ss"));
			run(com2,"parnumbers");
			String com3=String.Format("DELETE FROM DATA WHERE  objType=2 and (object in (53500,4)) and (parnumber in (12,212,226)) and DATA_DATE>='{0}' AND DATA_DATE<='{1}'",
				dateStart.ToString("yyyy-MM-dd HH:mm:ss"), dateEnd.ToString("yyyy-MM-dd HH:mm:ss"));
			run(com3, "53500 4");
		}
	}
}
