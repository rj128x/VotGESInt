using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;
using System.Data.SqlClient;
using VotGES.Piramida;

namespace ClearDB
{
	public class ClearDB
	{
		public static void Clear(DateTime dateStart, DateTime dateEnd,string DBName) {
			Logger.Info(String.Format("{0} - {1}", dateStart, dateEnd));
			String com1=String.Format("DELETE FROM DATA WHERE (parnumber=4 or parnumber=204) and DATA_DATE>='{0}' AND DATA_DATE<='{1}'",
					dateStart.ToString("yyyy-MM-dd HH:mm:ss"), dateEnd.ToString("yyyy-MM-dd HH:mm:ss"));
			run(com1, "4, 204",DBName);
			String com2=String.Format("DELETE FROM DATA WHERE parnumber in (24,26,34,36,46,101,204,213,10012,10024,10026,10034,10036,20012,20024,20026,20034,20036) and (object<>7 or object<>1) and DATA_DATE>='{0}' AND DATA_DATE<='{1}'",
				dateStart.ToString("yyyy-MM-dd HH:mm:ss"), dateEnd.ToString("yyyy-MM-dd HH:mm:ss"));
			run(com2, "parnumbers",DBName);
			/*String com3=String.Format("DELETE FROM DATA WHERE  objType=2 and (object in (53500,4)) and (parnumber in (12,212,226)) and DATA_DATE>='{0}' AND DATA_DATE<='{1}'",
				dateStart.ToString("yyyy-MM-dd HH:mm:ss"), dateEnd.ToString("yyyy-MM-dd HH:mm:ss"));
			run(com3, "53500 4");*/
		}

		public static void run(string com, string name = "", string DBName="P3000") {
			SqlConnection con=null;
			try {
				Logger.Info("==" + name);
				con = PiramidaAccess.getConnection(DBName);
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
	}
}
