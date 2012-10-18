﻿using System;
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

namespace MainSL.Views
{
	public partial class RUSAPage : Page
	{
		private RUSAData currentData;
		public RUSAData CurrentData {
			get { return currentData; }
			set { 
				currentData = value;
				pnlData.DataContext = CurrentData;
			}
		}

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

		public RUSAPage() {
			InitializeComponent();
			context = new RUSADomainContext();
		}

		

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			CurrentData=new RUSAData();
			CurrentData.GaAvail = new List<GAParams>();
			for (int ga=1; ga <= 10; ga++) {
				GAParams p=new GAParams();
				p.GaNumber = ga;
				p.Avail = true;
				CurrentData.GaAvail.Add(p);
			}
			CurrentData.Power = 300;
			CurrentData.Napor = 21;


			CurrentRashodHarsData = new RashodHarsData();
			CurrentRashodHarsData.Napor = 21;
			CurrentRashodHarsData.Power = 300;
			CurrentRashodHarsData.Rashod = 1200;
			CurrentRashodHarsData.GANumbers = new Dictionary<int, string>();
			for (int ga=1; ga <= 10; ga++) {
				CurrentRashodHarsData.GANumbers.Add(ga, "Генератор " + ga);
			}
			CurrentRashodHarsData.GANumbers.Add(11, "Средний по станции");
			CurrentRashodHarsData.GANumbers.Add(12, "Оптимальный по станции");
			CurrentRashodHarsData.GANumber = 11;
			cmbGenSelect.ItemsSource = CurrentRashodHarsData.GANumbers;

			CurrentMaket8 = new RashodHarsData();
			CurrentMaket8.Napor = 21;
			CurrentMaket8.NeedTime = 8;
			CurrentMaket8.RashodFavr = 1200;
			CurrentMaket8.PRaspGTP1 = 220;
			CurrentMaket8.PRaspGTP2 = 800;
			CurrentMaket8.PGTP1 = 80;
			CurrentMaket8.NeedTime = 8;
			CurrentMaket8.Rashod0 = 0;
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e) {
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
			},null);
			GlobalStatus.Current.StartLoad(currentOper);			
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

		}

	}
}
