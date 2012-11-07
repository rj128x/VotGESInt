using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;
using VotGES.Piramida;
using VotGES.Rashod;
using System.Data.SqlClient;

namespace ClearDB
{
	public class OptimRashodRec
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
			Napor = Napor > 23 ? 23 : Napor;
			Q_OPT_GTP1 = RUSA.getOptimRashod(P_GTP1 / 1000, Napor, true, null, gtp1);
			Q_OPT_GTP2 = RUSA.getOptimRashod(P_GTP2 / 1000, Napor, true, null, gtp2);
			Q_OPT_GES = RUSA.getOptimRashod(P_GES / 1000, Napor, true, null, ges);
		}
	}

	public class Water
	{
		public static void CalcOptim(DateTime dateStart, DateTime dateEnd) {
			System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
			ci.NumberFormat.NumberDecimalSeparator = ".";
			ci.NumberFormat.NumberGroupSeparator = "";

			System.Threading.Thread.CurrentThread.CurrentCulture = ci;
			System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

			Logger.Info(String.Format("{0} - {1}", dateStart, dateEnd));
			SortedList<DateTime,OptimRashodRec> Data=new SortedList<DateTime, OptimRashodRec>();
			int[]itemsP=new int[] { 1, 2, 3 };
			int[]itemsW=new int[] { 276 };
			List<PiramidaEnrty> dataP=PiramidaAccess.GetDataFromDB(dateStart, dateEnd, 0, 2, 12, itemsP.ToList(), true, true, "P3000");
			List<PiramidaEnrty> dataW=PiramidaAccess.GetDataFromDB(dateStart, dateEnd, 1, 2, 12, itemsW.ToList(), true, true, "P3000");
			foreach (PiramidaEnrty rec in dataP) {
				OptimRashodRec DataRec;
				if (!Data.ContainsKey(rec.Date)) {
					DataRec = new OptimRashodRec();
					Data.Add(rec.Date, DataRec);
					DataRec.Date = rec.Date;
				}
				DataRec = Data[rec.Date];
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
				Data[rec.Date].Napor = rec.Value0;
			}

			List<string> deletesStrings=new List<string>();
			List<string> insertsStrings=new List<string>();
			List<string> dates=new List<string>();
			
			foreach (OptimRashodRec DataRec in Data.Values) {
				if (DataRec.Napor == 0) {
					continue;
				}
				DataRec.calc();
				dates.Add(String.Format("'{0}'", DataRec.Date.ToString(DBClass.DateFormat)));
				string ins1=String.Format(DBClass.InsertInfoFormat, 12, 10, 1, DataRec.Q_OPT_GES, 2, DataRec.Date.ToString(DBClass.DateFormat), DateTime.Now.ToString(DBClass.DateFormat), DBSettings.getSeason(DataRec.Date));
				string ins2=String.Format(DBClass.InsertInfoFormat, 12, 10, 2, DataRec.Q_OPT_GTP1, 2, DataRec.Date.ToString(DBClass.DateFormat), DateTime.Now.ToString(DBClass.DateFormat), DBSettings.getSeason(DataRec.Date));
				string ins3=String.Format(DBClass.InsertInfoFormat, 12, 10, 3, DataRec.Q_OPT_GTP2, 2, DataRec.Date.ToString(DBClass.DateFormat), DateTime.Now.ToString(DBClass.DateFormat), DBSettings.getSeason(DataRec.Date));
				insertsStrings.Add(ins1);
				insertsStrings.Add(ins2);
				insertsStrings.Add(ins3);
			}


			string delStr=String.Format("DELETE FROM DATA WHERE OBJECT=10 AND OBJTYPE=2 AND PARNUMBER=12 and DATA_DATE IN ({0})", String.Join(",", dates));

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
			Logger.Info("--finish");

		}

		protected static void checkWater(int ga, SortedList<int, double> data, SortedList<int, double> dataPFull, int itemP, int itemQ, int itemNapor) {
			try {
				if (!data.ContainsKey(itemQ)) {
					data.Add(itemQ, 0);
				}
				double napor=data[itemNapor];
				double p=dataPFull[itemP];
				p = p > 100000 ? 100000 : p;
				double q=RashodTable.getRashod(ga, p / 1000, napor);
				if (data[itemQ] <= 0) {
					data[itemQ] = q;
				}
				try {
					if ((data[itemQ] / q >= 1.2) || (q / data[itemQ] >= 1.2)) {
						data[itemQ] = q;
					}
				} catch { }

			} catch (Exception e) {
				Logger.Info("checkWater");
				Logger.Info(e.ToString());
			}
		}

		public static void rewriteWater(DateTime dateStart, DateTime dateEnd) {			

			Logger.Info(String.Format("{0} - {1}", dateStart, dateEnd));
			SortedList<DateTime,OptimRashodRec> Data=new SortedList<DateTime, OptimRashodRec>();
			int[]itemsW=new int[] { 104, 129, 154, 179, 204, 229, 254, 279, 304, 329, 354, 276, 373, 275, 274 };
			int[]itemsP=new int[] { 2, 6, 18, 22, 26, 30, 42, 46, 50, 54 };
			List<PiramidaEnrty> dataW=PiramidaAccess.GetDataFromDB(dateStart, dateEnd, 1, 2, 12, itemsW.ToList(), true, true, "P3000");
			List<PiramidaEnrty> dataP=PiramidaAccess.GetDataFromDB(dateStart, dateEnd, 8738, 0, 12, itemsP.ToList(), true, true, "P3000");

			List<string> deletesStrings=new List<string>();
			List<string> insertsStrings=new List<string>();
			List<string> dates=new List<string>();

			SortedList<DateTime,SortedList<int,double>> data=new SortedList<DateTime, SortedList<int, double>>();
			SortedList<DateTime,SortedList<int,double>> dataPFull=new SortedList<DateTime, SortedList<int, double>>();

			DateTime date=dateStart;
			while (date <= dateEnd) {
				data.Add(date, new SortedList<int, double>());
				date = date.AddMinutes(30);
			}

			foreach (PiramidaEnrty rec in dataW) {
				if (!data.Keys.Contains(rec.Date)) {
					data.Add(rec.Date, new SortedList<int, double>());
				}
				if (!data[rec.Date].Keys.Contains(rec.Item)) {
					data[rec.Date].Add(rec.Item, rec.Value0);
				} else {
					if (data[rec.Date][rec.Item] > rec.Value0) {
						continue;
					} else {
						data[rec.Date][rec.Item] = rec.Value0;
					}
				}
			}

			Logger.Info(dataP.Count.ToString());
			foreach (PiramidaEnrty rec in dataP) {
				if (!dataPFull.Keys.Contains(rec.Date)) {
					dataPFull.Add(rec.Date, new SortedList<int, double>());
				}
				if (!dataPFull[rec.Date].Keys.Contains(rec.Item)) {
					dataPFull[rec.Date].Add(rec.Item, rec.Value0);
				} else {
					if (dataPFull[rec.Date][rec.Item] > rec.Value0) {
						continue;
					} else {
						dataPFull[rec.Date][rec.Item] = rec.Value0;
					}
				}
			}


			DateTime prevDate=DateTime.MinValue;
			int[] w=new int[] { 276, 373, 274, 275 };
			foreach (KeyValuePair<DateTime,SortedList<int,double>> rec in data) {
				dates.Add(String.Format("'{0}'", rec.Key.ToString(DBClass.DateFormat)));

				try {
					if (prevDate > DateTime.MinValue) {
						foreach (int item in w) {
							if (!rec.Value.ContainsKey(item)) {
								rec.Value.Add(item, 0);
							}
							double prev=data[prevDate][item];
							if (rec.Value[item] == 0) {
								rec.Value[item] = prev;
							}
						}
					}

					checkWater(1, rec.Value, dataPFull[rec.Key], 2, 104, 276);
					checkWater(2, rec.Value, dataPFull[rec.Key], 6, 129, 276);
					checkWater(3, rec.Value, dataPFull[rec.Key], 18, 154, 276);
					checkWater(4, rec.Value, dataPFull[rec.Key], 22, 179, 276);
					checkWater(5, rec.Value, dataPFull[rec.Key], 26, 204, 276);
					checkWater(6, rec.Value, dataPFull[rec.Key], 30, 229, 276);
					checkWater(7, rec.Value, dataPFull[rec.Key], 42, 254, 276);
					checkWater(8, rec.Value, dataPFull[rec.Key], 46, 279, 276);
					checkWater(9, rec.Value, dataPFull[rec.Key], 50, 304, 276);
					checkWater(10, rec.Value, dataPFull[rec.Key], 54, 329, 276);

					if (!rec.Value.ContainsKey(354)) {
						rec.Value.Add(354, 0);
					}
					double q = rec.Value[104] + rec.Value[129] + rec.Value[154] + rec.Value[179] + rec.Value[204] +
						rec.Value[229] + rec.Value[254] + rec.Value[279] + rec.Value[304] + rec.Value[329];
					if (rec.Value[354] <= 0) {
						rec.Value[354] = q;
					}
					try {
						if (Math.Abs(rec.Value[354] - q) > 10) {
							rec.Value[354] = q;
						}
					} catch { }

				} catch (Exception e) {
					Logger.Info(e.ToString());
				}

				foreach (KeyValuePair<int,double>de in rec.Value) {
					string ins1=String.Format(DBClass.InsertInfoFormat, 12, 1, de.Key, de.Value, 2, rec.Key.ToString(DBClass.DateFormat), rec.Key.ToString(DBClass.DateFormat), DBSettings.getSeason(rec.Key));
					insertsStrings.Add(ins1);
				}

				prevDate = rec.Key;
			}

			List<string>ins=new List<string>();

			string delStr=String.Format("DELETE FROM DATA WHERE OBJECT=1 AND OBJTYPE=2 AND PARNUMBER=12 and DATA_DATE IN ({0})", String.Join(",", dates));

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

			Logger.Info("--finish");

		}

	}
}
