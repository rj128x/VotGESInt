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
using VotGES.Piramida.Report;
using VotGES.Web.Services;
using System.ServiceModel.DomainServices.Client;

namespace MainSL.Views
{
	public partial class ReportsPage : Page
	{
		FullReportRoot Root { get; set; }
		ReportBaseDomainContext Context { get; set; }

		public ReportsPage() {
			InitializeComponent();
			Context = new ReportBaseDomainContext();
		}

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {

		}

		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			GlobalStatus.Current.StopLoad();
		}

		private void btnGetReport_Click(object sender, RoutedEventArgs e) {

		}

		

		
	}
}
