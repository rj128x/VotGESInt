using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Chart;

namespace VotGES.PBR
{
	public class GraphVyrabRGETableRow
	{
		public double RGE1 { get; set; }
		public double RGE2 { get; set; }
		public double RGE3 { get; set; }
		public double RGE4 { get; set; }
		public string Title { get; set; }
		public string Format { get; set; }

		public GraphVyrabRGETableRow(String title, double rge1, double rge2, double rge3, double rge4) {
			RGE1 = rge1;
			RGE2 = rge2;
			RGE3 = rge3;
			RGE4 = rge4;
			Title = title;
		}

		public GraphVyrabRGETableRow() {
		}

	}

	public class GraphVyrabRGEAnswer
	{
		public ChartAnswer ChartRGE1 { get; set; }
		public ChartAnswer ChartRGE2 { get; set; }
		public ChartAnswer ChartRGE3 { get; set; }
		public ChartAnswer ChartRGE4 { get; set; }
		public DateTime ActualDate { get; set; }
		public List<GraphVyrabRGETableRow> TableCurrent { get; set; }
		public List<GraphVyrabRGETableRow> TableHour { get; set; }

		public GraphVyrabRGEAnswer() {
			TableCurrent = new List<GraphVyrabRGETableRow>();
			TableHour = new List<GraphVyrabRGETableRow>();
		}
	}

	public class CheckGraphVyrabRGETableRow
	{
		public double RGE1Fakt { get; set; }
		public double RGE2Fakt { get; set; }
		public double RGE3Fakt { get; set; }
		public double RGE4Fakt { get; set; }

		public double RGE1Plan { get; set; }
		public double RGE2Plan { get; set; }
		public double RGE3Plan { get; set; }
		public double RGE4Plan { get; set; }

		public double RGE1Diff { get; set; }
		public double RGE2Diff { get; set; }
		public double RGE3Diff { get; set; }
		public double RGE4Diff { get; set; }

		public double RGE1DiffProc { get; set; }
		public double RGE2DiffProc { get; set; }
		public double RGE3DiffProc { get; set; }
		public double RGE4DiffProc { get; set; }

		public string Title { get; set; }
	}

	public class CheckGraphVyrabRGEAnswer
	{
		public ChartAnswer ChartRGE1 { get; set; }
		public ChartAnswer ChartRGE2 { get; set; }
		public ChartAnswer ChartRGE3 { get; set; }
		public ChartAnswer ChartRGE4 { get; set; }

		public List<CheckGraphVyrabRGETableRow> TableHH { get; set; }
		public List<CheckGraphVyrabRGETableRow> TableH { get; set; }

		public CheckGraphVyrabRGEAnswer() {
			TableHH = new List<CheckGraphVyrabRGETableRow>();
			TableH = new List<CheckGraphVyrabRGETableRow>();
		}
	}


