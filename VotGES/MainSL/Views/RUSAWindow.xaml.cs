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
using VotGES.Web.Services;

namespace MainSL.Views
{
	public partial class RUSAWindow : ChildWindow
	{
		RUSADomainContext context;
		
		public RUSAWindow() {
			InitializeComponent();
			context = new RUSADomainContext();
			cntrlRUSA.init(context);			
		}

		private void OKButton_Click(object sender, RoutedEventArgs e) {
			this.DialogResult = true;
		}

		protected override void OnOpened() {
			this.cntrlRUSA.clear();
		}

		public void initGTP1() {
			this.cntrlRUSA.CurrentData.GaAvail[0].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[1].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[2].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[3].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[4].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[5].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[6].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[7].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[8].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[9].Avail = false;
		}

		public void initGTP2() {
			this.cntrlRUSA.CurrentData.GaAvail[0].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[1].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[2].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[3].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[4].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[5].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[6].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[7].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[8].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[9].Avail = true;
		}

		public void initGES() {
			this.cntrlRUSA.CurrentData.GaAvail[0].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[1].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[2].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[3].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[4].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[5].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[6].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[7].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[8].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[9].Avail = true;
		}

		public void initRGE2() {
			this.cntrlRUSA.CurrentData.GaAvail[0].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[1].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[2].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[3].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[4].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[5].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[6].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[7].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[8].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[9].Avail = false;
		}

		public void initRGE3() {
			this.cntrlRUSA.CurrentData.GaAvail[0].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[1].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[2].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[3].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[4].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[5].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[6].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[7].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[8].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[9].Avail = false;
		}

		public void initRGE4() {
			this.cntrlRUSA.CurrentData.GaAvail[0].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[1].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[2].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[3].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[4].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[5].Avail = false;
			this.cntrlRUSA.CurrentData.GaAvail[6].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[7].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[8].Avail = true;
			this.cntrlRUSA.CurrentData.GaAvail[9].Avail = true;
		}

		public void initNapor(double Napor) {
			this.cntrlRUSA.CurrentData.Napor = Napor;
		}

		public void initPower(double Power) {
			this.cntrlRUSA.CurrentData.Power = Power;
		}

	}
}

