using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace VotGES.Piramida.Report
{
	public enum PuskStopState { start, stop, startGR, stopGR, startSK, stopSK }

	public class PuskStopEvent
	{
		public class EventGA
		{
			public int GA { get; set; }
			public bool? Start { get; set; }
			public bool? Stop { get; set; }
			public bool? StartGR { get; set; }
			public bool? StopGR { get; set; }
			public bool? StartSK { get; set; }
			public bool? StopSK { get; set; }
			public bool Runned { get; set; }
			public bool RunnedSK { get; set; }
			public bool RunnedGR { get; set; }

		}
		public DateTime Date { get; set; }
		public SortedList<int,EventGA> Data;
		public double DiffMin{get;set;}

		public PuskStopEvent() {
			Data = new SortedList<int, EventGA>();
		}

		public static string getValue(bool? valStart, bool? valStop, string strStart, string strStop, string strNone) {
			string str=strNone;
			if (!valStart.HasValue && !valStop.HasValue) {
				return str;
			}
			if (valStart.HasValue && valStart.Value) {
				str = strStart;
			}
			if (valStop.HasValue && valStop.Value) {
				str = strStop;
			}
			return str;
		}


	}

	public class PuskStopReportFull
	{
		public DateTime DateStart { get; protected set; }
		public DateTime DateEnd { get; protected set; }
		public SortedList<DateTime, PuskStopEvent> Data { get; protected set; }

		public PuskStopReportFull(DateTime DateStart, DateTime DateEnd) {
			this.DateEnd = DateEnd;
			this.DateStart = DateStart;

		}

		public void ReadData() {
			Data = new SortedList<DateTime, PuskStopEvent>();
			SqlConnection con=null;
			try {
				string sel=String.Format("SELECT data_date, item, value0  FROM DATA WHERE Parnumber=13 and object=30 and objtype=2 and item>=1 and item<=30 and data_date>=@dateStart and data_date<=@dateEnd");
				for (int item=1; item <= 30; item++) {
					int ga= item % 10;
					ga = ga == 0 ? 10 : ga;
					if (item <= 10 || (ga <= 2 || ga >= 9)) {
						sel += String.Format("\n UNION ALL \n SELECT data_date, item, value0  FROM DATA WHERE Parnumber=13 and object=30 and objtype=2 and item={0} and data_date= (SELECT top 1 data_date from data WHERE Parnumber=13 and object=30 and objtype=2 and item={0} and data_date<@dateStart order by data_date desc)",item);
					}
					
				}
				con = PiramidaAccess.getConnection("PSV");
				con.Open();
				SqlCommand command=con.CreateCommand();
				command.CommandText = sel;
				//Logger.Info(sel);
				command.Parameters.AddWithValue("@dateStart", DateStart);
				command.Parameters.AddWithValue("@dateEnd", DateEnd);

				SqlDataReader reader=command.ExecuteReader();
				while (reader.Read()) {
					DateTime date=Convert.ToDateTime(reader[0]);
					int item=Convert.ToInt32(reader[1]);
					int value0=Convert.ToInt32(reader[2]);
					int ga=item % 10;
					ga = ga == 0 ? 10 : ga;

					if (item <= 10) {
						if (value0 == 1) {
							AddData(date, ga, PuskStopState.start);
						}
						if (value0 == 0) {
							AddData(date, ga, PuskStopState.stop);
						}
					} else if (item <= 20) {
						if (value0 == 1) {
							AddData(date, ga, PuskStopState.startSK);
						}
						if (value0 == 0) {
							AddData(date, ga, PuskStopState.stopSK);
						}
					} else if (item <= 30) {
						if (value0 == 1) {
							AddData(date, ga, PuskStopState.startGR);
						}
						if (value0 == 0) {
							AddData(date, ga, PuskStopState.stopGR);
						}
					}
				}
				ProcessData();
			} catch (Exception e) {
				Logger.Error("Ошибка при получении пусков-остановов (подробно)");
				Logger.Error(e.ToString());
			} finally { try { con.Close(); } catch { } }
		}

		protected void AddData(DateTime date, int ga, PuskStopState state) {
			PuskStopEvent ev;
			if (!Data.ContainsKey(date)) {
				ev = new PuskStopEvent();
				ev.Date = date;
				for (int g=1; g <= 10; g++) {
					PuskStopEvent.EventGA evGA=new PuskStopEvent.EventGA();
					evGA.GA = g;
					ev.Data.Add(g, evGA);
				}
				Data.Add(date, ev);
			}
			ev = Data[date];
			switch (state) {
				case PuskStopState.start:
					ev.Data[ga].Start = true;
					break;
				case PuskStopState.stop:
					ev.Data[ga].Stop = true;
					break;
				case PuskStopState.startGR:
					ev.Data[ga].StartGR = true;
					break;
				case PuskStopState.stopGR:
					ev.Data[ga].StopGR = true;
					break;
				case PuskStopState.startSK:
					ev.Data[ga].StartSK = true;
					break;
				case PuskStopState.stopSK:
					ev.Data[ga].StopSK = true;
					break;
			}
		}

		protected void ProcessData() {
			for (int ga=1; ga <= 10; ga++) {
				DateTime prevDate=DateTime.MinValue;
				List<DateTime>prevDates=new List<DateTime>();
				bool isFirst=true;
				foreach (DateTime date in Data.Keys) {
					PuskStopEvent.EventGA ev=Data[date].Data[ga];
					if (ev.Start.HasValue || ev.Stop.HasValue) {
						ev.Runned = ev.Start.HasValue && ev.Start.Value;
						if (isFirst) {
							foreach (DateTime dt in prevDates) {
								Data[dt].Data[ga].Runned = !ev.Runned;
							}
						}
						isFirst = false;
					} else {
						if (!isFirst) {
							if (prevDate > DateTime.MinValue) {
								ev.Runned = Data[prevDate].Data[ga].Runned;
							}
						} else {
							prevDates.Add(date);
						}
					}
					prevDate = date;
				}

				if (ga <= 2 || ga >= 9) {
					prevDate = DateTime.MinValue;
					prevDates = new List<DateTime>();
					isFirst = true;
					foreach (DateTime date in Data.Keys) {
						PuskStopEvent.EventGA ev=Data[date].Data[ga];
						if (ev.StartGR.HasValue || ev.StopGR.HasValue) {
							ev.RunnedGR = ev.StartGR.HasValue && ev.StartGR.Value;
							if (isFirst) {
								foreach (DateTime dt in prevDates) {
									Data[dt].Data[ga].RunnedGR = !ev.RunnedGR;
								}
							}
							isFirst = false;
						} else {
							if (!isFirst) {
								if (prevDate > DateTime.MinValue) {
									ev.RunnedGR = Data[prevDate].Data[ga].RunnedGR;
								}
							} else {
								prevDates.Add(date);
							}
						}
						prevDate = date;
					}

					prevDate = DateTime.MinValue;
					prevDates = new List<DateTime>();
					isFirst = true;
					foreach (DateTime date in Data.Keys) {
						PuskStopEvent.EventGA ev=Data[date].Data[ga];
						if (ev.StartSK.HasValue || ev.StopSK.HasValue) {
							ev.RunnedSK = ev.StartSK.HasValue && ev.StartSK.Value;
							if (isFirst) {
								foreach (DateTime dt in prevDates) {
									Data[dt].Data[ga].RunnedSK = !ev.RunnedSK;
								}
							}
							isFirst = false;
						} else {
							if (!isFirst) {
								if (prevDate > DateTime.MinValue) {
									ev.RunnedSK = Data[prevDate].Data[ga].RunnedSK;
								}
							} else {
								prevDates.Add(date);
							}
						}
						prevDate = date;
					}
				}
			}

			List<DateTime> dates=Data.Keys.ToList();
			foreach (DateTime date in dates) {
				if (date < DateStart) {
					Data.Remove(date);
				}
			}

			foreach (PuskStopEvent ev in Data.Values) {
				try {
					DateTime next=Data.First(de => de.Key > ev.Date).Key;
					ev.DiffMin = PuskStopRecord.getDiffMin(ev.Date, next);
				} catch { }
			}
		}

		public static Dictionary<int, string> TimeStopGA() {
			Dictionary<int,string> Result=new Dictionary<int, string>();
			string sel;
			SqlConnection con=null;
			try {
				List<string> sels=new List<string>();
				for (int item=1; item <= 10; item++) {
					int ga= item % 10;
					ga = ga == 0 ? 10 : ga;
					sel = String.Format("SELECT data_date, item, value0  FROM DATA WHERE Parnumber=13 and object=30 and objtype=2 and item={0} and data_date= (SELECT top 1 data_date from data WHERE Parnumber=13 and object=30 and objtype=2 and item={0} and data_date<@dateStart order by data_date desc)",item);
					sels.Add(sel);					
				}
				sel=String.Join("\n UNION ALL \n", sels);
				con = PiramidaAccess.getConnection("PSV");
				con.Open();
				SqlCommand command=con.CreateCommand();
				command.CommandText = sel;
				command.Parameters.AddWithValue("@dateStart", DateTime.Now);

				SqlDataReader reader=command.ExecuteReader();
				while (reader.Read()) {
					DateTime date=Convert.ToDateTime(reader[0]);
					int item=Convert.ToInt32(reader[1]);
					int value0=Convert.ToInt32(reader[2]);
					int ga=item % 10;
					ga = ga == 0 ? 10 : ga;

					if (value0 == 1) {
						Result.Add(ga, "0");
					}
					if (value0 == 0) {
						double diff=PuskStopRecord.getDiffMin(date,DateTime.Now.AddHours(-2));
						int days=(int)(diff / 60 / 24);
						int hours=(int)(diff % (60*24))/60;
						int minutes=(int)(diff - days * 60 * 24 - hours * 60);
						string res=String.Format(" {0}:{1:00}:{2:00} ", days, hours, minutes);
						Result.Add(ga,res);
					}
				}
			} catch (Exception e) {
				Logger.Error("Ошибка при получении пусков-остановов (подробно)");
				Logger.Error(e.ToString());
			} finally { try { con.Close(); } catch { } }

			for (int ga=1; ga <= 10;ga++ ) {
				if (!Result.ContainsKey(ga)) {
					Result.Add(ga, "-");
				}
				if (Result[ga].Length > 12) {
					Result[ga] = "-";
				}
			}
			return Result;
		}

	}
}
