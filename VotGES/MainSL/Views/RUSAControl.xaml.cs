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
using Visiblox.Charts.Primitives;
using Visiblox.Charts;
using System.ComponentModel;
using VotGES.Chart;
using System.Windows.Markup;
using System.ServiceModel.DomainServices.Client;
using VotGES.Web.Models;
using VotGES.Web.Services;

namespace MainSL.Views
{
	public partial class RUSAControl : UserControl
	{
		RUSADomainContext context;
		Dictionary<int,TabItem>items;
		Dictionary<int,RUSAGridControl>gridControls;
		private RUSAData currentData;
		public RUSAData CurrentData {
			get { return currentData; }
			set {
				currentData = value;
				pnlData.DataContext = CurrentData;
			}
		}

		public RUSAControl() {
			InitializeComponent();
			init(null);
		}

		public void init(RUSADomainContext context) {
			this.context = context;
			CurrentData = new RUSAData();
			CurrentData.GaAvail = new List<GAParams>();
			for (int ga=1; ga <= 10; ga++) {
				GAParams p=new GAParams();
				p.GaNumber = ga;
				p.Avail = true;
				CurrentData.GaAvail.Add(p);
			}
			CurrentData.Power = 300;
			CurrentData.Napor = 21;

			items=new Dictionary<int, TabItem>();
			gridControls=new Dictionary<int, RUSAGridControl>();

			items.Add(1, tab1);
			items.Add(2, tab2);
			items.Add(3, tab3);
			items.Add(4, tab4);
			items.Add(5, tab5);
			items.Add(6, tab6);
			items.Add(7, tab7);
			items.Add(8, tab8);
			items.Add(9, tab9);
			items.Add(10, tab10);

			gridControls.Add(1, grid1);
			gridControls.Add(2, grid2);
			gridControls.Add(3, grid3);
			gridControls.Add(4, grid4);
			gridControls.Add(5, grid5);
			gridControls.Add(6, grid6);
			gridControls.Add(7, grid7);
			gridControls.Add(8, grid8);
			gridControls.Add(9, grid9);
			gridControls.Add(10, grid10);

		}

		public void destroy() {
			GlobalStatus.Current.StopLoad();
		}

		public void process() {
			gridDiff.Visibility = System.Windows.Visibility.Visible;
			gridEq.Visibility = System.Windows.Visibility.Visible;
			foreach (FullResultRUSARecord record in CurrentData.FullResultList) {
				gridControls[record.CountGA].DataContext = record.Data;
				items[record.CountGA].Visibility = System.Windows.Visibility.Visible;
			}
		}

		public void clear() {
			gridDiff.Visibility=System.Windows.Visibility.Collapsed;
			gridEq.Visibility = System.Windows.Visibility.Collapsed;
			for (int count=1; count <= 10; count++) {
				items[count].Visibility = System.Windows.Visibility.Collapsed;
				gridControls[count].DataContext = null;
			}
			try {
				CurrentData.DiffResult.Clear();
				CurrentData.EqResult.Clear();
				CurrentData.FullResultList.Clear();				
			} catch { }
		}

		private void btnCalcRUSA_Click(object sender, RoutedEventArgs e) {
			InvokeOperation currentOper=context.processRUSAData(CurrentData, oper => {
				if (oper.IsCanceled) {
					return;
				}
				try {
					GlobalStatus.Current.StartProcess();
					clear();
					CurrentData = oper.Value;					
					process();
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