	public class GraphVyrabRGE
	{
		public static GraphVyrabRGEAnswer getAnswer(DateTime date, bool calcTables = true, bool steppedPBR=true) {
			DateTime dateStart=date.Date;
			DateTime dateEnd=date.Date.AddHours(24);
			date = calcTables ? date : dateEnd;

			GraphVyrabRGEAnswer answer=new GraphVyrabRGEAnswer();


			PBRData rge1=new PBRData(dateStart, dateEnd, date, GTPEnum.rge1);
			PBRData rge2=new PBRData(dateStart, dateEnd, date, GTPEnum.rge2);
			PBRData rge3=new PBRData(dateStart, dateEnd, date, GTPEnum.rge3);
			PBRData rge4=new PBRData(dateStart, dateEnd, date, GTPEnum.rge4);
			rge1.IsSteppedPBR = steppedPBR;
			rge2.IsSteppedPBR = steppedPBR;
			rge3.IsSteppedPBR = steppedPBR;
			rge4.IsSteppedPBR = steppedPBR;

			answer.ChartRGE1 = new ChartAnswer();
			answer.ChartRGE1.Properties = getChartProperties(220, steppedPBR);
			answer.ChartRGE1.Data = new ChartData();

			answer.ChartRGE2 = new ChartAnswer();
			answer.ChartRGE2.Properties = getChartProperties(200, steppedPBR);
			answer.ChartRGE2.Data = new ChartData();

			answer.ChartRGE3 = new ChartAnswer();
			answer.ChartRGE3.Properties = getChartProperties(200, steppedPBR);
			answer.ChartRGE3.Data = new ChartData();

			answer.ChartRGE4 = new ChartAnswer();
			answer.ChartRGE4.Properties = getChartProperties(400, steppedPBR);
			answer.ChartRGE4.Data = new ChartData();


			rge1.InitData();
			rge2.InitData();
			rge3.InitData();
			rge4.InitData();

			DateTime[] dates=new DateTime[] { rge1.Date, rge2.Date, rge3.Date, rge4.Date };
			answer.ActualDate = dates.ToList().Min(); ;

			DateTime lastDate=answer.ActualDate;

			if (calcTables) {
				answer.TableCurrent.Add(new GraphVyrabRGETableRow("P план", Math.Round(rge1.MinutesPBR[lastDate]), Math.Round(rge2.MinutesPBR[lastDate]), Math.Round(rge3.MinutesPBR[lastDate]), Math.Round(rge4.MinutesPBR[lastDate])));
				answer.TableCurrent.Add(new GraphVyrabRGETableRow("P факт", Math.Round(rge1.RealP[lastDate]), Math.Round(rge2.RealP[lastDate]), Math.Round(rge3.RealP[lastDate]), Math.Round(rge4.RealP[lastDate])));
				answer.TableCurrent.Add(new GraphVyrabRGETableRow("P откл", rge1.getDiff(lastDate), rge2.getDiff(lastDate), rge3.getDiff(lastDate), rge4.getDiff(lastDate)));
				answer.TableCurrent.Add(new GraphVyrabRGETableRow("P откл %", rge1.getDiffProc(lastDate), rge2.getDiffProc(lastDate), rge3.getDiffProc(lastDate), rge4.getDiffProc(lastDate)));


				SortedList<string,double> rge1Hour=rge1.getHourVals(lastDate);
				SortedList<string,double> rge2Hour=rge2.getHourVals(lastDate);
				SortedList<string,double> rge3Hour=rge3.getHourVals(lastDate);
				SortedList<string,double> rge4Hour=rge4.getHourVals(lastDate);

				answer.TableHour.Add(new GraphVyrabRGETableRow("P план", Math.Round(rge1Hour["plan"]), Math.Round(rge2Hour["plan"]), Math.Round(rge3Hour["plan"]), Math.Round(rge4Hour["plan"])));
				answer.TableHour.Add(new GraphVyrabRGETableRow("P факт", Math.Round(rge1Hour["fakt"]), Math.Round(rge2Hour["fakt"]), Math.Round(rge3Hour["fakt"]), Math.Round(rge4Hour["fakt"])));
				answer.TableHour.Add(new GraphVyrabRGETableRow("P откл", rge1Hour["diff"], rge2Hour["diff"], rge3Hour["diff"], rge4Hour["diff"]));
				answer.TableHour.Add(new GraphVyrabRGETableRow("P откл %", rge1Hour["diffProc"], rge2Hour["diffProc"], rge3Hour["diffProc"], rge4Hour["diffProc"]));
				answer.TableHour.Add(new GraphVyrabRGETableRow("P рек", Math.Round(rge1Hour["recP"]), Math.Round(rge2Hour["recP"]), Math.Round(rge3Hour["recP"]), Math.Round(rge4Hour["recP"])));

			}


			answer.ChartRGE1.Data.addSerie(getDataSerie("Fakt", rge1.RealP, -1));
			answer.ChartRGE1.Data.addSerie(getDataSerie("Plan", steppedPBR ? rge1.SteppedPBR : rge1.RealPBR, 0));

			answer.ChartRGE2.Data.addSerie(getDataSerie("Fakt", rge2.RealP, -1));
			answer.ChartRGE2.Data.addSerie(getDataSerie("Plan", steppedPBR ? rge2.SteppedPBR : rge2.RealPBR, 0));

			answer.ChartRGE3.Data.addSerie(getDataSerie("Fakt", rge3.RealP, -1));
			answer.ChartRGE3.Data.addSerie(getDataSerie("Plan", steppedPBR ? rge3.SteppedPBR : rge3.RealPBR, 0));

			answer.ChartRGE4.Data.addSerie(getDataSerie("Fakt", rge4.RealP, -1));
			answer.ChartRGE4.Data.addSerie(getDataSerie("Plan", steppedPBR ? rge4.SteppedPBR : rge4.RealPBR, 0));

			answer.ChartRGE1.processAxes();
			answer.ChartRGE2.processAxes();
			answer.ChartRGE3.processAxes();
			answer.ChartRGE4.processAxes();

			return answer;
		}

