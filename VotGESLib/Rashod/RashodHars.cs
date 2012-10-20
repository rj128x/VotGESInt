using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Chart;

namespace VotGES.Rashod
{
	public enum RHChartType
	{
		GA_QotP, GA_KPDotP, GA_QotH, GA_KPDotH,
		CMPGA_QotP, CMPGA_KPDotP, CMPGA_QotH, CMPGA_KPDotH,
		CMPST_QotP, CMPST_KPDotP, CMPST_QotH, CMPST_KPDotH
	};
	public class RashodHars
	{
		public static int CountPoints=100;

		protected static ChartProperties getChartPropertiesNapors(double[] napors) {
			ChartProperties props=new ChartProperties();
			props.XAxisType = XAxisTypeEnum.numeric;
			props.XValueFormatString = "0.##";
			ChartAxisProperties yAx=new ChartAxisProperties();
			yAx.Auto = true;
			yAx.Index = 0;

			for (int index=0; index < napors.Length; index++) {
				string post=napors[index].ToString();
				ChartSerieProperties serie=new ChartSerieProperties();
				serie.Color = index == napors.Length - 1 ? "0-0-0" : ChartColor.NextColor();
				serie.LineWidth = index == napors.Length - 1 ? 2 : 1;
				serie.SerieType = ChartSerieType.line;
				serie.Title = "Напор " + post;
				serie.TagName = "Napor_" + index;
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
			props.XValueFormatString = "0.##";
			ChartAxisProperties yAx=new ChartAxisProperties();
			yAx.Auto = true;
			yAx.Index = 0;

			for (int index=0; index < powers.Length; index++) {
				string post=powers[index].ToString();
				ChartSerieProperties serie=new ChartSerieProperties();
				serie.Color = index == powers.Length - 1 ? "0-0-0" : ChartColor.NextColor();
				serie.LineWidth = index == powers.Length - 1 ? 2 : 1;
				serie.SerieType = ChartSerieType.line;
				serie.Title = "Мощность " + post;
				serie.TagName = "Power_" + index;
				serie.YAxisIndex = 0;
				props.addSerie(serie);
			}

			props.addAxis(yAx);
			return props;
		}

		protected static ChartProperties getChartPropertiesGA(int[] generators) {
			ChartProperties props=new ChartProperties();
			props.XAxisType = XAxisTypeEnum.numeric;
			props.XValueFormatString = "0.##";
			ChartAxisProperties yAx=new ChartAxisProperties();
			yAx.Auto = true;
			yAx.Index = 0;

			for (int index=0; index < generators.Length; index++) {
				int ga=generators[index];

				ChartSerieProperties serie=new ChartSerieProperties();
				serie.Color = ChartColor.NextColor();
				serie.LineWidth = 2;
				serie.SerieType = ChartSerieType.line;
				serie.Title = ga <= 10 ? "ГА-" + ga : (ga == 12 ? "Оптимальный" : "Средний");
				serie.TagName = "GA_" + index;
				serie.YAxisIndex = 0;
				props.addSerie(serie);
			}

			props.addAxis(yAx);
			return props;
		}

		protected static ChartProperties getChartPropertiesNaporsCMP(double[] napors) {
			ChartProperties props=new ChartProperties();
			props.XAxisType = XAxisTypeEnum.numeric;
			props.XValueFormatString = "0.##";
			ChartAxisProperties yAx=new ChartAxisProperties();
			yAx.Auto = true;
			yAx.Index = 0;

			for (int index=0; index < napors.Length; index++) {
				string color=index == napors.Length - 1 ? "0-0-0" : ChartColor.NextColor();
				string post=napors[index].ToString();
				ChartSerieProperties serie=new ChartSerieProperties();
				serie.Color = color;
				serie.LineWidth = 1;
				serie.SerieType = ChartSerieType.line;
				serie.Title = "Напор(опт) " + post;
				serie.TagName = "NaporOpt_" + index;
				serie.Enabled = true;
				serie.YAxisIndex = 0;
				props.addSerie(serie);

				serie = new ChartSerieProperties();
				serie.Color = color;
				serie.LineWidth = 2;
				serie.SerieType = ChartSerieType.line;
				serie.Title = "Напор(ср) " + post;
				serie.TagName = "NaporAvg_" + index;
				serie.Enabled = true;
				serie.YAxisIndex = 0;
				props.addSerie(serie);
			}

			props.addAxis(yAx);
			return props;
		}

		protected static ChartProperties getChartPropertiesPowersCMP(double[] powers) {
			ChartProperties props=new ChartProperties();
			props.XAxisType = XAxisTypeEnum.numeric;
			props.XValueFormatString = "0.##";
			ChartAxisProperties yAx=new ChartAxisProperties();
			yAx.Auto = true;
			yAx.Index = 0;

			for (int index=0; index < powers.Length; index++) {
				string color=index == powers.Length - 1 ? "0-0-0" : ChartColor.NextColor();
				string post=powers[index].ToString();
				ChartSerieProperties serie=new ChartSerieProperties();
				serie.Color = color;
				serie.LineWidth = 1;
				serie.SerieType = ChartSerieType.line;
				serie.Title = "Мощность(опт) " + post;
				serie.TagName = "PowerOpt_" + index;
				serie.YAxisIndex = 0;
				props.addSerie(serie);

				serie = new ChartSerieProperties();
				serie.Color = color;
				serie.LineWidth = 2;
				serie.SerieType = ChartSerieType.line;
				serie.Title = "Мощность(ср) " + post;
				serie.TagName = "PowerAvg_" + index;
				serie.YAxisIndex = 0;
				props.addSerie(serie);
			}

			props.addAxis(yAx);
			return props;
		}




		public static ChartAnswer GetGA_QotP(int ga, bool isKPD, double napor) {
			ChartAnswer answer=new ChartAnswer();
			if (ga == 11) {
				answer.Title = isKPD?"КПД (средний) при разных напорах":"Расходная характеристика станции (средняя) при разных напорах";
			} else if (ga == 12) {
				answer.Title = isKPD ? "КПД (опт) при разных напорах" : "Расходная характеристика станции (опт) при разных напорах";
			} else {
				answer.Title = isKPD ? "КПД ГА-{0} при разных напорах" : "Расходная характеристика ГА-{0} при разных напорах";
				answer.Title = String.Format(answer.Title, ga);
			}
			double[]napors=new double[] { 16, 17, 18, 19, 20, 21, 22, napor };
			answer.Properties = getChartPropertiesNapors(napors);
			answer.Data = new ChartData();
			RashodTable table=RashodTable.getRashodTable(ga);
			double step=(table.maxPower - table.minPower) / CountPoints;

			for (int index=0; index < napors.Length; index++) {
				ChartDataSerie data=new ChartDataSerie();
				data.Name = "Napor_" + index;
				double power=table.minPower;
				while (power <= table.maxPower) {
					double rashod=RashodTable.getRashod(ga, power, napors[index]);
					if (rashod > 0) {
						double val=isKPD ? RashodTable.KPD(power, napors[index], rashod) : rashod;
						data.Points.Add(new ChartDataPoint(power, val));
					}
					power += step;
				}
				answer.Data.addSerie(data);
			}
			return answer;
		}


		public static ChartAnswer GetGA_QotH(int ga, bool isKPD, double power) {
			ChartAnswer answer=new ChartAnswer();
			if (ga == 11) {
				answer.Title = isKPD ? "КПД (средний) при разных мощностях" : "Расходная характеристика станции (средняя) при разных мощностях";
			} else if (ga == 12) {
				answer.Title = isKPD ? "КПД (опт) при разных мощностях" : "Расходная характеристика станции (опт) при разных мощностях";
			} else {
				answer.Title = isKPD ? "КПД ГА-{0} при разных мощностях" : "Расходная характеристика ГА-{0} при разных мощностях";
				answer.Title = String.Format(answer.Title, ga);
			}

			double[]powers;
			if (ga > 10) {
				powers = new double[] { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, power };
			} else {
				powers = new double[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, power };
			}
			answer.Properties = getChartPropertiesPowers(powers);
			answer.Data = new ChartData();
			RashodTable table=RashodTable.getRashodTable(ga);
			double step=(table.maxNapor - table.minNapor) / CountPoints;

			for (int index=0; index < powers.Length; index++) {
				ChartDataSerie data=new ChartDataSerie();
				data.Name = "Power_" + index;
				double napor=table.minNapor;
				while (napor <= table.maxNapor) {
					double rashod=RashodTable.getRashod(ga, powers[index], napor);
					if (rashod > 0) {
						double val=isKPD ? RashodTable.KPD(powers[index], napor, rashod) : rashod;
						data.Points.Add(new ChartDataPoint(napor, val));
					}
					napor += step;
				}
				answer.Data.addSerie(data);
			}
			return answer;
		}

		public static ChartAnswer GetCMPGA_QotP(int[] generators, bool isKPD, double napor) {
			ChartAnswer answer=new ChartAnswer();
			answer.Title = isKPD ? "КПД генераторов при напоре {0}" : "Расходная характеристика генераторов при напоре {0}";
			answer.Title = String.Format(answer.Title, napor);
			answer.Properties = getChartPropertiesGA(generators);
			answer.Data = new ChartData();
			RashodTable table=RashodTable.getRashodTable(generators[0]);
			double step=(table.maxPower - table.minPower) / CountPoints;

			for (int index=0; index < generators.Length; index++) {
				int ga=generators[index];
				ChartDataSerie data=new ChartDataSerie();
				data.Name = "GA_" + index;
				double power=table.minPower;
				while (power <= table.maxPower) {
					double rashod=RashodTable.getRashod(ga, power, napor);
					if (rashod > 0) {
						double val=isKPD ? RashodTable.KPD(power, napor, rashod) : rashod;
						data.Points.Add(new ChartDataPoint(power, val));
					}
					power += step;
				}
				answer.Data.addSerie(data);
			}
			return answer;
		}

		public static ChartAnswer GetCMPGA_QotH(int[] generators, bool isKPD, double power) {
			ChartAnswer answer=new ChartAnswer();
			answer.Title = isKPD ? "КПД генераторов при мощности {0}" : "Расходная характеристика генераторов при мощности {0}";
			answer.Title = String.Format(answer.Title, power);
			answer.Properties = getChartPropertiesGA(generators);
			answer.Data = new ChartData();
			RashodTable table=RashodTable.getRashodTable(generators[0]);
			double step=(table.maxNapor - table.minNapor) / CountPoints;

			for (int index=0; index < generators.Length; index++) {
				int ga=generators[index];
				ChartDataSerie data=new ChartDataSerie();
				data.Name = "GA_" + index;
				double napor=table.minNapor;
				while (napor <= table.maxNapor) {
					double rashod=RashodTable.getRashod(ga, power, napor);
					if (rashod > 0) {
						double val=isKPD ? RashodTable.KPD(power, napor, rashod) : rashod;
						data.Points.Add(new ChartDataPoint(napor, val));
					}
					napor += step;
				}
				answer.Data.addSerie(data);
			}
			return answer;
		}

		public static ChartAnswer GetCMPST_QotP(bool isKPD, double napor) {
			ChartAnswer answer=new ChartAnswer();
			answer.Title = isKPD ? "Оптимальный и средний КПД станции при разных напорах" : "Оптимальная и средняя расходная характеристика станции при разных напорах";

			double[]napors=new double[] { 16, 17, 18, 19, 20, 21, 22, napor };
			answer.Properties = getChartPropertiesNaporsCMP(napors);
			answer.Data = new ChartData();
			RashodTable table=RashodTable.getRashodTable(11);
			double step=(table.maxPower - table.minPower) / CountPoints;

			for (int index=0; index < napors.Length; index++) {
				ChartDataSerie dataOpt=new ChartDataSerie();
				dataOpt.Name = "NaporOpt_" + index;
				ChartDataSerie dataAvg=new ChartDataSerie();
				dataAvg.Name = "NaporAvg_" + index;
				double power=table.minPower;
				while (power <= table.maxPower) {
					double rashodOpt=RashodTable.getRashod(12, power, napors[index]);
					double rashodAvg=RashodTable.getRashod(11, power, napors[index]);
					if (rashodOpt > 0 && rashodAvg > 0) {
						double valOpt=isKPD ? RashodTable.KPD(power, napors[index], rashodOpt) : rashodOpt;
						double valAvg=isKPD ? RashodTable.KPD(power, napors[index], rashodAvg) : rashodAvg;
						dataOpt.Points.Add(new ChartDataPoint(power, valOpt));
						dataAvg.Points.Add(new ChartDataPoint(power, valAvg));
					}
					power += step;
				}
				answer.Data.addSerie(dataOpt);
				answer.Data.addSerie(dataAvg);
			}
			return answer;
		}


		public static ChartAnswer GetCMPST_QotH(bool isKPD, double power) {
			ChartAnswer answer=new ChartAnswer();
			answer.Title = isKPD ? "Оптимальный и средний КПД станции при разных мощностях" : "Оптимальная и средняя расходная характеристика станции при разных мощностях";
			double[]powers = new double[] { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, power };
			answer.Properties = getChartPropertiesPowersCMP(powers);
			answer.Data = new ChartData();
			RashodTable table=RashodTable.getRashodTable(11);
			double step=(table.maxNapor - table.minNapor) / CountPoints;

			for (int index=0; index < powers.Length; index++) {
				ChartDataSerie dataOpt=new ChartDataSerie();
				dataOpt.Name = "PowerOpt_" + index;
				ChartDataSerie dataAvg=new ChartDataSerie();
				dataAvg.Name = "PowerAvg_" + index;
				double napor=table.minNapor;
				while (napor <= table.maxNapor) {
					double rashodOpt=RashodTable.getRashod(12, powers[index], napor);
					double rashodAvg=RashodTable.getRashod(11, powers[index], napor);
					if (rashodOpt > 0 && rashodAvg > 0) {
						double valOpt=isKPD ? RashodTable.KPD(powers[index], napor, rashodOpt) : rashodOpt;
						double valAvg=isKPD ? RashodTable.KPD(powers[index], napor, rashodAvg) : rashodAvg;
						dataOpt.Points.Add(new ChartDataPoint(napor, valOpt));
						dataAvg.Points.Add(new ChartDataPoint(napor, valAvg));
					}
					napor += step;
				}
				answer.Data.addSerie(dataOpt);
				answer.Data.addSerie(dataAvg);
			}
			return answer;
		}

	}
}

