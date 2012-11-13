using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;

namespace VotGES.PBR
{
	public enum GTPEnum { gtp1 = 2, gtp2 = 3, ges = 1, rge4 = 1046, rge2 = 1703, rge3 = 1704, rge1=1045 }
	public class PBRData
	{
		public DateTime DateStart { get; protected set; }
		public DateTime DateEnd { get; protected set; }
		public DateTime Date { get; protected set; }

		public GTPEnum GTPIndex { get; protected set; }

		public SortedList<DateTime, double> RealPBR { get; protected set; }
		public SortedList<DateTime, double> SteppedPBR { get; protected set; }
		public SortedList<DateTime, bool> ChangePBR { get; protected set; }
		public SortedList<DateTime, double> MinutesPBR { get; protected set; }
		public SortedList<DateTime, double> RealP { get; protected set; }

		public SortedList<DateTime, double> IntegratedP { get; protected set; }
		public SortedList<DateTime, double> IntegratedPBR { get; protected set; }
		public bool IsSteppedPBR { get; set; }



		public PBRData(DateTime dateStart, DateTime dateEnd, DateTime date, GTPEnum gtp) {
			DateStart = dateStart.Date.AddHours(dateStart.Hour);
			DateEnd = dateEnd.Date.AddHours(dateEnd.Hour);
			Date = date;
			GTPIndex = gtp;
			RealPBR = new SortedList<DateTime, double>();
			SteppedPBR = new SortedList<DateTime, double>();
			MinutesPBR = new SortedList<DateTime, double>();
			ChangePBR = new SortedList<DateTime, bool>();
			RealP = new SortedList<DateTime, double>();
			IntegratedP = new SortedList<DateTime, double>();
			IntegratedPBR = new SortedList<DateTime, double>();
		}

		public void readData(bool readFakt) {
			int item=(int)GTPIndex;

			List<int> items=(new int[] { item }).ToList<int>();
			List<PiramidaEnrty> dataPBR=PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 0, 2, 212, items, true, true, "P3000");
			foreach (PiramidaEnrty data in dataPBR) {
				try {
					DateTime date=data.Date;
					double val=data.Value0 / 1000;
					if (date.Minute == 0) {
						RealPBR.Add(date, val);
					}
				} catch (Exception e) {
					Logger.Info("Ошибка при чтении ПБР " + e.ToString());
				}
			}


