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
using System.ComponentModel;
using VotGES.Piramida.Report;

namespace MainSL.Views
{
	


	public partial class ReportSettingsControl : UserControl
	{
		public ReportSettings Settings { get; set; }
		public ReportSettingsControl() {
			InitializeComponent();
			Init();
		}

		public void Init() {
			Settings = new ReportSettings();			
			pnlSettings.DataContext = Settings;
		}

		public void InitOnlyDates() {
			Settings = new ReportSettings(true);
			pnlSettings.DataContext = Settings;
		}

		private void btnRemCompare_Click(object sender, RoutedEventArgs e) {
			Settings.ParentReport.RemoveChildReport(this.Settings);
		}
		

	}
}
