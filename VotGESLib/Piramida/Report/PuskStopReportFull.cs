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

			
		}
		public DateTime Date { get; set; }
		public SortedList<int,EventGA> Data;

		public PuskStopEvent() {			
			Data = new SortedList<int, EventGA>();
		}

		public static string getValue(bool? valStart, bool? valStop, string strStart, string strStop, string strNone) {
			string str=strNone;
			if (!valStart.HasValue && !valStart.HasValue) {
				return str;
			}
			if (valStart.Value) {
				str = strStart;
			} if (valStart.Value) {
				str = strNone;
			}
			return str;
		}

		public static void AddData(SortedList<DateTime,PuskStopEvent> DataArray,DateTime date, int ga, PuskStopState state){
			PuskStopEvent ev;
			if (!DataArray.ContainsKey(date)){
				ev=new PuskStopEvent();
				ev.Date = date;
				for (int g=1;g<=10;g++){
					EventGA evGA=new EventGA();
					evGA.GA = g;
					ev.Data.Add(g,evGA);
				}
				DataArray.Add(date,ev);
			}
			ev=DataArray[date];
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
							PuskStopEvent.AddData(Data, date, ga, PuskStopState.start);
						}
						if (value0 == 0) {
							PuskStopEvent.AddData(Data, date, ga, PuskStopState.stop);
						}
					} else if (item <= 20) {
						if (value0 == 1) {
							PuskStopEvent.AddData(Data, date, ga, PuskStopState.startGR);
						}
						if (value0 == 0) {
							PuskStopEvent.AddData(Data, date, ga, PuskStopState.stopGR);
						}
					} else if (item <= 30) {
						if (value0 == 1) {
							PuskStopEvent.AddData(Data, date, ga, PuskStopState.startSK);
						}
						if (value0 == 0) {
							PuskStopEvent.AddData(Data, date, ga, PuskStopState.stopSK);
						}
					}
				}
			} catch (Exception e) {
				Logger.Error("Ошибка при получении пусков-остановов (подробно)");
				Logger.Error(e.ToString());
			} finally { try { con.Close(); } catch { } }
		}
	}
}
