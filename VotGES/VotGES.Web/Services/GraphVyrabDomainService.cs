
namespace VotGES.Web.Services
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;
	using VotGES.PBR;
	using VotGES.Piramida.Report;
	using VotGES.Piramida;

	public class FullGraphVyrab
	{
		public GraphVyrabAnswer GTP { get; set; }
		public GraphVyrabRGEAnswer RGE { get; set; }
		public Dictionary<int, string> TimeStopGA { get; set; }
		public double Napor { get; set; }
		public FullGraphVyrab(){
			Napor = 21;
		}
	}

	// TODO: создайте методы, содержащие собственную логику приложения.
	[EnableClientAccess()]
	public class GraphVyrabDomainService : DomainService
	{
		public GraphVyrabAnswer getGraphVyrab() {
			try {
				Logger.Info("Получение графика нагрузки");
				DateTime date=DateTime.Now.AddHours(-2);
				return GraphVyrab.getAnswer(date, true);
			} catch (Exception e) {
				Logger.Error("Ошибка при получении графика нагрузки " + e);
				return null;
			}
		}

		public GraphVyrabAnswer getGraphVyrabMin(DateTime date) {
			try {
				Logger.Info("Получение факта нагрузки по минутам"+date.ToString());
				date = date.Date;
				return GraphVyrab.getAnswer(date, false);
			} catch (Exception e) {
				Logger.Error("Ошибка при получении факта нагрузки " + e);
				return null;
			}
		}

		public CheckGraphVyrabAnswer getGraphVyrabHH(DateTime date) {
			try {
				Logger.Info("Получение факта нагрузки по получасовкам" + date.ToString());
				date = date.Date;
				return GraphVyrab.getAnswerHH(date);
			} catch (Exception e) {
				Logger.Error("Ошибка при получении факта нагрузки " + e);
				return null;
			}
		}

		public GraphVyrabRGEAnswer getGraphVyrabRGE() {
			try {
				Logger.Info("Получение графика нагрузки РГЕ");
				DateTime date=DateTime.Now.AddHours(-2);
				return GraphVyrabRGE.getAnswer(date, true);
			} catch (Exception e) {
				Logger.Error("Ошибка при получении графика нагрузки РГЕ" + e);
				return null;
			}
		}

		public GraphVyrabRGEAnswer getGraphVyrabRGEMin(DateTime date) {
			try {
				Logger.Info("Получение факта нагрузки по минутам РГЕ" + date.ToString());
				date = date.Date;
				return GraphVyrabRGE.getAnswer(date, false);
			} catch (Exception e) {
				Logger.Error("Ошибка при получении факта нагрузки РГЕ" + e);
				return null;
			}
		}

		public CheckGraphVyrabRGEAnswer getGraphVyrabRGEHH(DateTime date) {
			try {
				Logger.Info("Получение факта нагрузки по получасовкам РГЕ" + date.ToString());
				date = date.Date;
				return GraphVyrabRGE.getAnswerHH(date);
			} catch (Exception e) {
				Logger.Error("Ошибка при получении факта нагрузки РГЕ" + e);
				return null;
			}
		}

		public FullGraphVyrab getFullGraphVyrab() {
			FullGraphVyrab answer=new FullGraphVyrab();
			answer.GTP = getGraphVyrab();
			answer.RGE = getGraphVyrabRGE();			
			answer.TimeStopGA = PuskStopReportFull.TimeStopGA();
			try {
				List<PiramidaEnrty>list=PiramidaAccess.GetDataFromDB(DateTime.Now.AddHours(-4), DateTime.Now.AddHours(-2), 1, 2, 12, (new int[] { 276 }).ToList(), true, true, "P3000");
				answer.Napor = list.Last().Value0;
			} catch { }
			
			return answer;
		}
	}
}
