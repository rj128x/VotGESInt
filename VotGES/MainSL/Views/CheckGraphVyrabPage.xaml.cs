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
using System.ServiceModel.DomainServices.Client;
using VotGES.Web.Services;
using VotGES.Chart;
using VotGES.PBR;

namespace MainSL.Views
{
	public partial class CheckGraphVyrabPage : Page
	{
		public SettingsBase settings;
		public GraphVyrabDomainContext context;
		public CheckGraphVyrabAnswer currentAnswer;

		public CheckGraphVyrabPage() {
			InitializeComponent();
			settings = new SettingsBase();
			settings.Date = DateTime.Now.Date.AddHours(-24);
			pnlSettings.DataContext = settings;
			chbIsSteppedPBR.IsChecked = true;
			context = new GraphVyrabDomainContext();

			tabHHReport.Visibility = System.Windows.Visibility.Collapsed;
			tabHReport.Visibility = System.Windows.Visibility.Collapsed;
			tabHHRGEReport.Visibility = System.Windows.Visibility.Collapsed;
			tabHRGEReport.Visibility = System.Windows.Visibility.Collapsed;
			tabChart.Visibility = System.Windows.Visibility.Collapsed;
			tabChartRGE.Visibility = System.Windows.Visibility.Collapsed;
			tabChart.Visibility = System.Windows.Visibility.Collapsed;
		}



		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
		}
		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			GlobalStatus.Current.StopLoad();

		}



		private void btnMin_Click(object sender, RoutedEventArgs e) {
			InvokeOperation currentOper=context.getGraphVyrabMin(settings.Date,chbIsSteppedPBR.IsChecked.Value,
				oper => {
					if (oper.IsCanceled) {
						return;
					}
					GlobalStatus.Current.StartProcess();
					try {
						tabChart.IsSelected = true;
						ChartAnswer answer=oper.Value.Chart;
						chartControl.Create(answer);
						tabHHReport.Visibility = System.Windows.Visibility.Collapsed;
						tabHReport.Visibility = System.Windows.Visibility.Collapsed;
						tabHHRGEReport.Visibility = System.Windows.Visibility.Collapsed;
						tabHRGEReport.Visibility = System.Windows.Visibility.Collapsed;
						tabChartRGE.Visibility = System.Windows.Visibility.Collapsed;
						tabChart.Visibility = System.Windows.Visibility.Visible;
						tabChart.IsSelected = true;
					} catch (Exception ex) {
						Logging.Logger.info(ex.ToString());
						GlobalStatus.Current.ErrorLoad("Ошибка");
					} finally {
						GlobalStatus.Current.StopLoad();
					}
				}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}

		private void btnHH_Click(object sender, RoutedEventArgs e) {			
			InvokeOperation currentOper=context.getGraphVyrabHH(settings.Date,
				oper => {
					if (oper.IsCanceled) {
						return;
					}
					GlobalStatus.Current.StartProcess();
					
					try {
						ChartAnswer answer=oper.Value.Chart;
						chartControl.Create(answer);
						currentAnswer = oper.Value;

						tabHHReport.Visibility = System.Windows.Visibility.Visible;
						tabHReport.Visibility = System.Windows.Visibility.Visible;
						tabHHRGEReport.Visibility = System.Windows.Visibility.Collapsed;
						tabHRGEReport.Visibility = System.Windows.Visibility.Collapsed;
						tabChartRGE.Visibility = System.Windows.Visibility.Collapsed;
						tabChart.Visibility = System.Windows.Visibility.Visible;
						tabChart.IsSelected = true;
						cntrlHHReport.grdReport.ItemsSource = currentAnswer.TableHH;
						cntrlHReport.grdReport.ItemsSource = currentAnswer.TableH;
					} catch (Exception ex) {
						Logging.Logger.info(ex.ToString());
						GlobalStatus.Current.ErrorLoad("Ошибка");
					} finally {
						GlobalStatus.Current.StopLoad();
					}
				}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}

		private void btnMinRGE_Click(object sender, RoutedEventArgs e) {
			InvokeOperation currentOper=context.getGraphVyrabRGEMin(settings.Date,chbIsSteppedPBR.IsChecked.Value,
				oper => {
					if (oper.IsCanceled) {
						return;
					}
					GlobalStatus.Current.StartProcess();
					try {
						tabChartRGE.IsSelected = true;
						chartControlRGE1.Create(oper.Value.ChartRGE1);
						chartControlRGE2.Create(oper.Value.ChartRGE2);
						chartControlRGE3.Create(oper.Value.ChartRGE3);
						chartControlRGE4.Create(oper.Value.ChartRGE4);

						tabHHReport.Visibility = System.Windows.Visibility.Collapsed;
						tabHReport.Visibility = System.Windows.Visibility.Collapsed;
						tabHHRGEReport.Visibility = System.Windows.Visibility.Collapsed;
						tabHRGEReport.Visibility = System.Windows.Visibility.Collapsed;
						tabChart.Visibility = System.Windows.Visibility.Collapsed;
						tabChartRGE.Visibility = System.Windows.Visibility.Visible;
						tabChart.Visibility = System.Windows.Visibility.Collapsed;
						tabChartRGE.IsSelected = true;
					} catch (Exception ex) {
						Logging.Logger.info(ex.ToString());
						GlobalStatus.Current.ErrorLoad("Ошибка");
					} finally {
						GlobalStatus.Current.StopLoad();
					}
				}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}

		private void btnHHRGE_Click(object sender, RoutedEventArgs e) {
			InvokeOperation currentOper=context.getGraphVyrabRGEHH(settings.Date,
				oper => {
					if (oper.IsCanceled) {
						return;
					}
					GlobalStatus.Current.StartProcess();

					try {
						chartControlRGE1.Create(oper.Value.ChartRGE1);
						chartControlRGE2.Create(oper.Value.ChartRGE2);
						chartControlRGE3.Create(oper.Value.ChartRGE3);
						chartControlRGE4.Create(oper.Value.ChartRGE4);

						tabHHReport.Visibility = System.Windows.Visibility.Collapsed;
						tabHReport.Visibility = System.Windows.Visibility.Collapsed;
						tabHHRGEReport.Visibility = System.Windows.Visibility.Visible;
						tabHRGEReport.Visibility = System.Windows.Visibility.Visible;
						tabChart.Visibility = System.Windows.Visibility.Collapsed;
						tabChartRGE.Visibility = System.Windows.Visibility.Visible;
						tabChart.Visibility = System.Windows.Visibility.Collapsed;
						tabChartRGE.IsSelected = true;

						cntrlHHRGEReport.grdReport.ItemsSource = oper.Value.TableHH;
						cntrlHRGEReport.grdReport.ItemsSource = oper.Value.TableH;
					} catch (Exception ex) {
						Logging.Logger.info(ex.ToString());
						GlobalStatus.Current.ErrorLoad("Ошибка");
					} finally {
						GlobalStatus.Current.StopLoad();
					}
				}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}


	}
}
