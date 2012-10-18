using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;

namespace VotGES.Web.Models
{
	
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

		public void ProcessData(bool calcRashod) {
			if (calcRashod) {
				if (gaNumber < 12) {
					Rashod = RashodTable.getRashod(gaNumber, Power, Napor);
				} else if (GANumber == 12) {
					Power = RashodTable.getStationRashod(Power, Napor, RashodCalcMode.min);
				}
			} else {
				Power = RashodTable.getPower(gaNumber, Rashod, Napor);
			}
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