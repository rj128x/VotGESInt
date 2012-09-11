using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.PBR
{
	public class PBRFull
	{
		public PBRData GTP1 { get; protected set; }
		public PBRData GTP2 { get; protected set; }
		public PBRData GES { get; protected set; }
		public PBRData RGE2 { get; protected set; }
		public PBRData RGE3 { get; protected set; }
		public PBRData RGE4 { get; protected set; }
		public SortedList<DateTime, bool> ChangePBR { get; protected set; }
		
		public PBRFull(DateTime date) {
			GTP1 = new PBRData(date.Date, date.Date.AddDays(1), date.Date.AddDays(1), GTPEnum.gtp1);
			GTP2 = new PBRData(date.Date, date.Date.AddDays(1), date.Date.AddDays(1), GTPEnum.gtp2);
			GES = new PBRData(date.Date, date.Date.AddDays(1), date.Date.AddDays(1), GTPEnum.ges);
			RGE2 = new PBRData(date.Date, date.Date.AddDays(1), date.Date.AddDays(1), GTPEnum.rge2);
			RGE3 = new PBRData(date.Date, date.Date.AddDays(1), date.Date.AddDays(1), GTPEnum.rge3);
			RGE4 = new PBRData(date.Date, date.Date.AddDays(1), date.Date.AddDays(1), GTPEnum.rge4);			

			GTP1.InitData();
			GTP2.InitData();
			GES.InitData();
			RGE2.InitData();
			RGE3.InitData();
			RGE4.InitData();
			ChangePBR=new SortedList<DateTime,bool>();

			foreach (DateTime dt in GES.SteppedPBR.Keys) {
				bool change=GTP1.ChangePBR[dt] || GTP2.ChangePBR[dt] || GES.ChangePBR[dt] || RGE2.ChangePBR[dt] || RGE3.ChangePBR[dt] || RGE4.ChangePBR[dt];
				ChangePBR.Add(dt, change);
			}

		}
	}
}
