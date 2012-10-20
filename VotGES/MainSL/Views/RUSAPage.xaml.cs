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
using VotGES.Web.Models;
using System.ServiceModel.DomainServices.Client;
using VotGES.Web.Services;
using VotGES.Rashod;

namespace MainSL.Views
{
	public partial class RUSAPage : Page
	{	

		RashodHarsData currentRashodHarsData;
		public RashodHarsData CurrentRashodHarsData {
			get { return currentRashodHarsData; }
			set { 
				currentRashodHarsData = value;				
				pnlDataRashodHars.DataContext = CurrentRashodHarsData;
			}
		}

		RashodHarsData currentMaket8;
		public RashodHarsData CurrentMaket8 {
			get { return currentMaket8; }
			set { 								
				currentMaket8 = value;
				pnlDataMaket8.DataContext = CurrentMaket8;
				
			}
		}

		RUSADomainContext context;
		ChartWindow chartWindow;

		public RUSAPage() {
			InitializeComponent();
			context = new RUSADomainContext();
			cntrlRUSA.init(context);
			chartWindow = new ChartWindow();
		}

		

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			CurrentRashodHarsData = new RashodHarsData();
			CurrentRashodHarsData.Napor = 21;
			CurrentRashodHarsData.Power = 300;
			CurrentRashodHarsData.Rashod = 1300;
			CurrentRashodHarsData.GANumbers = new Dictionary<int, string>();
			for (int ga=1; ga <= 10; ga++) {
				CurrentRashodHarsData.GANumbers.Add(ga, "Генератор " + ga);
			}
			CurrentRashodHarsData.GANumbers.Add(11, "Средний по станции");
			CurrentRashodHarsData.GANumbers.Add(12, "Оптимальный по станции");
			cmbGenSelect.ItemsSource = CurrentRashodHarsData.GANumbers;
			CurrentRashodHarsData.GANumber = 11;
			

			CurrentMaket8 = new RashodHarsData();
			CurrentMaket8.Napor = 21;
			CurrentMaket8.NeedTime = 8;
			CurrentMaket8.RashodFavr = 1200;
			CurrentMaket8.PRaspGTP1 = 220;
			CurrentMaket8.PRaspGTP2 = 800;
			CurrentMaket8.PGTP1 = 80;
			CurrentMaket8.NeedTime = 8;
			CurrentMaket8.Rashod0 = 0;
			CurrentMaket8.Maket = new Maket8HoursData();
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			GlobalStatus.Current.StopLoad();
		}
				
		void processOper_Completed(object sender, EventArgs e) {

		}

		private void btnCalcRashod_Click(object sender, RoutedEventArgs e) {			
			InvokeOperation currentOper=context.processRashodHarsData(CurrentRashodHarsData,true, oper => {
				if (oper.IsCanceled) {
					return;
				}
				try {
					GlobalStatus.Current.StartProcess();
					CurrentRashodHarsData = oper.Value;
				} catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
					GlobalStatus.Current.ErrorLoad("Ошибка");
				} finally {
					GlobalStatus.Current.StopLoad();
				}
			}, null);
			GlobalStatus.Current.StartLoad(currentOper);		
		}

		private void btnCalcPower_Click(object sender, RoutedEventArgs e) {
			InvokeOperation currentOper=context.processRashodHarsData(CurrentRashodHarsData,false, oper => {
				if (oper.IsCanceled) {
					return;
				}
				try {
					GlobalStatus.Current.StartProcess();
					CurrentRashodHarsData = oper.Value;
				} catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
					GlobalStatus.Current.ErrorLoad("Ошибка");
				} finally {
					GlobalStatus.Current.StopLoad();
				}
			}, null);
			GlobalStatus.Current.StartLoad(currentOper);	
		}

		private void btnCalcMaket_Click(object sender, RoutedEventArgs e) {
			InvokeOperation currentOper=context.processMaket(CurrentMaket8, oper => {
				if (oper.IsCanceled) {
					return;
				}
				try {
					GlobalStatus.Current.StartProcess();
					CurrentMaket8 = oper.Value;
				} catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
					GlobalStatus.Current.ErrorLoad("Ошибка");
				} finally {
					GlobalStatus.Current.StopLoad();
				}
			}, null);
			GlobalStatus.Current.StartLoad(currentOper);	
		}

		private void loadChart(RHChartType type) {
			InvokeOperation currentOper=context.getChart(CurrentRashodHarsData,type, oper => {
				if (oper.IsCanceled) {
					return;
				}
				try {
					GlobalStatus.Current.StartProcess();

					chartWindow.cntrlChart.Create(oper.Value);
					chartWindow.Title = oper.Value.Title;
					chartWindow.Show();
				} catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
					GlobalStatus.Current.ErrorLoad("Ошибка");
				} finally {
					GlobalStatus.Current.StopLoad();
				}
			}, null);
			GlobalStatus.Current.StartLoad(currentOper);	
		}

		private void btnGA_QotP_Click(object sender, RoutedEventArgs e) {
			loadChart(RHChartType.GA_QotP);
		}

		private void btnGA_KPDotP_Click(object sender, RoutedEventArgs e) {
			loadChart(RHChartType.GA_KPDotP);
		}

		private void btnGA_QotH_Click(object sender, RoutedEventArgs e) {
			loadChart(RHChartType.GA_QotH);
		}

		private void btnGA_KPDotH_Click(object sender, RoutedEventArgs e) {
			loadChart(RHChartType.GA_KPDotH);
		}

		private void btnCMPGA_QotP_Click(object sender, RoutedEventArgs e) {
			loadChart(RHChartType.CMPGA_QotP);
		}

		private void btnCMPGA_KPDotP_Click(object sender, RoutedEventArgs e) {
			loadChart(RHChartType.CMPGA_KPDotP);
		}

		private void btnCMPGA_QotH_Click(object sender, RoutedEventArgs e) {
			loadChart(RHChartType.CMPGA_QotH);
		}

		private void btnCMPGA_KPDotH_Click(object sender, RoutedEventArgs e) {
			loadChart(RHChartType.CMPGA_KPDotH);
		}

		private void btnCMPST_QotP_Click(object sender, RoutedEventArgs e) {
			loadChart(RHChartType.CMPST_QotP);
		}

		private void btnCMPST_KPDotP_Click(object sender, RoutedEventArgs e) {
			loadChart(RHChartType.CMPST_KPDotP);
		}

		private void btnCMPST_QotH_Click(object sender, RoutedEventArgs e) {
			loadChart(RHChartType.CMPST_QotH);
		}

		private void btnCMPST_KPDotH_Click(object sender, RoutedEventArgs e) {
			loadChart(RHChartType.CMPST_KPDotH);
		}

	}
}
