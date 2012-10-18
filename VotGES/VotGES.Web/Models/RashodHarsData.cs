using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;

namespace VotGES.Web.Models
{
	public class Maket8HoursData
	{
		public double PRaspGTP1 { get; set; }
		public double PRaspGTP2 { get; set; }
		public double PRaspGES { get; set; }

		public double P8HoursGTP1 { get; set; }
		public double P8HoursGTP2 { get; set; }
		public double P8HoursGES { get; set; }

		public double PPikGTP1 { get; set; }
		public double PPikGTP2 { get; set; }
		public double PPikGES { get; set; }

		public double RashodGTP1 { get; set; }
		public double RashodGTP2 { get; set; }
		public double RashodGES { get; set; }
				
	}
	public class RashodHarsData : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
		
		private Guid id;
		public Guid Id {
			get { return id; }
			set { id = value; }
		}

		private int gaNumber;
		public int GANumber
		{
		  get { return gaNumber; }
		  set { gaNumber = value; }
		}

		private Dictionary<int,string>gaNumbers;
		public Dictionary<int, string> GANumbers {
			get { return gaNumbers; }
			set { gaNumbers = value; }
		}
		
		private double napor;
		public double Napor {
			get { return napor; }
			set { napor = value; }
		}

		private double power;
		public double Power {
			get { return power; }
			set { power = value; }
		}

		private double rashod;
		public double Rashod {
			get { return rashod; }
			set { rashod = value; }
		}

		private int needTime;
		public int NeedTime {
			get { return needTime; }
			set { needTime = value; }
		}

		private double pRaspGTP1;
		public double PRaspGTP1 {
			get { return pRaspGTP1; }
			set { pRaspGTP1 = value; }
		}

		private double pRaspGTP2;
		public double PRaspGTP2 {
			get { return pRaspGTP2; }
			set { pRaspGTP2 = value; }
		}

		private double rashod0;
		public double Rashod0 {
			get { return rashod0; }
			set { rashod0 = value; }
		}

		private double pGTP1;
		public double PGTP1 {
			get { return pGTP1; }
			set { pGTP1 = value; }
		}

		private double rashodFavr;
		public double RashodFavr {
			get { return rashodFavr; }
			set { rashodFavr = value; }
		}

		private Maket8HoursData maket;
		public Maket8HoursData Maket {
			get { return maket; }
			set { maket = value; }
		}

		public void ProcessData(bool calcRashod) {
			if (calcRashod) {
				if (gaNumber < 12) {
					Rashod = RashodTable.getRashod(gaNumber, Power, Napor);
				} else if (GANumber == 12) {
					Rashod = RashodTable.getStationRashod(Power, Napor, RashodCalcMode.min);
				}
			} else {
				Power = RashodTable.getPower(gaNumber, Rashod, Napor);
			}
		}

		public void ProcessMaket(bool calcRashod) {
			Maket = new Maket8HoursData();
		}

		public RashodHarsData() {
			gaNumber=0;			
			power = 300;
			napor = 21;
			rashod = 1200;
			GANumbers = new Dictionary<int, string>();
			for (int ga=1; ga <= 10; ga++) {
				GANumbers.Add(ga, "Генератор " + ga);
			}
			GANumbers.Add(11,"Средний по станции");
			GANumbers.Add(12,"Оптимальный по станции");
		}
				
	}
}