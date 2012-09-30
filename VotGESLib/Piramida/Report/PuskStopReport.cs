using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace VotGES.Piramida.Report
{
	public class PuskStopRecord
	{
		public int GA { get; set; }
		public int CountPusk { get; set; }
		public int CountStop { get; set; }
		public int CountPuskGen { get; set; }
		public int CountPuskSK { get; set; }
		public double HoursWork { get; set; }
		public double HoursStay { get; set; }
		public double HoursGen { get; set; }
		public double HoursSK { get; set; }

		public double MinGen1 { get; set; }
		public double MinGen0 { get; set; }
		public double MinSK1 { get; set; }
		public double MinSK0 { get; set; }
		public double MinRun1 { get; set; }
		public double MinRun0 { get; set; }

		public DateTime FirstRun { get; set; }
		public DateTime FirstStop { get; set; }
		public DateTime LastRun { get; set; }
		public DateTime LastStop { get; set; }

		public DateTime FirstRunGen { get; set; }
		public DateTime FirstStopGen { get; set; }
		public DateTime LastRunGen { get; set; }
		public DateTime LastStopGen { get; set; }
		
		public DateTime FirstRunSK { get; set; }
		public DateTime FirstStopSK { get; set; }
		public DateTime LastRunSK { get; set; }
		public DateTime LastStopSK { get; set; }

		public PuskStopRecord() {
			FirstRun = DateTime.MinValue;
			FirstStop = DateTime.MinValue;
			LastRun = DateTime.MaxValue;
			LastStop = DateTime.MaxValue;

			FirstRunGen = DateTime.MinValue;
			FirstStopGen = DateTime.MinValue;
			LastRunGen = DateTime.MaxValue;
			LastStopGen = DateTime.MaxValue;

			FirstRunSK = DateTime.MinValue;
			FirstStopSK = DateTime.MinValue;
			LastRunSK = DateTime.MaxValue;
			LastStopSK = DateTime.MaxValue;
		}

		public static double getDiffMin(DateTime start, DateTime end){
			if (end>start){
				return (end.Ticks - start.Ticks) / (10000000.0 * 60.0);
			}
			return 0;
		}

		protected double processDates(DateTime DateStart, DateTime DateEnd, DateTime firstRun, DateTime lastRun, DateTime firstStop, DateTime lastStop, double min0, double min1) {
			double result=0;
			double wrk=min0;			
			double stp=min1;
			double wrkDiff;
			double stpDiff;

			if (lastRun < lastStop) {
				stp += getDiffMin(lastStop, DateEnd);
			}else if (lastStop>lastRun){
				wrk+=getDiffMin(LastRun,DateEnd);
			}

			wrkDiff = getDiffMin(DateStart, DateEnd) - stp;
			stpDiff = getDiffMin(DateStart, DateEnd) - wrk;

			if (firstRun < firstStop) {
				result = wrk;
			} else {
				result = wrkDiff;
			}
			
			return result;
		}
		

		public void ProcessData(DateTime DateStart, DateTime DateEnd) {
			HoursWork = processDates(DateStart, DateEnd, FirstRun, LastRun, FirstStop, LastStop, MinRun0, MinRun1);
			HoursStay = getDiffMin(DateStart, DateEnd)  - HoursWork;
			HoursSK = processDates(DateStart, DateEnd, FirstRunSK, LastRunSK, FirstStopSK, LastStopSK, MinSK0, MinSK1);
			HoursGen = processDates(DateStart, DateEnd, FirstRunGen, LastRunGen, FirstStopGen, LastStopGen, MinGen0, MinGen1);			
		}

		protected string getStrHours(double hours) {
			return String.Format("{0}:{1}", (int)(hours / 60), (int)(hours % 60)); 
		}

		public string HoursWorkStr {
			get { return getStrHours(HoursWork); }
		}

		public string HoursStayStr {
			get { return getStrHours(HoursStay); }
		}

		public string HoursSKStr {
			get { return getStrHours(HoursSK); }
		}

		public string HoursGenStr {
			get { return getStrHours(HoursGen); }
		}
	}

	public class PuskStopReport
	{
		public DateTime DateStart { get; protected set; }
		public DateTime DateEnd { get; protected set; }
		public SortedList<int, PuskStopRecord> Data { get; protected set; }
		public PuskStopRecord SumRecord { get; protected set; }

		public PuskStopReport(DateTime DateStart, DateTime DateEnd) {
			this.DateEnd = DateEnd;
			this.DateStart = DateStart;
			
		}
		
		public void ReadData() {
			this.Data=new SortedList<int,PuskStopRecord>();
			for (int ga=1;ga<=10;ga++){
				PuskStopRecord rec=new PuskStopRecord();
				rec.GA=ga;
				Data.Add(ga,rec);
			}
			SumRecord = new PuskStopRecord();
					

			SqlConnection con=null;			
			try {				
				string sel=String.Format("SELECT item, Value0, COUNT(VALUE0), sum(value1), min(data_date), max(data_date) FROM DATA WHERE Parnumber=13 and object=30 and objtype=2 and item>=1 and item<=30 and data_date>=@dateStart and data_date<=@dateEnd group by item, value0");
				con = PiramidaAccess.getConnection("PSV");
				con.Open();
				SqlCommand command=con.CreateCommand();
				command.CommandText = sel;
				Logger.Info(sel);
				command.Parameters.AddWithValue("@dateStart", DateStart);
				command.Parameters.AddWithValue("@dateEnd", DateEnd);

				SqlDataReader reader=command.ExecuteReader();
				while (reader.Read()) {
					int item=Convert.ToInt32(reader[0]);
					int value0=Convert.ToInt32(reader[1]);
					int cnt=Convert.ToInt32(reader[2]);
					double hours=Convert.ToDouble(reader[3]);
					DateTime minDate=Convert.ToDateTime(reader[4]);
					DateTime maxDate=Convert.ToDateTime(reader[5]);
										
					int ga=item % 10;
					ga = ga == 0 ? 10 : ga;
					
					if (item <= 10) {
						if (value0 == 1) {
							Data[ga].CountPusk = cnt;
							Data[ga].MinRun1 = hours;
							Data[ga].FirstRun = minDate;
							Data[ga].LastRun = maxDate;
						}
						if (value0 == 0) {
							Data[ga].CountStop = cnt;
							Data[ga].MinRun0 = hours;
							Data[ga].FirstStop = minDate;
							Data[ga].LastStop = maxDate;
						}
					}else	if (item <= 20) {
						if (value0 == 1) {
							Data[ga].CountPuskSK = cnt;
							Data[ga].MinSK1 = hours;
							Data[ga].FirstRunSK = minDate;
							Data[ga].LastRunSK = maxDate;
						}
						if (value0 == 0) {
							Data[ga].MinSK0 = hours;
							Data[ga].FirstStopSK = minDate;
							Data[ga].LastStopSK = maxDate;
						}
					}else	if (item <= 30) {
						if (value0 == 1) {
							Data[ga].CountPuskGen = cnt;
							Data[ga].MinGen1 = cnt;
							Data[ga].FirstRunGen = minDate;
							Data[ga].LastRunGen = maxDate;							
						}
						if (value0 == 0) {
							Data[ga].MinGen0 = cnt;
							Data[ga].FirstStopGen = minDate;
							Data[ga].LastStopGen = maxDate;	
						}
					}
				}
			} catch (Exception e) {
				Logger.Error("Ошибка при получении пусков-остановов");
				Logger.Error(e.ToString());
			} finally { try { con.Close(); } catch { } }


			double FullMinutes=(DateEnd.Ticks - DateStart.Ticks) / (10000000.0 * 60.0);
			for (int ga=1; ga <= 10; ga++) {
				SumRecord.CountPusk += Data[ga].CountPusk;
				SumRecord.CountStop += Data[ga].CountStop;
				SumRecord.CountPuskSK += Data[ga].CountPuskSK;
				SumRecord.CountPuskGen += Data[ga].CountPuskGen;
				Data[ga].ProcessData(DateStart,DateEnd);
			}

		}
	}
}
