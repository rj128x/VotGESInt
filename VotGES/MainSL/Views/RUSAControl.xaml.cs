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
		}

		public void destroy() {
			GlobalStatus.Current.StopLoad();
		}

		private void btnCalcRUSA_Click(object sender, RoutedEventArgs e) {
			InvokeOperation currentOper=context.processRUSAData(CurrentData, oper => {
				if (oper.IsCanceled) {
					return;
				}
				try {
					GlobalStatus.Current.StartProcess();
					CurrentData = oper.Value;
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