		public static CheckGraphVyrabRGEAnswer getAnswerHH(DateTime date) {
			DateTime dateStart=date.Date;
			DateTime dateEnd=date.Date.AddHours(24);

			CheckGraphVyrabRGEAnswer answer=new CheckGraphVyrabRGEAnswer();


			PBRDataHH rge1=new PBRDataHH(dateStart, dateEnd, GTPEnum.rge1);
			PBRDataHH rge2=new PBRDataHH(dateStart, dateEnd, GTPEnum.rge2);
			PBRDataHH rge3=new PBRDataHH(dateStart, dateEnd, GTPEnum.rge3);
			PBRDataHH rge4=new PBRDataHH(dateStart, dateEnd, GTPEnum.rge4);

			answer.ChartRGE1 = new ChartAnswer();
			answer.ChartRGE1.Properties = getChartProperties(220,true);
			answer.ChartRGE1.Data = new ChartData();

			answer.ChartRGE2 = new ChartAnswer();
			answer.ChartRGE2.Properties = getChartProperties(200,true);
			answer.ChartRGE2.Data = new ChartData();

			answer.ChartRGE3 = new ChartAnswer();
			answer.ChartRGE3.Properties = getChartProperties(200,true);
			answer.ChartRGE3.Data = new ChartData();

			answer.ChartRGE4 = new ChartAnswer();
			answer.ChartRGE4.Properties = getChartProperties(400,true);
			answer.ChartRGE4.Data = new ChartData();

			rge1.InitData();
			rge2.InitData();
			rge3.InitData();
			rge4.InitData();

			CheckGraphVyrabRGETableRow rowFull=new CheckGraphVyrabRGETableRow();
			rowFull.Title = "Итог";
			CheckGraphVyrabRGETableRow rowMin=new CheckGraphVyrabRGETableRow();
			rowMin.Title = "Итог -";
			CheckGraphVyrabRGETableRow rowPl=new CheckGraphVyrabRGETableRow();
			rowPl.Title = "Итог +";

			answer.TableH.Add(rowFull);
			answer.TableH.Add(rowMin);
			answer.TableH.Add(rowPl);

			answer.TableHH.Add(rowFull);
			answer.TableHH.Add(rowMin);
			answer.TableHH.Add(rowPl);


			foreach (DateTime dt in rge1.HalfHoursPBR.Keys) {
				CheckGraphVyrabRGETableRow row=new CheckGraphVyrabRGETableRow();

				row.Title = dt.ToString("dd.MM.yy HH:mm");
				row.RGE1Fakt = rge1.HalfHoursP[dt];
				row.RGE1Plan = rge1.HalfHoursPBR[dt];
				row.RGE1Diff = rge1.HalfHoursP[dt] - rge1.HalfHoursPBR[dt];
				row.RGE1DiffProc = PBRData.getDiffProc(rge1.HalfHoursP[dt], rge1.HalfHoursPBR[dt]);

				row.RGE2Fakt = rge2.HalfHoursP[dt];
				row.RGE2Plan = rge2.HalfHoursPBR[dt];
				row.RGE2Diff = rge2.HalfHoursP[dt] - rge2.HalfHoursPBR[dt];
				row.RGE2DiffProc = PBRData.getDiffProc(rge2.HalfHoursP[dt], rge2.HalfHoursPBR[dt]);

				row.RGE3Fakt = rge3.HalfHoursP[dt];
				row.RGE3Plan = rge3.HalfHoursPBR[dt];
				row.RGE3Diff = rge3.HalfHoursP[dt] - rge3.HalfHoursPBR[dt];
				row.RGE3DiffProc = PBRData.getDiffProc(rge3.HalfHoursP[dt], rge3.HalfHoursPBR[dt]);

				row.RGE4Fakt = rge4.HalfHoursP[dt];
				row.RGE4Plan = rge4.HalfHoursPBR[dt];
				row.RGE4Diff = rge4.HalfHoursP[dt] - rge4.HalfHoursPBR[dt];
				row.RGE4DiffProc = PBRData.getDiffProc(rge4.HalfHoursP[dt], rge4.HalfHoursPBR[dt]);

				rowMin.RGE1Fakt += row.RGE1Fakt / 2; rowPl.RGE1Fakt += row.RGE1Fakt / 2; rowFull.RGE1Fakt += row.RGE1Fakt / 2;
				rowMin.RGE1Plan += row.RGE1Plan / 2; rowPl.RGE1Plan += row.RGE1Plan / 2; rowFull.RGE1Plan += row.RGE1Plan / 2;
				rowMin.RGE1Diff += row.RGE1Diff < 0 ? row.RGE1Diff / 2 : 0;
				rowPl.RGE1Diff += row.RGE1Diff > 0 ? row.RGE1Diff / 2 : 0;
				rowFull.RGE1Diff += row.RGE1Diff / 2;

				rowMin.RGE2Fakt += row.RGE2Fakt / 2; rowPl.RGE2Fakt += row.RGE2Fakt / 2; rowFull.RGE2Fakt += row.RGE2Fakt / 2;
				rowMin.RGE2Plan += row.RGE2Plan / 2; rowPl.RGE2Plan += row.RGE2Plan / 2; rowFull.RGE2Plan += row.RGE2Plan / 2;
				rowMin.RGE2Diff += row.RGE2Diff < 0 ? row.RGE2Diff / 2 : 0;
				rowPl.RGE2Diff += row.RGE2Diff > 0 ? row.RGE2Diff / 2 : 0;
				rowFull.RGE2Diff += row.RGE2Diff / 2;

				rowMin.RGE3Fakt += row.RGE3Fakt / 2; rowPl.RGE3Fakt += row.RGE3Fakt / 2; rowFull.RGE3Fakt += row.RGE3Fakt / 2;
				rowMin.RGE3Plan += row.RGE3Plan / 2; rowPl.RGE3Plan += row.RGE3Plan / 2; rowFull.RGE3Plan += row.RGE3Plan / 2;
				rowMin.RGE3Diff += row.RGE3Diff < 0 ? row.RGE3Diff / 2 : 0;
				rowPl.RGE3Diff += row.RGE3Diff > 0 ? row.RGE3Diff / 2 : 0;
				rowFull.RGE3Diff += row.RGE3Diff / 2;

				rowMin.RGE4Fakt += row.RGE4Fakt / 2; rowPl.RGE4Fakt += row.RGE4Fakt / 2; rowFull.RGE4Fakt += row.RGE4Fakt / 2;
				rowMin.RGE4Plan += row.RGE4Plan / 2; rowPl.RGE4Plan += row.RGE4Plan / 2; rowFull.RGE4Plan += row.RGE4Plan / 2;
				rowMin.RGE4Diff += row.RGE4Diff < 0 ? row.RGE4Diff / 2 : 0;
				rowPl.RGE4Diff += row.RGE4Diff > 0 ? row.RGE4Diff / 2 : 0;
				rowFull.RGE4Diff += row.RGE4Diff / 2;


				answer.TableHH.Add(row);
			}

			rowMin.RGE1DiffProc = PBRData.getDiffProcDiff(rowMin.RGE1Diff, rowMin.RGE1Plan);
			rowPl.RGE1DiffProc = PBRData.getDiffProcDiff(rowPl.RGE1Diff, rowPl.RGE1Plan);
			rowFull.RGE1DiffProc = PBRData.getDiffProc(rowFull.RGE1Fakt, rowFull.RGE1Plan);

			rowMin.RGE2DiffProc = PBRData.getDiffProcDiff(rowMin.RGE2Diff, rowMin.RGE2Plan);
			rowPl.RGE2DiffProc = PBRData.getDiffProcDiff(rowPl.RGE2Diff, rowPl.RGE2Plan);
			rowFull.RGE2DiffProc = PBRData.getDiffProc(rowFull.RGE2Fakt, rowFull.RGE2Plan);

			rowMin.RGE3DiffProc = PBRData.getDiffProcDiff(rowMin.RGE3Diff, rowMin.RGE3Plan);
			rowPl.RGE3DiffProc = PBRData.getDiffProcDiff(rowPl.RGE3Diff, rowPl.RGE3Plan);
			rowFull.RGE3DiffProc = PBRData.getDiffProc(rowFull.RGE3Fakt, rowFull.RGE3Plan);

			rowMin.RGE4DiffProc = PBRData.getDiffProcDiff(rowMin.RGE4Diff, rowMin.RGE4Plan);
			rowPl.RGE4DiffProc = PBRData.getDiffProcDiff(rowPl.RGE4Diff, rowPl.RGE4Plan);
			rowFull.RGE4DiffProc = PBRData.getDiffProc(rowFull.RGE4Fakt, rowFull.RGE4Plan);


			foreach (DateTime dt in rge1.HoursPBR.Keys) {
				CheckGraphVyrabRGETableRow row=new CheckGraphVyrabRGETableRow();

				row.Title = dt.ToString("dd.MM.yy HH:mm");
				row.RGE1Fakt = rge1.HoursP[dt];
				row.RGE1Plan = rge1.HoursPBR[dt];
				row.RGE1Diff = rge1.HoursP[dt] - rge1.HoursPBR[dt];
				row.RGE1DiffProc = PBRData.getDiffProc(rge1.HoursP[dt], rge1.HoursPBR[dt]);

				row.RGE2Fakt = rge2.HoursP[dt];
				row.RGE2Plan = rge2.HoursPBR[dt];
				row.RGE2Diff = rge2.HoursP[dt] - rge2.HoursPBR[dt];
				row.RGE2DiffProc = PBRData.getDiffProc(rge2.HoursP[dt], rge2.HoursPBR[dt]);

				row.RGE3Fakt = rge3.HoursP[dt];
				row.RGE3Plan = rge3.HoursPBR[dt];
				row.RGE3Diff = rge3.HoursP[dt] - rge3.HoursPBR[dt];
				row.RGE3DiffProc = PBRData.getDiffProc(rge3.HoursP[dt], rge3.HoursPBR[dt]);

				row.RGE4Fakt = rge4.HoursP[dt];
				row.RGE4Plan = rge4.HoursPBR[dt];
				row.RGE4Diff = rge4.HoursP[dt] - rge4.HoursPBR[dt];
				row.RGE4DiffProc = PBRData.getDiffProc(rge4.HoursP[dt], rge4.HoursPBR[dt]);

				answer.TableH.Add(row);
			}



			answer.ChartRGE1.Data.addSerie(getDataSerie("Fakt", rge1.HalfHoursP, -30));
			answer.ChartRGE1.Data.addSerie(getDataSerie("Plan", rge1.HalfHoursPBR, -30));

			answer.ChartRGE2.Data.addSerie(getDataSerie("Fakt", rge2.HalfHoursP, -30));
			answer.ChartRGE2.Data.addSerie(getDataSerie("Plan", rge2.HalfHoursPBR, -30));

			answer.ChartRGE3.Data.addSerie(getDataSerie("Fakt", rge3.HalfHoursP, -30));
			answer.ChartRGE3.Data.addSerie(getDataSerie("Plan", rge3.HalfHoursPBR, -30));

			answer.ChartRGE4.Data.addSerie(getDataSerie("Fakt", rge4.HalfHoursP, -30));
			answer.ChartRGE4.Data.addSerie(getDataSerie("Plan", rge4.HalfHoursPBR, -30));

			answer.ChartRGE1.processAxes();
			answer.ChartRGE2.processAxes();
			answer.ChartRGE3.processAxes();
			answer.ChartRGE4.processAxes();

			return answer;
		}





