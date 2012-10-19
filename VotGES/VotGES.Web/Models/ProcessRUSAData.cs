using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VotGES.Rashod;

namespace VotGES.Web.Models
{
	public class ProcessRUSAData
	{
		public static void processEqualData(RUSAData data) {
			SortedList<double,List<int>> sostavs=RUSA.getOptimRashodsFull(data.Power, data.Napor, data.getAvailGenerators());
			int index=0;
			data.EqResult = new List<RUSAResult>();
			Dictionary<int, FullResultRUSARecord> FullResult = new Dictionary<int, FullResultRUSARecord>();
			try {
				foreach (KeyValuePair<double, List<int>> de in sostavs) {
					index++;
					double rashod=de.Key;
					List<int> sostav=de.Value;
					RUSAResult result=new RUSAResult();
					result.Rashod = rashod;
					result.Sostav = new Dictionary<int, double>();
					foreach (int ga in sostav) {
						result.Sostav.Add(ga, data.Power / sostav.Count);
					}
					result.ProcessSostav(result.Sostav);
					result.Sostav = null;

					result.KPD = RashodTable.KPD(data.Power, data.Napor, rashod) * 100;
					result.Count = sostav.Count;

					if (!FullResult.ContainsKey(sostav.Count)) {
						data.EqResult.Add(result);
						FullResult.Add(sostav.Count, new FullResultRUSARecord());
						FullResult[sostav.Count].Data = new List<RUSAResult>();
					}
					FullResult[sostav.Count].Data.Add(result);
					FullResult[sostav.Count].CountGA = sostav.Count;
				}
				data.FullResultList = FullResult.Values.ToList();
			} catch (Exception e) {
				Logger.Error("Ошибка при расчете РУСА ");
				Logger.Error(e.ToString());
			}
		}

		public static void processDiffData(RUSAData data) {
			List<RUSADiffPower.RusaChoice> choices=RUSADiffPower.getChoices(data.getAvailGenerators(), data.Napor, data.Power,5);
			data.DiffResult = new List<RUSAResult>();
			foreach (RUSADiffPower.RusaChoice choice in choices) {
				double rashod=choice.rashod;				
				RUSAResult result=new RUSAResult();
				result.Rashod = rashod;
				result.Sostav = new Dictionary<int, double>();
				int count=0;
				foreach (KeyValuePair<int, double> de in choice.sostav) {
					if (de.Value > 0) {
						result.Sostav.Add(de.Key, de.Value);
						count++;
					}
				}
				result.ProcessSostav(result.Sostav);
				result.Sostav = null;
				result.KPD = RashodTable.KPD(data.Power, data.Napor, rashod)*100;
				result.Count = count;
				data.DiffResult.Add(result);
			}			
		}


	}
}