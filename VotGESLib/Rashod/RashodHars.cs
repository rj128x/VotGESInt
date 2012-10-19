using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Chart;

namespace VotGES.Rashod
{
	public enum RHChartType{GA_QotP,GA_KPDotP,GA_QotH,GA_KPDotH,
		CMPGA_QotP,CMPGA_KPDotP,CMPGA_QotH,CMPGA_KPDotH,
		CMPST_QotP, CMPST_KPDotP, CMPST_QotH, CMPST_KPDotH
	};
	public class RashodHars
	{
		public static int CountPoints=100;
		
		protected static ChartProperties getChartPropertiesNapors(double[]napors){
			ChartProperties props=new ChartProperties();
			props.XAxisType = XAxisTypeEnum.numeric;
			props.XValueFormatString = "0.##";
			ChartAxisProperties yAx=new ChartAxisProperties();			
			yAx.Auto = true;
			yAx.Index = 0;
			
			for (int index=0;index<napors.Length;index++) {
				string post=index == 0 ? "" : napors[index].ToString("#.##");
				ChartSerieProperties serie=new ChartSerieProperties();
				serie.Color = index == 0 ? "0-0-0" : ChartColor.NextColor();
				serie.LineWidth = index == 0 ? 2 : 1;
				serie.SerieType = ChartSerieType.line;
				serie.Title = "Напор " + post;
				serie.TagName = "Napor_"+index;
				serie.Enabled = true;
				serie.YAxisIndex = 0;
				props.addSerie(serie);
			}
			
			props.addAxis(yAx);
			return props;
		}

		protected static ChartProperties getChartPropertiesPowers(double[] powers) {
			ChartProperties props=new ChartProperties();
			props.XAxisType = XAxisTypeEnum.numeric;
			ChartAxisProperties yAx=new ChartAxisProperties();
			yAx.Auto = true;
			yAx.Index = 0;

			for (int index=0; index < powers.Length; index++) {
				string post=index == 0 ? "" : powers[index].ToString("#.##");
				ChartSerieProperties serie=new ChartSerieProperties();
				serie.Color = index == 0 ? "0-0-0" : ChartColor.NextColor();
				serie.LineWidth = index == 0 ? 2 : 1;
				serie.SerieType = ChartSerieType.line;
				serie.Title = "Мощность " + post;
				serie.TagName = "Power_" + index;
				serie.YAxisIndex = 0;
				props.addSerie(serie);
			}

			props.addAxis(yAx);
			return props;
		}



		public static ChartAnswer GetGA_QotP(int ga, bool isKPD, double napor) {
			Logger.Info(ga.ToString());
			ChartAnswer answer=new ChartAnswer();
			double[]napors=new double[] { napor, 16, 17, 18, 19, 20, 21, 22 };
			answer.Properties = getChartPropertiesNapors(napors);
			answer.Data = new ChartData();
			RashodTable table=RashodTable.getRashodTable(ga);
			double step=(table.maxPower - table.minPower) / CountPoints;

			Logger.Info(table.minPower.ToString());
			Logger.Info(table.maxPower.ToString());
			for (int index=0; index < napors.Length; index++) {
				ChartDataSerie data=new ChartDataSerie();
				data.Name = "Napor_" + index;
				double power=table.minPower;
				while (power <= table.maxPower) {
					double rashod=RashodTable.getRashod(ga,power,napors[index]);
					if (rashod > 0) {
						double val=isKPD ? RashodTable.KPD(power, napors[index], rashod) : rashod;
						data.Points.Add(new ChartDataPoint(power, val));
					}
					power += step;
				}
				Logger.Info(data.Points.Count.ToString());
				answer.Data.addSerie(data);
			}
			return answer;
		}

	}
}