		public static ChartDataSerie getDataSerie(string serieName, SortedList<DateTime, double> data, int correctTime) {
			ChartDataSerie serie=new ChartDataSerie();
			serie.Name = serieName;
			foreach (KeyValuePair<DateTime,double> de in data) {
				serie.Points.Add(new ChartDataPoint(de.Key.AddMinutes(correctTime), de.Value));
			}
			return serie;
		}

		public static ChartProperties getChartProperties(int max, bool steppedPBR) {
			ChartProperties props=new ChartProperties();
			props.XAxisType = XAxisTypeEnum.datetime;
			props.XValueFormatString = "dd.MM HH:mm";

			ChartAxisProperties pAx=new ChartAxisProperties();
			pAx.ProcessAuto = true;
			pAx.Auto = true;
			pAx.Min = 0;
			pAx.Max = max;
			pAx.Interval = 10;
			pAx.MinHeight = 10;
			pAx.Index = 0;

			ChartAxisProperties vAx=new ChartAxisProperties();
			vAx.Auto = true;
			vAx.Index = 1;

			props.addAxis(pAx);
			props.addAxis(vAx);

			ChartSerieProperties FaktSerie=new ChartSerieProperties();
			FaktSerie.Color = "0-0-255";
			FaktSerie.Title = "Факт";
			FaktSerie.TagName = "Fakt";
			FaktSerie.LineWidth = 2;
			FaktSerie.SerieType = ChartSerieType.stepLine;
			FaktSerie.YAxisIndex = 0;
			FaktSerie.Enabled = true;

			ChartSerieProperties PlanSerie=new ChartSerieProperties();
			PlanSerie.Color = "0-255-0";
			PlanSerie.Title = "План";
			PlanSerie.TagName = "Plan";
			PlanSerie.LineWidth = 1;
			PlanSerie.SerieType = steppedPBR ? ChartSerieType.stepLine : ChartSerieType.line;
			PlanSerie.YAxisIndex = 0;
			PlanSerie.Enabled = true;

			props.addSerie(FaktSerie);
			props.addSerie(PlanSerie);



			return props;
		}
	}
}
