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
using VotGES.Chart;
using VotGES.PBR;
using System.Threading;
using System.Windows.Threading;
using MainSL.Logging;

namespace MainSL.Views
{
	public partial class GraphVyrabRGEPage : Page
	{
		DispatcherTimer timer;
		public FullGraphVyrab CurrentAnswer { get; set; }
		public GraphVyrabDomainContext context;
		public SettingsGraphVyab settings;
		public RUSAWindow rusaWindow;
		public bool PrevAutoRefresh { get; set; }
		
		public GraphVyrabRGEPage() {
			InitializeComponent();
			CurrentAnswer = new FullGraphVyrab();
			CurrentAnswer.Napor = 21;
			context = new GraphVyrabDomainContext();
			pnlSettings.DataContext = CurrentAnswer;
			settings = new SettingsGraphVyab();
			chbIsSteppedPBR.IsChecked = true;
			settings.Second = 60;
			settings.AutoRefresh = false;
			pnlRefresh.DataContext = settings;
			timer = new DispatcherTimer();
			timer.Tick += new EventHandler(timer_Tick);
			timer.Interval = new TimeSpan(0, 0, 1);
			rusaWindow = new RUSAWindow();
			rusaWindow.Closed += new EventHandler(rusaWindow_Closed);
		}

		void rusaWindow_Closed(object sender, EventArgs e) {
			settings.AutoRefresh = PrevAutoRefresh;
		}
				
	
		void timer_Tick(object sender, EventArgs e) {
			if (!GlobalStatus.Current.IsBusy && settings.AutoRefresh) {
				settings.Second--;
				if (settings.Second == 0 ) {
					refresh();
				}
			}
			
		}

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			timer.Start();
			refresh();			
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			GlobalStatus.Current.StopLoad();
			timer.Stop();
		}

		private void btnRefresh_Click(object sender, RoutedEventArgs e) {
			refresh();
		}

		private void processGTP() {
			try {
				if (tabChart.IsSelected) {
					chartControl.Create(CurrentAnswer.GTP.Chart);
					chartControl.Visibility = System.Windows.Visibility.Visible;
				} else {
					chartControl.Visibility = System.Windows.Visibility.Collapsed;
				}
			} catch (Exception e) {
				//Logger.info("Ошибка при создании графика ГТП");
				//Logger.info(e.ToString());
			}
		}

		private void processRGE() {
			try {
				if (tabChartRGE.IsSelected) {

					chartControlRGE1.Create(CurrentAnswer.RGE.ChartRGE1);
					chartControlRGE2.Create(CurrentAnswer.RGE.ChartRGE2);
					chartControlRGE3.Create(CurrentAnswer.RGE.ChartRGE3);
					chartControlRGE4.Create(CurrentAnswer.RGE.ChartRGE4);
					chartControlRGE1.Visibility = System.Windows.Visibility.Visible;
					chartControlRGE2.Visibility = System.Windows.Visibility.Visible;
					chartControlRGE3.Visibility = System.Windows.Visibility.Visible;
					chartControlRGE4.Visibility = System.Windows.Visibility.Visible;
				} else {
					chartControlRGE1.Visibility = System.Windows.Visibility.Collapsed;
					chartControlRGE2.Visibility = System.Windows.Visibility.Collapsed;
					chartControlRGE3.Visibility = System.Windows.Visibility.Collapsed;
					chartControlRGE4.Visibility = System.Windows.Visibility.Collapsed;
				}
			} catch (Exception e) {
				//Logger.info("Ошибка при создании графика РГЕ");
				//Logger.info(e.ToString());
			}
		}

