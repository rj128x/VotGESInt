using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VotGES.Piramida.Report
{
	

	public class ReportSettings: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
	
		public class DateTimeStartEnd
		{
			public DateTime DateStart { get; set; }
			public DateTime DateEnd { get; set; }
			public String Title { get; set; }

			public static DateTimeStartEnd getFullDay(DateTime date) {
				DateTimeStartEnd result=new DateTimeStartEnd();
				result.DateStart = date.Date;
				result.DateEnd = date.Date.AddDays(1);
				result.Title = date.ToString("dd.MM.yyyy");
				return result;
			}

			public static DateTimeStartEnd getFullMonth(int year, int month) {
				DateTimeStartEnd result=new DateTimeStartEnd();				
				result.DateStart = new DateTime(year, month, 1);				
				result.DateEnd = result.DateStart.AddMonths(1);
				result.Title = String.Format("{0}-{1}",month,year);
				return result;
			}

			public static DateTimeStartEnd getFullQuarter(int year, int quarter) {
				DateTimeStartEnd result=new DateTimeStartEnd();
				result.DateStart = new DateTime(year, (quarter-1)*3+1, 1);
				result.DateEnd = result.DateStart.AddMonths(3);
				result.Title = String.Format("{0}кв. {1}", quarter, year);
				return result;
			}

			public static DateTimeStartEnd getFullYear(int year) {
				DateTimeStartEnd result=new DateTimeStartEnd();
				result.DateStart = new DateTime(year, 1, 1);
				result.DateEnd = result.DateStart.AddYears(1);
				result.Title = String.Format("{0}", year);
				return result;
			}

			public static DateTimeStartEnd getFullYears(int year,int yearEnd) {
				DateTimeStartEnd result=new DateTimeStartEnd();
				result.DateStart = new DateTime(year, 1, 1);
				result.DateEnd = new DateTime(yearEnd+1, 1, 1); ;
				result.Title = String.Format("{0}-{1}", year,yearEnd);
				return result;
			}

			public static DateTimeStartEnd getBySettings(ReportSettings settings) {
				switch (settings.reportType) {
					case ReportTypeEnum.dayByHalfHours:
					case ReportTypeEnum.dayByHours:
					case ReportTypeEnum.dayByMinutes:
					case ReportTypeEnum.day:
						return getFullDay(settings.Date);
					case ReportTypeEnum.monthByDays:
					case ReportTypeEnum.monthByHours:
					case ReportTypeEnum.monthByHalfHours:
					case ReportTypeEnum.month:
						return getFullMonth(settings.Year, settings.Month);
					case ReportTypeEnum.quarterByDays:
					case ReportTypeEnum.quarter:
						return getFullQuarter(settings.Year, settings.Quarter);
					case ReportTypeEnum.yearByHalfHours:
					case ReportTypeEnum.yearByHours:
					case ReportTypeEnum.yearByDays:
					case ReportTypeEnum.yearByMonths:
					case ReportTypeEnum.yearByQarters:
					case ReportTypeEnum.year:
						return getFullYear(settings.Year);
					case ReportTypeEnum.years:
						return getFullYears(settings.Year, settings.YearEnd);
				}
				return null;
			}
		}



		private Dictionary<ReportTypeEnum,string> reportTypeNames;
		public Dictionary<ReportTypeEnum, string> ReportTypeNames {
			get { return reportTypeNames; }
			set { reportTypeNames = value; }
		}

		private Dictionary<int,string> monthNames;
		public Dictionary<int, string> MonthNames {
			get { return monthNames; }
			set { monthNames = value; }
		}

		private Dictionary<int,string> quarterNames;
		public Dictionary<int, string> QuarterNames {
			get { return quarterNames; }
			set { quarterNames = value; }
		}

		private Dictionary<FullReportMembersType,string>mbTypeNames;
		public Dictionary<FullReportMembersType, string> MBTypeNames {
			get { return mbTypeNames; }
			set { mbTypeNames = value; }
		}
		

		private ReportTypeEnum reportType;
		public ReportTypeEnum ReportType {
			get { return reportType; }
			set { 
				reportType = value;
				switch (reportType) {
					case ReportTypeEnum.dayByMinutes:
					case ReportTypeEnum.dayByHalfHours:
					case ReportTypeEnum.dayByHours:
					case ReportTypeEnum.day:
						IsVisibleDate = true;
						IsVisibleMonth = false;
						IsVisibleQuarter = false;
						IsVisibleYear = false;
						IsVisibleYearEnd = false;						
						break;
					case ReportTypeEnum.monthByDays:
					case ReportTypeEnum.monthByHalfHours:
					case ReportTypeEnum.monthByHours:
					case ReportTypeEnum.month:
						IsVisibleDate = false;
						IsVisibleYear = true;
						IsVisibleYearEnd = false;
						IsVisibleMonth = true;
						IsVisibleQuarter = false;
						break;
					case ReportTypeEnum.quarterByDays:
					case ReportTypeEnum.quarter:
						IsVisibleDate = false;
						IsVisibleYear = true;
						IsVisibleYearEnd = false;
						IsVisibleMonth = false;
						IsVisibleQuarter = true;
						break;
					case ReportTypeEnum.yearByHalfHours:
					case ReportTypeEnum.yearByHours:
					case ReportTypeEnum.yearByDays:
					case ReportTypeEnum.yearByMonths:
					case ReportTypeEnum.yearByQarters:
					case ReportTypeEnum.year:
						IsVisibleDate = false;
						IsVisibleYear = true;
						IsVisibleYearEnd = false;
						IsVisibleMonth = false;
						IsVisibleQuarter = false;
						break;
					case ReportTypeEnum.years:
						IsVisibleDate = false;
						IsVisibleYear = true;
						IsVisibleYearEnd = true;
						IsVisibleMonth = false;
						IsVisibleQuarter = false;
						break;
				}
				if (HasCompareReport && ChildReports.Count>0) {
					foreach (ReportSettings child in ChildReports) {
						child.ReportType = reportType;
					}
				}
				NotifyChanged("ReportType");
			}
		}

		private FullReportMembersType mbType;
		public FullReportMembersType MBType {
			get { return mbType; }
			set {
				mbType = value;
				NotifyChanged("MBType");
			}
		}

		private int year;
		public int Year {
			get { return year; }
			set { 
				year = value;
				year=year<1960?DateTime.Now.Year:year;
				year = year > DateTime.Now.Year ? DateTime.Now.Year : year;

				NotifyChanged("Year");
			}
		}

		private int yearEnd;
		public int YearEnd {
			get { return yearEnd; }
			set {
				yearEnd = value;
				yearEnd = yearEnd < 1960 ? DateTime.Now.Year : yearEnd;
				yearEnd = yearEnd > DateTime.Now.Year ? DateTime.Now.Year : yearEnd;

				NotifyChanged("YearEnd");
			}
		}

		private int month;
		public int Month {
			get { return month; }
			set { 
				month = value;
				NotifyChanged("Month");
			}
		}

		private int quarter;
		public int Quarter {
			get { return quarter; }
			set {
				quarter = value;
				NotifyChanged("Quarter");
			}
		}

		private DateTime date;
		public DateTime Date {
			get { return date; }
			set { 
				date = value;
				Year = Date.Year;
				Month = Date.Month;
				NotifyChanged("Date");
			}
		}

		private bool isFullReport;

		public bool IsFullReport {
			get { return isFullReport; }
			set { 
				isFullReport = value;
				NotifyChanged("IsFullReport");
			}
		}

		private bool isVisibleDate;
		public bool IsVisibleDate {
			get { return isVisibleDate; }
			set { 
				isVisibleDate = value;
				NotifyChanged("IsVisibleDate");
			}
		}

		private bool isVisibleMonth;
		public bool IsVisibleMonth {
			get { return isVisibleMonth; }
			set { 
				isVisibleMonth = value;
				NotifyChanged("IsVisibleMonth");
			}
		}

		private bool isVisibleQuarter;
		public bool IsVisibleQuarter {
			get { return isVisibleQuarter; }
			set {
				isVisibleQuarter = value;
				NotifyChanged("IsVisibleQuarter");
			}
		}

		private bool isVisibleYear;
		public bool IsVisibleYear {
			get { return isVisibleYear; }
			set { 
				isVisibleYear = value;
				NotifyChanged("IsVisibleYear");
			}
		}

		private bool isVisibleYearEnd;
		public bool IsVisibleYearEnd {
			get { return isVisibleYearEnd; }
			set {
				isVisibleYearEnd = value;
				NotifyChanged("IsVisibleYearEnd");
			}
		}

		private bool hasCompareReport;
		public bool HasCompareReport {
			get { return hasCompareReport; }
			set {
				hasCompareReport = value;
				NotifyChanged("HasCompareReport");
			}
		}

		private bool isChildReport;
		public bool IsChildReport {
			get { return isChildReport; }
			set {
				isChildReport = value;
				ShowChartTable = !IsChildReport;
				NotifyChanged("IsChildReport");
			}
		}

		private bool isChart;
		public bool IsChart {
			get { return isChart; }
			set { 
				isChart = value;
				NotifyChanged("IsChart");
			}
		}

		private bool isTable;
		public bool IsTable {
			get { return isTable; }
			set {
				isTable = value;
				NotifyChanged("IsTable");
			}
		}

		private bool isExcel;
		public bool IsExcel {
			get { return isExcel; }
			set {
				isExcel = value;
				NotifyChanged("IsExcel");
	
			}
		}

		private bool showChartTable;
		public bool ShowChartTable {
			get { return showChartTable; }
			set {
				showChartTable = value;
				NotifyChanged("ShowChartTable");
			}
		}

		private List<ReportSettings> childReports;
		public List<ReportSettings> ChildReports {
			get { return childReports; }
			set { childReports = value; }
		}

		public void AddChildReport(ReportSettings child){
			if (!ChildReports.Contains(child)) {
				ChildReports.Add(child);
				child.ReportType = ReportType;
				child.ParentReport = this;
				child.IsChildReport = true;
				HasCompareReport = true;
			}
		}

		public void RemoveChildReport(ReportSettings child) {
			if (ChildReports.Contains(child)) {
				ChildReports.Remove(child);
				child.ParentReport = null;
				child.IsChildReport = false;
				HasCompareReport = ChildReports.Count>0;
				NotifyChanged("ChildReports");
			}
		}

		private ReportSettings parentReport;
		public ReportSettings ParentReport {
			get { return parentReport; }
			set { 
				parentReport = value; 
			}
		}
		
		public void init(bool onlyDates = false) {
			ReportTypeNames = new Dictionary<ReportTypeEnum, string>();
			MonthNames = new Dictionary<int, string>();
			QuarterNames = new Dictionary<int, string>();
			MBTypeNames = new Dictionary<FullReportMembersType, string>();
			IsFullReport = !onlyDates;
			if (!onlyDates) {
				ReportTypeNames.Add(ReportTypeEnum.dayByMinutes, "За сутки по минутам");
				ReportTypeNames.Add(ReportTypeEnum.dayByHalfHours, "За сутки по 30 минут");
				ReportTypeNames.Add(ReportTypeEnum.dayByHours, "За сутки по часам");
				ReportTypeNames.Add(ReportTypeEnum.monthByHalfHours, "За месяц по 30 минут");
				ReportTypeNames.Add(ReportTypeEnum.monthByHours, "За месяц по часам");
				ReportTypeNames.Add(ReportTypeEnum.monthByDays, "За месяц по дням");
				ReportTypeNames.Add(ReportTypeEnum.quarterByDays, "За квартал по дням");
				ReportTypeNames.Add(ReportTypeEnum.yearByHalfHours, "За год по 30 минут");
				ReportTypeNames.Add(ReportTypeEnum.yearByHours, "За год по часам");
				ReportTypeNames.Add(ReportTypeEnum.yearByDays, "За год по дням");
				ReportTypeNames.Add(ReportTypeEnum.yearByMonths, "За год по месяцам");
				ReportTypeNames.Add(ReportTypeEnum.yearByQarters, "За год по кварталам");
				ReportTypeNames.Add(ReportTypeEnum.years, "По годам");
				ShowChartTable = true;
			} else {
				ReportTypeNames.Add(ReportTypeEnum.day, "За сутки");
				ReportTypeNames.Add(ReportTypeEnum.month, "За месяц");
				ReportTypeNames.Add(ReportTypeEnum.quarter, "За квартал");
				ReportTypeNames.Add(ReportTypeEnum.year, "За год");
				ShowChartTable = false;
			}


			MonthNames.Add(1, "Январь");
			MonthNames.Add(2, "Февраль");
			MonthNames.Add(3, "Март");
			MonthNames.Add(4, "Апрель");
			MonthNames.Add(5, "Май");
			MonthNames.Add(6, "Июнь");
			MonthNames.Add(7, "Июль");
			MonthNames.Add(8, "Август");
			MonthNames.Add(9, "Сентябрь");
			MonthNames.Add(10, "Октябрь");
			MonthNames.Add(11, "Ноябрь");
			MonthNames.Add(12, "Декабрь");

			QuarterNames.Add(1, "1 Квартал");
			QuarterNames.Add(2, "2 Квартал");
			QuarterNames.Add(3, "3 Квартал");
			QuarterNames.Add(4, "4 Квартал");

			MBTypeNames.Add(FullReportMembersType.def, "По умолчанию");
			MBTypeNames.Add(FullReportMembersType.avg, "Среднее");
			MBTypeNames.Add(FullReportMembersType.min, "Минимум");
			MBTypeNames.Add(FullReportMembersType.max, "Максимум");
			MBTypeNames.Add(FullReportMembersType.eq, "Точно");

			Year = DateTime.Now.Year; 
			YearEnd = DateTime.Now.Year;
			Month = DateTime.Now.Month;
			Date = DateTime.Now.Date;
			Quarter = 1;
			MBType = FullReportMembersType.def;
			IsChart = true;
			IsTable = true;

			ChildReports = new List<ReportSettings>();

			ReportType = ReportTypeEnum.dayByHalfHours;
		}

		public ReportSettings(bool onlyDates=false) {
			init(onlyDates);

		}

	}
	
}
