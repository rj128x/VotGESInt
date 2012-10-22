
namespace VotGES.Web.Services
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;
	using VotGES.Piramida.Report;


	// TODO: создайте методы, содержащие собственную логику приложения.
	[EnableClientAccess()]
	public class ReportBaseDomainService : DomainService
	{
		public FullReportRoot GetFullReportRoot() {
			try {
				FullReportRoot root=new FullReportRoot();
				Logger.Info("Получение данных для полного отчета");
				FullReportInitData report=new FullReportInitData();
				root.RootMain = report.RootMain;
				root.RootLines = report.RootLines;
				root.RootSN = report.RootSN;
				return root;
			} catch (Exception e) {
				Logger.Error("Ошибка при получении данных для полного отчета "+e.ToString());
				return null;

			}
		}

		public ReportAnswer GetFullReport(List<string> selectedData, DateTime dateStart, DateTime dateEnd, ReportTypeEnum ReportType, 
			List<string> TitleList, List<DateTime>DateStartList, List<DateTime>DateEndList) {
			try {
				Logger.Info(String.Format("Получение отчета {0} - {1} [{2}]",dateStart,dateEnd,ReportType));
				FullReport report=new FullReport(dateStart, dateEnd, Report.GetInterval(ReportType));
				report.InitNeedData(selectedData);				
				report.ReadData();

				List<Report> reportAddList=null;
				if (TitleList.Count>0) {
					reportAddList = new List<Report>();
					for (int index=0; index < TitleList.Count; index++) {
						FullReport reportAdd = new FullReport(DateStartList[index], DateEndList[index], Report.GetInterval(ReportType));
						reportAdd.AddReportTitle = TitleList[index];
						reportAdd.InitNeedData(selectedData);						
						reportAdd.ReadData();
						reportAddList.Add(reportAdd);
					}
				}
				report.CreateAnswerData(reportAddList: reportAddList);
				report.CreateChart(reportAddList);
				Logger.Info("Отчет сформирован: "+report.Answer.Data.Count());
				return report.Answer;
			} catch (Exception e) {
				Logger.Error("Ошибка при получении отчета " + e.ToString());
				return null;
			}
		}

		public string GetSutVedReport(DateTime date) {
			try {
				Logger.Info("Получение суточной ведомости " + date);
				SutVedReport report=new SutVedReport(date.Date, date.Date.AddDays(1), IntervalReportEnum.hour);
				report.ReadData();
				Logger.Info("Суточная ведомость сформирована ");
				return "OK";
			} catch (Exception e) {
				Logger.Error("Ошибка при получении суточной ведомости " + e.ToString());
				return null;
			}
		}

	}
}


