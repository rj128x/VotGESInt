
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
	}
}