			if (!readFakt) {
				return;
			}
			List<PiramidaEnrty> dataFakt=PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 0, 2, 4, items, true, true, "PMin");
			foreach (PiramidaEnrty data in dataFakt) {
				try {
					DateTime date=data.Date;
					double val=data.Value0 / 1000;
					if (date > DateStart) {
						RealP.Add(date, val);
						Date = date;
					}
				} catch (Exception e) {
					Logger.Info("Ошибка при чтении Факт " + e.ToString());
				}
			}
		}


		public void checkData() {
			DateTime date=DateStart.AddMinutes(1);
			try {
				while (date <= Date) {
					if (!RealP.Keys.Contains(date)) {
						if (RealP.Keys.Contains(date.AddMinutes(-1))) {
							RealP.Add(date, RealP[date.AddMinutes(-1)]);
						} else {
							RealP.Add(date, -1);
						}
					}
					date = date.AddMinutes(1);
				}
			} catch { }

			try {
				date = DateStart.AddMinutes(0);
				while (date <= DateEnd) {
					if (!RealPBR.Keys.Contains(date)) {
						if (RealPBR.Keys.Contains(date.AddMinutes(-60))) {
							RealPBR.Add(date, RealPBR[date.AddMinutes(-60)]);
						} else {
							RealPBR.Add(date, -1);
							Logger.Info("Не записан ПБР " + GTPIndex.ToString() + " " + date.ToString());
						}
					}
					date = date.AddMinutes(60);
				}
			} catch { }

			try {
				date = DateStart.AddMinutes(30);
				while (date <= DateEnd) {
					DateTime prevDate=date.AddMinutes(-30);
					DateTime nextDate=date.AddMinutes(30);
					if (RealPBR.Keys.Contains(prevDate) && (RealPBR.Keys.Contains(nextDate))) {
						RealPBR.Add(date, (RealPBR[prevDate] + RealPBR[nextDate]) / 2);
					} else {
						RealPBR.Add(date, -1);
						Logger.Info("Ошибка при формировании получасовок");
					}
					date = date.AddMinutes(60);
				}
			} catch { }

		}

		public void createSteppedPBR() {
			DateTime LastPBRDate=RealPBR.Last().Key;
			DateTime FirstPBRDate=RealPBR.First().Key;
			foreach (KeyValuePair<DateTime, double> de in RealPBR) {
				DateTime date=de.Key.AddMinutes(-15);
				if (de.Key == FirstPBRDate)
					date = DateStart;

				SteppedPBR.Add(date, de.Value);
				if (de.Key == LastPBRDate) {
					SteppedPBR.Add(DateEnd, de.Value);
				}
			}

		}

		public void createMinutesPBR() {
			DateTime date=DateStart;
			double val=0;
			Random r=new Random();
			while (date < DateEnd) {
				if (SteppedPBR.Keys.Contains(date)) {
					val = SteppedPBR[date];
				}
				MinutesPBR.Add(date.AddMinutes(1), val);
				//RealP[date.AddMinutes(1)] = val + r.Next(-3, 3);
				date = date.AddMinutes(1);
			}
		}

		protected static double getDiffMin(DateTime start, DateTime end) {
			double res=0;
			res = (end.Ticks - start.Ticks) / (10000000.0 * 60.0);
			res = res > 0 ? res : 0;
			return res;
		}

		public void createMinutesPBRNotStair() {
			DateTime date=DateStart;
			double val=0;
			while (date < DateEnd) {
				DateTime prevDate=RealPBR.Last(de => de.Key <= date).Key;
				DateTime nextDate=RealPBR.First(de => de.Key >= date).Key;
				double prevVal=RealPBR[prevDate];
				double nextVal=RealPBR[nextDate];
				if (prevDate == date) {
					val = prevVal;
				} else if (nextDate == date) {
					val = nextVal;
				} else {
					double diff=getDiffMin(prevDate, nextDate);
					double diffDT=getDiffMin(prevDate, date);
					val = prevVal + (nextVal-prevVal) / diff * diffDT;
				}

				MinutesPBR.Add(date.AddMinutes(1), val);
				date = date.AddMinutes(1);
			}
		}

		public void createIntegratedValues() {
			double sum=0;
			foreach (KeyValuePair<DateTime,double> de in MinutesPBR) {
				sum += de.Value;
				IntegratedPBR.Add(de.Key, sum / 60);
			}

			sum = 0;
			foreach (KeyValuePair<DateTime,double> de in RealP) {
				sum += de.Value;
				IntegratedP.Add(de.Key, sum / 60);
			}
		}

		public double getDiff(DateTime date) {
			return RealP[date] - MinutesPBR[date];
		}

		public static double getDiffProc(double fakt, double plan) {
			if (plan > 0) {
				return (fakt - plan) / plan * 100;
			} else {
				if (fakt == 0)
					return 0;
				else
					return 100;
			}
		}

		public static double getDiffProcDiff(double diff, double plan) {
			if (plan > 0) {
				return (diff) / plan * 100;
			} else {
				if (diff == 0)
					return 0;
				else
					return 100;
			}
		}

		public double getDiffProc(DateTime date) {
			return getDiffProc(RealP[date], MinutesPBR[date]);
		}

		public static double getAvgHour(DateTime date, SortedList<DateTime, double> data) {
			date = date.AddMinutes(-1);
			DateTime ds=date.Date.AddHours(date.Hour);
			DateTime de=date.Date.AddHours(date.Hour + 1);
			DateTime dt=ds.AddMinutes(1);

			de = de > date ? date : de;

			double sum=0;
			double count=0;
			while (dt <= de) {
				sum += data[dt];
				count++;
				dt = dt.AddMinutes(1);
			}
			return sum / count;
		}

		public SortedList<string, double> getHourVals(DateTime date) {
			SortedList<string,double> result=new SortedList<string, double>();
			double fakt=getAvgHour(date, RealP);
			double plan=getAvgHour(date, MinutesPBR);
			double diff=fakt - plan;
			double diffProc=getDiffProc(fakt, plan);

			DateTime dt=date.AddMinutes(1);
			double sum=0;
			int count=0;
			while (dt.Hour == date.Hour) {
				sum += MinutesPBR[dt];
				count++;
				dt = dt.AddMinutes(1);
			}

			double recP=(sum - diff * date.Minute) / count;

			result.Add("fakt", fakt);
			result.Add("plan", plan);
			result.Add("diff", diff);
			result.Add("diffProc", diffProc);
			result.Add("recP", recP);
			return result;

		}

		public void InitData(bool readFakt=true) {
			readData(readFakt);
			checkData();
			createSteppedPBR();
			if (IsSteppedPBR) {
				createMinutesPBR();
			} else {
				createMinutesPBRNotStair();
			}
			
			createIntegratedValues();
			DateTime dt=SteppedPBR.Keys.First();
			foreach (DateTime date in SteppedPBR.Keys) {
				bool change=SteppedPBR[date] != SteppedPBR[dt];
				ChangePBR.Add(date, change);
				dt = date;
			}
		}

	}
}

