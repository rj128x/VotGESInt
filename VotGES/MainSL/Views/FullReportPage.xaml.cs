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
using VotGES.Piramida.Report;
using VotGES.Web.Services;
using System.ServiceModel.DomainServices.Client;
using System.Reflection;
using System.ServiceModel;

namespace MainSL.Views
{

	public partial class FullReportPage : Page
	{
		string ExcelUri;

		FullReportRoot Root { get; set; }
		ReportBaseDomainContext Context { get; set; }
		List<String> SelectedValues { get; set; }

		public FullReportPage() {
			InitializeComponent();
			Context = new ReportBaseDomainContext();
			SelectedValues = new List<string>();
			SettingsControl.Settings.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Settings_PropertyChanged);
		}

		void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName == "ChildReports") {
				ReportSettingsControl forRemove=null;
				foreach (ReportSettingsControl cntrl in pnlAddReports.Children) {
					if (!SettingsControl.Settings.ChildReports.Contains(cntrl.Settings)) {
						forRemove = cntrl;
						break;
					}
				}
				pnlAddReports.Children.Remove(forRemove);
			}
		}

		private void btnAddCompare_Click(object sender, RoutedEventArgs e) {
			if (SettingsControl.Settings.ChildReports.Count < 10) {
				ReportSettingsControl cntrl=new ReportSettingsControl();
				pnlAddReports.Children.Add(cntrl);
				SettingsControl.Settings.AddChildReport(cntrl.Settings);
			}
		}


		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			if (Root == null) {
				loadRoot();
			}
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			GlobalStatus.Current.StopLoad();
		}

		protected void loadRoot() {
			InvokeOperation currentOper=Context.GetFullReportRoot(oper => {
				if (oper.IsCanceled) {
					return;
				}
				try {
					GlobalStatus.Current.StartProcess();
					Root = oper.Value;
					TreeMainRecords.ItemsSource = Root.RootMain.Children;
					TreeLinesRecords.ItemsSource = Root.RootLines.Children;
					TreeSNRecords.ItemsSource = Root.RootSN.Children;

				} catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
					GlobalStatus.Current.ErrorLoad("Ошибка при получении дерева");
				} finally {
					GlobalStatus.Current.StopLoad();
				}

			}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}

		protected void RefreshSelectedValues() {
			SelectedValues.Clear();
			createSelectedList(Root.RootMain);
			createSelectedList(Root.RootLines);
			createSelectedList(Root.RootSN);
		}

		protected void createSelectedList(FullReportRecord record) {
			foreach (FullReportRecord child in record.Children) {
				if (child.Selected) {
					SelectedValues.Add(child.Key);
				}
				if (child.IsGroup) {
					createSelectedList(child);
				}
			}
		}

		private void btnGetReport_Click(object sender, RoutedEventArgs e) {
			Guid reportGUID=Guid.NewGuid();
			if (SettingsControl.Settings.IsExcel) {
				ExcelUri = String.Format("Reports/FullReport?guid={0}", reportGUID);
				FloatWindow.OpenWindow(ExcelUri, 20, 20);
			}


			RefreshSelectedValues();
			ReportSettings.DateTimeStartEnd des=ReportSettings.DateTimeStartEnd.getBySettings(SettingsControl.Settings);
			List<DateTime> dateStartList=new List<DateTime>();
			List<DateTime> dateEndList=new List<DateTime>();
			List<FullReportMembersType>mbTypeList=new List<FullReportMembersType>();
			List<string> TitleList=new List<string>();
			foreach (ReportSettings setting in SettingsControl.Settings.ChildReports) {
				ReportSettings.DateTimeStartEnd desCmp=ReportSettings.DateTimeStartEnd.getBySettings(setting);
				if (!TitleList.Contains(desCmp.Title)) {
					dateStartList.Add(desCmp.DateStart);
					dateEndList.Add(desCmp.DateEnd);
					mbTypeList.Add(setting.MBType);
					string title=setting.MBType != FullReportMembersType.def ? desCmp.Title + "(" + setting.MBType.ToString() + ")" : desCmp.Title;
					TitleList.Add(title);
				}
			}
			if (TitleList.Count > 0) {
				des.Title = SettingsControl.Settings.MBType != FullReportMembersType.def ?
					des.Title + "(" + SettingsControl.Settings.MBType.ToString() + ")" : des.Title;
			} else {
				des.Title = "";
			}


			InvokeOperation currentOper=Context.GetFullReport(SelectedValues, des.Title, des.DateStart, des.DateEnd,
				SettingsControl.Settings.ReportType, SettingsControl.Settings.MBType,
				SettingsControl.Settings.IsChart, SettingsControl.Settings.IsTable, SettingsControl.Settings.IsExcel, reportGUID,
				TitleList, dateStartList, dateEndList, mbTypeList,
				oper => {
					if (oper.IsCanceled) {
						return;
					}
					try {
						GlobalStatus.Current.StartProcess();
						if (SettingsControl.Settings.IsExcel) {
							ExcelUri = String.Format("Reports/FullReport?guid={0}", oper.Value.ReportID);
							FloatWindow.OpenWindow(ExcelUri);
						} else {
							ResultControl.Create(oper.Value);
							tabResult.IsSelected = true;
						}
					} catch (Exception ex) {
						Logging.Logger.info(ex.ToString());
						GlobalStatus.Current.ErrorLoad("Ошибка при получении данных");
					} finally {
						GlobalStatus.Current.StopLoad();

					}
				}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}



		private void btnGetChart_Click(object sender, RoutedEventArgs e) {

		}

		private void btnGetReport_Click_1(object sender, RoutedEventArgs e) {

		}


	}
}
