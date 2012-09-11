using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotGES.Piramida.Report;
using VotGES.PBR;

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
		public ActionResult SutVed(int year, int month, int day) {
			DateTime date=new DateTime(year, month, day);
			SutVedReport report=new SutVedReport(date.Date, date.Date.AddDays(1), IntervalReportEnum.halfHour);
			report.ReadData();

			return View("SutVed", report);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult PBR(int year, int month, int day) {
			DateTime date=new DateTime(year, month, day);
			PBRFull pbr=new PBRFull(date);
			return View("PBR", pbr);
		}

	}
}
