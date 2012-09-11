using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using VotGES.Web.Services;
using System.ServiceModel.DomainServices.Client;

namespace MainSL.Views
{
	public partial class SutVedPage : Page
	{
		public SettingsBase settings;
		ReportBaseDomainContext context;

		public SutVedPage() {
			InitializeComponent();
			settings = new SettingsBase();
			settings.Date = DateTime.Now.Date.AddDays(-1);
			pnlSettings.DataContext = settings;
			context = new ReportBaseDomainContext();
		}

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
		}	

		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			GlobalStatus.Current.StopLoad();
		}

		private void btnGetReport_Click(object sender, RoutedEventArgs e) {
			string host=Application.Current.Host.Source.Host;
			int port=Application.Current.Host.Source.Port;
			Uri uri=new Uri(String.Format("http://{0}:{1}/Reports/SutVed?year={2}&month={3}&day={4}",host,port,
				settings.Date.Year,settings.Date.Month,settings.Date.Day));
			System.Windows.Browser.HtmlPopupWindowOptions options=new System.Windows.Browser.HtmlPopupWindowOptions();
			System.Windows.Browser.HtmlPage.PopupWindow(uri, "", options);
		}

		private void btnGetPBR_Click(object sender, RoutedEventArgs e) {
			string host=Application.Current.Host.Source.Host;
			int port=Application.Current.Host.Source.Port;
			Uri uri=new Uri(String.Format("http://{0}:{1}/Reports/PBR?year={2}&month={3}&day={4}", host, port,
				settings.Date.Year, settings.Date.Month, settings.Date.Day));
			System.Windows.Browser.HtmlPopupWindowOptions options=new System.Windows.Browser.HtmlPopupWindowOptions();
			System.Windows.Browser.HtmlPage.PopupWindow(uri, "", options);
		}



	}
}
