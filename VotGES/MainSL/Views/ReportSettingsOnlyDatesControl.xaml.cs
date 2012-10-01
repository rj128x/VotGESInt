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
	


	public partial class ReportSettingsOnlyDatesControl : UserControl
	{
		public ReportSettings Settings { get; set; }
		public ReportSettingsOnlyDatesControl() {
			InitializeComponent();
			Init();
		}

		public void Init() {
			Settings = new ReportSettings(true);			
			pnlSettings.DataContext = Settings;
		}

		

	}
}
