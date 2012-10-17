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

namespace MainSL.Views
{
	public class SettingsGraphVyab : SettingsBase
	{
		private int second;

		public int Second {
			get { return second; }
			set {
				second = value;
				second = second < 0 ? 30 : second;
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

	public partial class GraphVyrabRGEPage : Page
	{
		DispatcherTimer timer;
		public FullGraphVyrab CurrentAnswer { get; set; }
		public GraphVyrabDomainContext context;
		public SettingsGraphVyab settings;

		public GraphVyrabRGEPage() {
			InitializeComponent();
			CurrentAnswer = new FullGraphVyrab();
			context = new GraphVyrabDomainContext();
			pnlSettings.DataContext = CurrentAnswer;
			settings = new SettingsGraphVyab();
			settings.Second = 30;
			settings.AutoRefresh = true;
			pnlRefresh.DataContext = settings;
			timer = new DispatcherTimer();
			timer.Tick += new EventHandler(timer_Tick);
			timer.Interval = new TimeSpan(0, 0, 1);
			
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

		private void refresh() {
			if (GlobalStatus.Current.IsBusy)
				return;
			InvokeOperation currentOper=context.getFullGraphVyrab(
				oper => {
					if (oper.IsCanceled) {
						return;
					}
					GlobalStatus.Current.StartProcess();
					try {
						txtActualDate.Text = oper.Value.GTP.ActualDate.ToString("HH:mm");
						pnlSettings.DataContext = oper.Value;
						chartControl.Create(oper.Value.GTP.Chart);
						chartControlRGE1.Create(oper.Value.RGE.ChartRGE1);
						chartControlRGE2.Create(oper.Value.RGE.ChartRGE2);
						chartControlRGE3.Create(oper.Value.RGE.ChartRGE3);
						chartControlRGE4.Create(oper.Value.RGE.ChartRGE4);
						CurrentAnswer = oper.Value;
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
