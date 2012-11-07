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
			String com2=String.Format("DELETE FROM DATA WHERE parnumber in (24,26,34,36,46,101,204,213,10012,10024,10026,10034,10036,20012,20024,20026,20034,20036) and (object<>7 and object<>1) and DATA_DATE>='{0}' AND DATA_DATE<='{1}'",
				dateStart.ToString("yyyy-MM-dd HH:mm:ss"), dateEnd.ToString("yyyy-MM-dd HH:mm:ss"));
			run(com2, "parnumbers",DBName);
			/*String com3=String.Format("DELETE FROM DATA WHERE  objType=2 and (object in (53500,4)) and (parnumber in (12,212,226)) and DATA_DATE>='{0}' AND DATA_DATE<='{1}'",
				dateStart.ToString("yyyy-MM-dd HH:mm:ss"), dateEnd.ToString("yyyy-MM-dd HH:mm:ss"));
			run(com3, "53500 4");*/
		}

		public static void FindDupl(DateTime dateStart, DateTime dateEnd, string DBName,bool del=true) {
			Logger.Info(String.Format("{0} - {1}", dateStart, dateEnd));
			String com=String.Format("SELECT  DATA_DATE,OBJECT,OBJTYPE,ITEM,PARNUMBER,MIN(SEASON),COUNT(VALUE0) FROM DATA WHERE DATA_DATE>='{0}' AND DATA_DATE<='{1}' GROUP BY DATA_DATE,OBJECT,OBJTYPE,ITEM,PARNUMBER HAVING COUNT(VALUE0)>1 ",
					dateStart.ToString("yyyy-MM-dd HH:mm:ss"), dateEnd.ToString("yyyy-MM-dd HH:mm:ss"));
			SqlConnection con=null;
			List<string> deletes=new List<string>();
			try {
				con = PiramidaAccess.getConnection(DBName);
				con.Open();

				SqlCommand command=con.CreateCommand();
				command.CommandType = System.Data.CommandType.Text;
				command.CommandText = com;
				command.CommandTimeout = 60;
				SqlDataReader reader=command.ExecuteReader();
				while (reader.Read()) {
					Logger.Info(String.Format("{0} - {1} - {2} - {3} - {4} - {5} - {6} ", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]));
					if (del) {
						DateTime date=DateTime.Parse(reader[0].ToString());
						if (date.Month != 10) {
							com=String.Format("DELETE FROM DATA WHERE data_date='{0}' and object={1} and objtype={2} and item={3} and parnumber={4} and season={5}",
								date.ToString("yyyy-MM-dd HH:mm:ss"), reader[1], reader[2], reader[3], reader[4], reader[5]);
							deletes.Add(com);
							
						}
					}
				}
				reader.Close();
				foreach (string c in deletes) {
					SqlCommand command1 = con.CreateCommand();
					command1.CommandType = System.Data.CommandType.Text;
					command1.CommandText = c;
					command1.CommandTimeout = 60;
					command1.ExecuteNonQuery();
				}

				Logger.Info("--finish");
			} catch (Exception e) {
				Logger.Info(e.Message);
			} finally {
				try { con.Close(); } catch { }
			}
		}

		public static void RefreshSeason(DateTime dateStart, DateTime dateEnd, string DBName) {
			Logger.Info(String.Format("{0} - {1}", dateStart, dateEnd));
			int season1=DBSettings.getSeason(dateStart);
			int season2=DBSettings.getSeason(dateEnd);
			if (season1 != season2) {
				DateTime dt=dateStart.AddHours(0);
				while (DBSettings.getSeason(dt)==season1) {
					dt = dt.AddMinutes(30);
				}
				dt = dt.AddMinutes(-30);
				RefreshSeason(dateStart, dt,DBName);
				RefreshSeason(dt.AddMinutes(30), dateEnd, DBName);
				return;
			}
			SqlConnection con=null;
			List<string> deletes=new List<string>();
			try {
				con = PiramidaAccess.getConnection(DBName);
				con.Open();

				SqlCommand command=con.CreateCommand();
				command.CommandType = System.Data.CommandType.Text;
				string com=String.Format("UPDATE DATA SET SEASON={0} WHERE data_date>='{1}' and data_date<='{2}' and season<4000",
						DBSettings.getSeason(dateStart), dateStart.ToString("yyyy-MM-dd HH:mm:ss"), dateEnd.ToString("yyyy-MM-dd HH:mm:ss"));
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
