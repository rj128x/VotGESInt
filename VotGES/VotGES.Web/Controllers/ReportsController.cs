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
		public ActionResult SutVedPDF(int year, int month, int day) {
			Logger.Info(String.Format("Суточная ведомость за {0}.{1}.{2}", day, month, year));
			DateTime date=new DateTime(year, month, day);
			SutVedReport report=new SutVedReport(date.Date, date.Date.AddDays(1), IntervalReportEnum.halfHour);
			report.ReadData();

			StringWriter writer=new StringWriter();
						
			ViewResult view=View("SutVedPart", report);
			view.ExecuteResult(this.ControllerContext);
			view.View.Render(new ViewContext(this.ControllerContext,view.View,this.ViewData,this.TempData, writer),writer);
			string html=writer.GetStringBuilder().ToString();
			writer.Close();
			
			MemoryStream outPDF=new MemoryStream();
			Document document = new Document(PageSize.A4_LANDSCAPE, 0, 0, 0, 0);
			PdfWriter.GetInstance(document, outPDF);
			document.Open();

			//html = "<h1>dsgdfg</h1>";
			List<IElement> parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(html), new StyleSheet());
						

			foreach (IElement htmlElement in parsedHtmlElements)
				document.Add(htmlElement as IElement); ;
			document.Close();
			//return view;

			System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
			response.Clear();
			response.ContentType = "application/pdf";
			response.AddHeader("Content-Disposition", string.Format("attachment;filename=sutVed.pdf;size={0}",outPDF.ToArray().Length));
			response.Flush();
			response.BinaryWrite(outPDF.ToArray());
			response.Flush();
			response.End();
			return null;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult SutVed(int year, int month, int day) {
			Logger.Info(String.Format("Суточная ведомость за {0}.{1}.{2}", day, month, year));
			DateTime date=new DateTime(year, month, day);
			SutVedReport report=new SutVedReport(date.Date, date.Date.AddDays(1), IntervalReportEnum.halfHour);
			report.ReadData();

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

	}
}
