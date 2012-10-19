using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;

namespace VotGES.Web.Models
{
	public class GAParams
	{
		int gaNumber;
		public int GaNumber {
			get { return gaNumber; }
			set { gaNumber = value; }
		}

		bool avail;
		public bool Avail {
			get { return avail; }
			set { avail = value; }
		}

		public GAParams() {
		}

		public GAParams(int ga, bool avail) {
			this.gaNumber = ga;
			this.avail = avail;
		}
	}

	public class RUSAResult
	{
		private double rashod;
		public double Rashod {
			get { return rashod; }
			set { rashod = value; }
		}

		private double kpd;
		public double KPD {
			get { return kpd; }
			set { kpd = value; }
		}

		private Dictionary<int,double> sostav;
		public Dictionary<int, double> Sostav {
			get { return sostav; }
			set { sostav = value; }
		}

		public double PGA1 { get; set; }
		public double PGA2 { get; set; }
		public double PGA3 { get; set; }
		public double PGA4 { get; set; }
		public double PGA5 { get; set; }
		public double PGA6 { get; set; }
		public double PGA7 { get; set; }
		public double PGA8 { get; set; }
		public double PGA9 { get; set; }
		public double PGA10 { get; set; }

		public void ProcessSostav(Dictionary<int, double> sostav){
			PGA1 = sostav.ContainsKey(1) ? sostav[1] : 0;
			PGA2 = sostav.ContainsKey(2) ? sostav[2] : 0;
			PGA3 = sostav.ContainsKey(3) ? sostav[3] : 0;
			PGA4 = sostav.ContainsKey(4) ? sostav[4] : 0;
			PGA5 = sostav.ContainsKey(5) ? sostav[5] : 0;
			PGA6 = sostav.ContainsKey(6) ? sostav[6] : 0;
			PGA7 = sostav.ContainsKey(7) ? sostav[7] : 0;
			PGA8 = sostav.ContainsKey(8) ? sostav[8] : 0;
			PGA9 = sostav.ContainsKey(9) ? sostav[9] : 0;
			PGA10 = sostav.ContainsKey(10) ? sostav[10] : 0;
		}

		private int count;
		public int Count {
			get { return count; }
			set { count = value; }
		}
	}

	public class FullResultRUSARecord
	{
		public int CountGA { get; set; }
		public List<RUSAResult> Data { get; set; }
	}

	public class RUSAData : INotifyPropertyChanged
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

		private List<GAParams> gaAvail;
		public List<GAParams> GaAvail {
			get { return gaAvail; }
			set { gaAvail = value; }
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
								
		public RUSAData() {
			gaAvail = new List<GAParams>();
			for (int ga=1; ga <= 10; ga++) {
				gaAvail.Add(new GAParams(ga,true));
			}
			power = 300;
			napor = 21;
		}

		public List<int> getAvailGenerators() {
			List<int> res=new List<int>();
			foreach (GAParams avail in gaAvail) {
				if (avail.Avail) {
					res.Add(avail.GaNumber);
				}
			}
			return res;
		}

		private List<RUSAResult> eqResult;
		public List<RUSAResult> EqResult {
			get { return eqResult; }
			set { eqResult = value; }
		}

		private List<RUSAResult> diffResult;
		public List<RUSAResult> DiffResult {
			get { return diffResult; }
			set { diffResult = value; }
		}


		private List<FullResultRUSARecord>fullResultList;
		public List<FullResultRUSARecord> FullResultList {
			get { return fullResultList; }
			set { fullResultList = value; }
		}
		

		
		private List<RUSAResult> result;
		public List<RUSAResult> Result {
			get { return result; }
			set { result = value; }
		}
		
	}
}