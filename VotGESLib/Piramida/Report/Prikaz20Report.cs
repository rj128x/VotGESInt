using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report
{
	public class Prikaz20Report
	{
		public DateTime Date { get; set; }
		public FullReport ReportDay { get; set; }
		public FullReport ReportMonth { get; set; }

		public Prikaz20Report(DateTime date) {
			this.Date = date;
			List<string> need=new List<string>();
			need.Add(ReportMainRecords.P_SP.ID);
			need.Add(PiramidaRecords.P_GES.Key);
			need.Add(PiramidaRecords.P_GA1_Otd.Key);
			need.Add(PiramidaRecords.P_GA2_Otd.Key);
			need.Add(PiramidaRecords.P_GA3_Otd.Key);
			need.Add(PiramidaRecords.P_GA4_Otd.Key);
			need.Add(PiramidaRecords.P_GA5_Otd.Key);
			need.Add(PiramidaRecords.P_GA6_Otd.Key);
			need.Add(PiramidaRecords.P_GA7_Otd.Key);
			need.Add(PiramidaRecords.P_GA8_Otd.Key);
			need.Add(PiramidaRecords.P_GA9_Otd.Key);
			need.Add(PiramidaRecords.P_GA10_Otd.Key);
			need.Add(ReportLinesRecords.P_VL110_Saldo.ID);
			need.Add(ReportLinesRecords.P_VL220_Saldo.ID);
			need.Add(ReportLinesRecords.P_VL500_Saldo.ID);
			need.Add(ReportLinesRecords.P_KL6_Saldo.ID);
			ReportDay=new FullReport(date.Date, date.Date.AddDays(1), IntervalReportEnum.halfHour);
			ReportDay.InitNeedData(need);
			ReportMonth = new FullReport(new DateTime(Date.Year,Date.Month,1).Date, Date.Date.AddDays(1), IntervalReportEnum.halfHour);
			ReportMonth.InitNeedData(need);
			ReportDay.ReadData();
			ReportMonth.ReadData();
		}
		
	}
}
