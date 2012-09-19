﻿using System;
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

		protected void OpenWindow(string url) {
			string host=Application.Current.Host.Source.Host;
			int port=Application.Current.Host.Source.Port;
			Uri uri=new Uri(String.Format("http://{0}:{1}/{2}", host, port,url));
			System.Windows.Browser.HtmlPopupWindowOptions options=new System.Windows.Browser.HtmlPopupWindowOptions();			
			options.Resizeable=true;
			options.Width=1100;
			options.Height=600;
			options.Menubar=true;
			options.Directories=true;
			options.Toolbar=true;
			options.Status=true;			
			System.Windows.Browser.HtmlPage.PopupWindow(uri, "", options);
			
		}

		private void btnGetReport_Click(object sender, RoutedEventArgs e) {			
			string uri=String.Format("Reports/SutVed?year={0}&month={1}&day={2}",settings.Date.Year,settings.Date.Month,settings.Date.Day);
			OpenWindow(uri);
		}

		private void btnGetPBR_Click(object sender, RoutedEventArgs e) {
			string uri=String.Format("Reports/PBR?year={0}&month={1}&day={2}", settings.Date.Year, settings.Date.Month, settings.Date.Day);
			OpenWindow(uri);
		}

		private void btnPrikaz20_Click(object sender, RoutedEventArgs e) {
			string uri=String.Format("Reports/Prikaz20?year={0}&month={1}&day={2}", settings.Date.Year, settings.Date.Month, settings.Date.Day);
			OpenWindow(uri);
		}



	}
}
