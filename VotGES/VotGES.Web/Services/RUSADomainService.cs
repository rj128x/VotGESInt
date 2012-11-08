
namespace VotGES.Web.Services
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;
	using VotGES.Web.Models;
	using VotGES.Web.Logging;
	using VotGES.Piramida.Report;
using VotGES.Chart;
using VotGES.Rashod;


	// TODO: создайте методы, содержащие собственную логику приложения.
	[EnableClientAccess()]
	public class RUSADomainService : DomainService
	{
		public RUSAData processRUSAData(RUSAData data) {
			WebLogger.Info("RUSA process", VotGES.Logger.LoggerSource.service);
			data.Result = new List<RUSAResult>();
			ProcessRUSAData.processEqualData(data);
			ProcessRUSAData.processDiffData(data);
			foreach (RUSAResult result in data.EqResult) {
				data.Result.Add(result);
			}
			foreach (RUSAResult result in data.DiffResult) {
				data.Result.Add(result);
			}
			return data;
		}

		public RashodHarsData processRashodHarsData(RashodHarsData data, bool calcRashod) {
			WebLogger.Info("RashodHars process", VotGES.Logger.LoggerSource.service);
			data.ProcessData(calcRashod);
			return data;
		}

		public RashodHarsData processMaket(RashodHarsData data) {
			WebLogger.Info("Maket process", VotGES.Logger.LoggerSource.service);
			data.ProcessMaket();
			return data;
		}

		public Dictionary<int, string> getStopGA() {
			WebLogger.Info("Get Pusk Stop GA", VotGES.Logger.LoggerSource.service);
			return PuskStopReportFull.TimeStopGA();
		}

		public ChartAnswer getChart(RashodHarsData data, RHChartType type) {
			WebLogger.Info("RashodHars Chart process "+type.ToString(), VotGES.Logger.LoggerSource.service);
			switch (type) {
				case RHChartType.GA_QotP:
					return RashodHars.GetGA_QotP(data.GANumber, false, data.Napor);
				case RHChartType.GA_KPDotP:
					return RashodHars.GetGA_QotP(data.GANumber, true, data.Napor);
				case RHChartType.GA_QotH:
					return RashodHars.GetGA_QotH(data.GANumber, false, data.Power);
				case RHChartType.GA_KPDotH:
					return RashodHars.GetGA_QotH(data.GANumber, true, data.Power);
				case RHChartType.CMPGA_QotP:
					return RashodHars.GetCMPGA_QotP(new int[]{1,2,3,4,5,6,7,8,9,10}, false, data.Napor);
				case RHChartType.CMPGA_KPDotP:
					return RashodHars.GetCMPGA_QotP(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, true, data.Napor);
				case RHChartType.CMPGA_QotH:
					return RashodHars.GetCMPGA_QotH(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, false, data.Power);
				case RHChartType.CMPGA_KPDotH:
					return RashodHars.GetCMPGA_QotH(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, true, data.Power);
				case RHChartType.CMPST_QotP:
					return RashodHars.GetCMPST_QotP(false, data.Napor);
				case RHChartType.CMPST_KPDotP:
					return RashodHars.GetCMPST_QotP(true, data.Napor);
				case RHChartType.CMPST_QotH:
					return RashodHars.GetCMPST_QotH(false, data.Power);
				case RHChartType.CMPST_KPDotH:
					return RashodHars.GetCMPST_QotH(true, data.Power);	
			}
			return null;
		}
	}
}


