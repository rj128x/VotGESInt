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
using VotGES.Piramida.Report;

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
			SettingsControl.InitOnlyDates();
			SettingsControl.Settings.ReportType = ReportTypeEnum.day;
			
		}
		
		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {

		}	

		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			GlobalStatus.Current.StopLoad();
		}

		

		private void btnGetReport_Click(object sender, RoutedEventArgs e) {			
			string uri=String.Format("Reports/SutVed?year={0}&month={1}&day={2}",settings.Date.Year,settings.Date.Month,settings.Date.Day);
			FloatWindow.OpenWindow(uri);
		}

		private void btnGetPBR_Click(object sender, RoutedEventArgs e) {
			string uri=String.Format("Reports/PBR?year={0}&month={1}&day={2}", settings.Date.Year, settings.Date.Month, settings.Date.Day);
			FloatWindow.OpenWindow(uri);
		}

		private void btnPrikaz20_Click(object sender, RoutedEventArgs e) {
			string uri=String.Format("Reports/Prikaz20?year={0}&month={1}&day={2}", settings.Date.Year, settings.Date.Month, settings.Date.Day);
			FloatWindow.OpenWindow(uri);
		}

		private void btnGetPuskStop_Click(object sender, RoutedEventArgs e) {
			ReportSettings.DateTimeStartEnd des=ReportSettings.DateTimeStartEnd.getBySettings(SettingsControl.Settings);
			string uri=String.Format("Reports/PuskStop?year1={0}&month1={1}&day1={2}&year2={3}&month2={4}&day2={5}", 
				des.DateStart.Year,des.DateStart.Month,des.DateStart.Day,
				des.DateEnd.Year,des.DateEnd.Month,des.DateEnd.Day);

			FloatWindow.OpenWindow(uri);
		}

		private void btnGetPuskStopFull_Click(object sender, RoutedEventArgs e) {
			ReportSettings.DateTimeStartEnd des=ReportSettings.DateTimeStartEnd.getBySettings(SettingsControl.Settings);
			string uri=String.Format("Reports/PuskStopFull?year1={0}&month1={1}&day1={2}&year2={3}&month2={4}&day2={5}",
				des.DateStart.Year, des.DateStart.Month, des.DateStart.Day,
				des.DateEnd.Year, des.DateEnd.Month, des.DateEnd.Day);

			FloatWindow.OpenWindow(uri);
		}



	}
}
