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

		public double RashodTime { get; set; }
		public double PowerTime { get; set; }
				
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

		private int gaNumber=11;
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

		private bool maket8Optim;

		public bool Maket8Optim {
			get { return maket8Optim; }
			set { maket8Optim = value; }
		}

		private Maket8HoursData maket=new Maket8HoursData();
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

		public void ProcessMaket() {			
			double rashod8=(RashodFavr * 24 - Rashod0 * (24 - NeedTime)) / NeedTime;
			double power=RashodTable.getPower(maket8Optim?12:11, rashod8, Napor);
			Maket.P8HoursGTP1 = PGTP1;
			Maket.P8HoursGTP2 = power - PGTP1;
			Maket.P8HoursGES = power;
			Maket.PPikGTP1 = PRaspGTP1 - Maket.P8HoursGTP1;
			Maket.PPikGTP2 = PRaspGTP2 - Maket.P8HoursGTP2;
			Maket.PPikGES = PRaspGTP1 + PRaspGTP2 - power;

			int ga=Maket8Optim ? 12 : 11;

			Maket.RashodGTP1 = RashodTable.getRashod(ga, Maket.P8HoursGTP1, Napor);
			Maket.RashodGTP2 = RashodTable.getRashod(ga, Maket.P8HoursGTP2, Napor);
			Maket.RashodGES = Maket.RashodGTP1 + Maket.RashodGTP2;

			Maket.PRaspGTP1 = PRaspGTP1;
			Maket.PRaspGTP2 = PRaspGTP2;
			Maket.PRaspGES = PRaspGTP1+PRaspGTP2;

			Maket.RashodTime = rashod8;
			Maket.PowerTime = power;
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