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
	class OptimRashodRec
	{
		public static List<int>gtp1=new List<int>(new int[] { 1, 2 });
		public static List<int>gtp2=new List<int>(new int[] { 3, 4, 5, 6, 7, 8, 9, 10 });
		public static List<int>ges=new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

		public DateTime Date { get; set; }
		public double P_GTP1 { get; set; }
		public double P_GTP2 { get; set; }
		public double P_GES { get; set; }
		public double Napor { get; set; }

		public double Q_OPT_GTP1 { get; set; }
		public double Q_OPT_GTP2 { get; set; }
		public double Q_OPT_GES { get; set; }

		public void calc() {			
			Q_OPT_GTP1 = RUSA.getOptimRashod(P_GTP1 / 1000, Napor, true, null, gtp1);
			Q_OPT_GTP2 = RUSA.getOptimRashod(P_GTP2 / 1000, Napor, true, null, gtp2);
			Q_OPT_GES = RUSA.getOptimRashod(P_GES / 1000, Napor, true, null, ges);
		}
	}
	class Program
	{
		static void Main(string[] args) {
			DBSettings.init();
			Logger.InitFileLogger("c:\\clearDB","clear");
			DateTime dateStart=new DateTime(2012, 1, 1);
			DateTime dateEnd=new DateTime(2012, 10, 1);
			
			DateTime date=dateStart.AddMinutes(0);
			while (date <= dateEnd) {
				//Clear(date, date.AddHours(24));
				CalcOptim(date, date.AddHours(24));
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


		public static void CalcOptim(DateTime dateStart, DateTime dateEnd) {
			System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
			ci.NumberFormat.NumberDecimalSeparator = ".";
			ci.NumberFormat.NumberGroupSeparator = "";

			System.Threading.Thread.CurrentThread.CurrentCulture = ci;
			System.Threading.Thread.CurrentThread.CurrentUICulture = ci;	

			Logger.Info(String.Format("{0} - {1}", dateStart, dateEnd));
			SortedList<DateTime,OptimRashodRec> Data=new SortedList<DateTime,OptimRashodRec>();
			int[]itemsP=new int[]{1,2,3};
			int[]itemsW=new int[] { 276 };
			List<PiramidaEnrty> dataP=PiramidaAccess.GetDataFromDB(dateStart, dateEnd, 0, 2, 12, itemsP.ToList(), true, true, "P3000");
			List<PiramidaEnrty> dataW=PiramidaAccess.GetDataFromDB(dateStart, dateEnd, 1, 2, 12, itemsW.ToList(), true, true, "P3000");
			foreach (PiramidaEnrty rec in dataP) {		
				OptimRashodRec DataRec;
				if (!Data.ContainsKey(rec.Date)){
					DataRec=new OptimRashodRec();
					Data.Add(rec.Date,DataRec);
					DataRec.Date=rec.Date;
				}
				DataRec=Data[rec.Date];
				switch (rec.Item) {
					case 1:
						DataRec.P_GES = rec.Value0;
						break;
					case 2:
						DataRec.P_GTP1 = rec.Value0;
						break;
					case 3:
						DataRec.P_GTP2 = rec.Value0;
						break;
				}
			}
			foreach (PiramidaEnrty rec in dataW) {
				if (!Data.ContainsKey(rec.Date)) {
					continue;
				}
				Data[rec.Date].Napor=rec.Value0;				
			}

			List<string> deletesStrings=new List<string>();
			List<string> insertsStrings=new List<string>();
			List<string> dates=new List<string>();
			string insertIntoHeader="INSERT INTO Data (parnumber,object,item,value0,objtype,data_date,rcvstamp,season)";
			string frmt="SELECT {0}, {1}, {2}, {3}, {4}, '{5}', '{6}', {7}";

			foreach (OptimRashodRec DataRec in Data.Values) {
				if (DataRec.Napor == 0) {
					continue;
				}
				DataRec.calc();
				dates.Add(String.Format("'{0}'",DataRec.Date.ToString("yyyy-MM-dd HH:mm:ss")));
				string ins1=String.Format(frmt, 12, 10, 1, DataRec.Q_OPT_GES, 2, DataRec.Date.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0);
				string ins2=String.Format(frmt, 12, 10, 2, DataRec.Q_OPT_GTP1, 2, DataRec.Date.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0);
				string ins3=String.Format(frmt, 12, 10, 3, DataRec.Q_OPT_GTP2, 2, DataRec.Date.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0);
				insertsStrings.Add(ins1);
				insertsStrings.Add(ins2);
				insertsStrings.Add(ins3);				
			}

			string insertsSQL=String.Join("\nUNION ALL\n", insertsStrings);
			string insertSQL=String.Format("{0}\n{1}", insertIntoHeader, insertsSQL);

			string delStr=String.Format("DELETE FROM DATA WHERE OBJECT=10 AND OBJTYPE=2 AND PARNUMBER=12 and DATA_DATE IN ({0})", String.Join(",",dates));

			if (dates.Count > 0) {
				SqlConnection con=null;
				try {
					con = PiramidaAccess.getConnection("P3000");
					con.Open();
					SqlCommand commandDel=con.CreateCommand();
					commandDel.CommandText = delStr;
					//Logger.Info(delStr);
					commandDel.ExecuteNonQuery();

					SqlCommand commandIns=con.CreateCommand();
					commandIns.CommandText = insertSQL;
					//Logger.Info(insertSQL);
					commandIns.ExecuteNonQuery();
				} catch (Exception e) {
					Logger.Info(e.ToString());
				} finally {
					try { con.Close(); } catch { }
				}
			}
			Logger.Info("--finish");

		}
	}
}
