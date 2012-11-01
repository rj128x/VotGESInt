using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotGES.Piramida.Report;
using VotGES.PBR;
using VotGES.Piramida;
using System.IO;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using System.Threading;

namespace VotGES.Web.Controllers
{
	public class ReportsController : Controller
	{
		//
		// GET: /Reports/

		public ActionResult Index() {
			return View();
		}


		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult SutVed(int year, int month, int day, bool isUral=true) {
			Logger.Info(String.Format("Суточная ведомость за {0}.{1}.{2}", day, month, year));
			DateTime date=new DateTime(year, month, day);
			int h=isUral ? 2 : 0;
			SutVedReport report=new SutVedReport(date.Date.AddHours(-h), date.Date.AddDays(1).AddHours(-h), IntervalReportEnum.halfHour);
			report.ReadData();
			report.AddHours = h;

			StringWriter writer=new StringWriter();

			ViewResult view=View("SutVed", report);
			return view;
		}
		
			

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult PBR(int year, int month, int day) {
			Logger.Info(String.Format("ПБР за {0}.{1}.{2}", day, month, year));
			DateTime date=new DateTime(year, month, day);
			PBRFull pbr=new PBRFull(date);
			return View("PBR", pbr);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Prikaz20(int year, int month, int day) {
			Logger.Info(String.Format("Приказ 20 за {0}.{1}.{2}", day, month, year));
			DateTime date=new DateTime(year, month, day);
			Prikaz20Report report=new Prikaz20Report(date);

			return View("Prikaz20", report);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult PuskStop(int year1, int month1, int day1, int year2, int month2, int day2) {
			DateTime dateStart=new DateTime(year1, month1, day1);
			DateTime dateEnd=new DateTime(year2, month2, day2);
			dateEnd = dateEnd > DateTime.Now.AddHours(-2) ? DateTime.Now.AddHours(-2) : dateEnd;
			Logger.Info(String.Format("Пуски-остановы с {0} по {1}", dateStart,dateEnd));
			PuskStopReport puskStop=new PuskStopReport(dateStart, dateEnd);
			puskStop.ReadData();
			
			return View("PuskStop", puskStop);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult PuskStopFull(int year1, int month1, int day1, int year2, int month2, int day2) {
			DateTime dateStart=new DateTime(year1, month1, day1);
			DateTime dateEnd=new DateTime(year2, month2, day2);
			dateEnd = dateEnd > DateTime.Now.AddHours(-2) ? DateTime.Now.AddHours(-2) : dateEnd;
			Logger.Info(String.Format("Пуски-остановы (подробно) с {0} по {1}", dateStart, dateEnd));
			PuskStopReportFull puskStop=new PuskStopReportFull(dateStart, dateEnd);
			puskStop.ReadData();

			return View("PuskStopFull", puskStop);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult FullReport(string guid) {
			ReportAnswer report=null;
			Guid ID=Guid.Parse(guid);
			while (report==null) {
				if (Reports.CreatedReports.ContainsKey(ID)) {					
					report = Reports.CreatedReports[ID];
					Reports.removeReport(ID);
				} else {
					Thread.Sleep(TimeSpan.FromSeconds(2));
				}
			}
			return View("FullReport", report);
		}

	}
}
