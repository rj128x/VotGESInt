using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using VotGES.Web.Logging;
using VotGES.Piramida;

namespace VotGES.Web
{
	// Примечание: Инструкции по включению классического режима IIS6 или IIS7 
	// см. по ссылке http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				 "Default", // Имя маршрута
				 "{controller}/{action}/{id}", // URL-адрес с параметрами
				 new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Параметры по умолчанию
			);

		}
			

		protected void Application_Start() {
			DBSettings.init(Server.MapPath("/bin/Data/DBSettings.xml"));
			VotGES.GlobalVotGES.setCulture();

			Logger logger=new WebLogger();
			Logger.InitFileLogger(Server.MapPath("/logs/"), "orders", new Web.Logging.WebLogger());
			Logger.Info("Старт приложения "+DateTime.Now);			
			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);
		}

		
	}
}