		private void refresh() {
			if (GlobalStatus.Current.IsBusy)
				return;			
			InvokeOperation currentOper=context.getFullGraphVyrab(chbIsSteppedPBR.IsChecked.Value,
				oper => {
					if (oper.IsCanceled) {
						return;
					}
					GlobalStatus.Current.StartProcess();
					try {
						try {
							txtActualDate.Text = "График нагрузки на " + oper.Value.GTP.ActualDate.ToString("HH:mm") + " (мск)";
						} catch { }
						pnlSettings.DataContext = oper.Value;
						CurrentAnswer = oper.Value;
						
						processGTP();
						processRGE();

						try {
							Dictionary<int,string> stopTime=oper.Value.TimeStopGA;
							txtSropGA1.Text = stopTime[1];
							txtSropGA2.Text = stopTime[2];
							txtSropGA3.Text = stopTime[3];
							txtSropGA4.Text = stopTime[4];
							txtSropGA5.Text = stopTime[5];
							txtSropGA6.Text = stopTime[6];
							txtSropGA7.Text = stopTime[7];
							txtSropGA8.Text = stopTime[8];
							txtSropGA9.Text = stopTime[9];
							txtSropGA10.Text = stopTime[10];
						} catch (Exception e) {
							Logger.info("Ошибка при обработке пусков-остановов");
							Logger.info(e.ToString());
						}

					} catch (Exception ex) {
						Logging.Logger.info(ex.ToString());
						GlobalStatus.Current.ErrorLoad("Ошибка");
					} finally {
						GlobalStatus.Current.StopLoad();
					}
				}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}

		private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			try {
				processRGE();
			} catch { }
			try {
				processGTP();
			} catch { }
		}

		public void showRusa() {
			PrevAutoRefresh = settings.AutoRefresh;
			settings.AutoRefresh = false;			
			try {
				if (CurrentAnswer != null) {
					rusaWindow.initNapor(CurrentAnswer.Napor);
				}
			} catch { }
			rusaWindow.Show();
		}

		private void btnGTP1_Click(object sender, RoutedEventArgs e) {
			rusaWindow.initGTP1();
			try {
				rusaWindow.initPower(CurrentAnswer.GTP.TableCurrent[1].GTP1);
			} catch { }
			showRusa();
		}

		private void btnGTP2_Click(object sender, RoutedEventArgs e) {
			rusaWindow.initGTP2();
			try {
				rusaWindow.initPower(CurrentAnswer.GTP.TableCurrent[1].GTP2);
			} catch { }
			showRusa();
		}

		private void btnGES_Click(object sender, RoutedEventArgs e) {
			rusaWindow.initGES();
			try {
				rusaWindow.initPower(CurrentAnswer.GTP.TableCurrent[1].GES);
			} catch { }
			showRusa();
		}

		private void btnRGE2_Click(object sender, RoutedEventArgs e) {
			rusaWindow.initRGE2();
			try {
				rusaWindow.initPower(CurrentAnswer.RGE.TableCurrent[1].RGE2);
			} catch { }
			showRusa();
		}

		private void btnRGE3_Click(object sender, RoutedEventArgs e) {
			rusaWindow.initRGE3();
			try {
				rusaWindow.initPower(CurrentAnswer.RGE.TableCurrent[1].RGE3);
			} catch { }
			showRusa();
		}

		private void btnRGE4_Click(object sender, RoutedEventArgs e) {
			rusaWindow.initRGE4();
			try {
				rusaWindow.initPower(CurrentAnswer.RGE.TableCurrent[1].RGE4);
			} catch { }
			showRusa();
		}

		private void btnPbr_Click(object sender, RoutedEventArgs e) {
			string uri=String.Format("Reports/PBR?year={0}&month={1}&day={2}", 
				CurrentAnswer.GTP.ActualDate.Year,CurrentAnswer.GTP.ActualDate.Month,CurrentAnswer.GTP.ActualDate.Day);
			FloatWindow.OpenWindow(uri);
		}

		private void btnCurrent_Click(object sender, RoutedEventArgs e) {
			if (brdCurrent.Visibility == System.Windows.Visibility.Collapsed) {
				brdCurrent.Visibility = System.Windows.Visibility.Visible;
			} else {
				brdCurrent.Visibility = System.Windows.Visibility.Collapsed;
			}
		}

		private void btnHour_Click(object sender, RoutedEventArgs e) {
			if (brdHour.Visibility == System.Windows.Visibility.Collapsed) {
				brdHour.Visibility = System.Windows.Visibility.Visible;
			} else {
				brdHour.Visibility = System.Windows.Visibility.Collapsed;
			}
		}
		
	}

	public class SettingsGraphVyab : SettingsBase
	{
		private int second;

		public int Second {
			get { return second; }
			set {
				second = value;
				second = second < 0 ? 60 : second;
				NotifyChanged("Second");
			}
		}

		private bool autoRefresh;
		public bool AutoRefresh {
			get { return autoRefresh; }
			set {
				autoRefresh = value;
				NotifyChanged("AutoRefresh");
			}
		}
	}
}
