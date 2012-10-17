using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Chart;

namespace VotGES.PBR
{
	public class GraphVyrabRGETableRow
	{
		public double RGE1{ get; set; }
		public double RGE2{ get; set; }
		public double RGE3{ get; set; }
		public double RGE4{ get; set; }
		public string Title { get; set; }
		public string Format { get; set; }

		public GraphVyrabRGETableRow(String title, double rge1, double rge2, double rge3, double rge4) {
			RGE1=rge1;
			RGE2=rge2;
			RGE3=rge3;
			RGE4=rge4;
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
		public static GraphVyrabRGEAnswer getAnswer(DateTime date, bool calcTables = true) {
			DateTime dateStart=date.Date;
			DateTime dateEnd=date.Date.AddHours(24);
			date = calcTables ? date : dateEnd;

			GraphVyrabRGEAnswer answer=new GraphVyrabRGEAnswer();


			PBRData rge1=new PBRData(dateStart, dateEnd, date, GTPEnum.gtp1);
			PBRData rge2=new PBRData(dateStart, dateEnd, date, GTPEnum.rge2);
			PBRData rge3=new PBRData(dateStart, dateEnd, date, GTPEnum.rge3);
			PBRData rge4=new PBRData(dateStart, dateEnd, date, GTPEnum.rge4);
					
			answer.ChartRGE1 = new ChartAnswer();
			answer.ChartRGE1.Properties = getChartProperties(220);
			answer.ChartRGE1.Data = new ChartData();

			answer.ChartRGE2 = new ChartAnswer();
			answer.ChartRGE2.Properties = getChartProperties(200);
			answer.ChartRGE2.Data = new ChartData();

			answer.ChartRGE3 = new ChartAnswer();
			answer.ChartRGE3.Properties = getChartProperties(200);
			answer.ChartRGE3.Data = new ChartData();

			answer.ChartRGE4 = new ChartAnswer();
			answer.ChartRGE4.Properties = getChartProperties(400);
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
				answer.TableHour.Add(new GraphVyrabRGETableRow("P откл", Math.Round(rge1Hour["diff"]), Math.Round(rge2Hour["diff"]), Math.Round(rge3Hour["diff"]), Math.Round(rge4Hour["diff"])));
				answer.TableHour.Add(new GraphVyrabRGETableRow("P откл %", Math.Round(rge1Hour["diffProc"]), Math.Round(rge2Hour["diffProc"]), Math.Round(rge3Hour["diffProc"]), Math.Round(rge4Hour["diffProc"])));
				answer.TableHour.Add(new GraphVyrabRGETableRow("P рек", Math.Round(rge1Hour["recP"]), Math.Round(rge2Hour["recP"]), Math.Round(rge3Hour["recP"]), Math.Round(rge4Hour["recP"])));
		
			}


			answer.ChartRGE1.Data.addSerie(getDataSerie("Fakt", rge1.RealP, -1));
			answer.ChartRGE1.Data.addSerie(getDataSerie("Plan", rge1.SteppedPBR, -1));

			answer.ChartRGE2.Data.addSerie(getDataSerie("Fakt", rge2.RealP, -1));
			answer.ChartRGE2.Data.addSerie(getDataSerie("Plan", rge2.SteppedPBR, -1));

			answer.ChartRGE3.Data.addSerie(getDataSerie("Fakt", rge3.RealP, -1));
			answer.ChartRGE3.Data.addSerie(getDataSerie("Plan", rge3.SteppedPBR, -1));

			answer.ChartRGE4.Data.addSerie(getDataSerie("Fakt", rge4.RealP, -1));
			answer.ChartRGE4.Data.addSerie(getDataSerie("Plan", rge4.SteppedPBR, -1));


			return answer;
		}

		public static CheckGraphVyrabRGEAnswer getAnswerHH(DateTime date) {
			DateTime dateStart=date.Date;
			DateTime dateEnd=date.Date.AddHours(24);

			CheckGraphVyrabRGEAnswer answer=new CheckGraphVyrabRGEAnswer();


			PBRDataHH rge1=new PBRDataHH(dateStart, dateEnd, GTPEnum.gtp1);
			PBRDataHH rge2=new PBRDataHH(dateStart, dateEnd, GTPEnum.rge2);
			PBRDataHH rge3=new PBRDataHH(dateStart, dateEnd, GTPEnum.rge3);
			PBRDataHH rge4=new PBRDataHH(dateStart, dateEnd, GTPEnum.rge4);
			
			answer.ChartRGE1 = new ChartAnswer();
			answer.ChartRGE1.Properties = getChartProperties(220);
			answer.ChartRGE1.Data = new ChartData();

			answer.ChartRGE2 = new ChartAnswer();
			answer.ChartRGE2.Properties = getChartProperties(200);
			answer.ChartRGE2.Data = new ChartData();

			answer.ChartRGE3 = new ChartAnswer();
			answer.ChartRGE3.Properties = getChartProperties(200);
			answer.ChartRGE3.Data = new ChartData();

			answer.ChartRGE4 = new ChartAnswer();
			answer.ChartRGE4.Properties = getChartProperties(400);
			answer.ChartRGE4.Data = new ChartData();

			rge1.InitData();
			rge2.InitData();
			rge3.InitData();
			rge4.InitData();

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
				
				answer.TableHH.Add(row);
			}

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

		public static ChartProperties getChartProperties(int max) {
			ChartProperties props=new ChartProperties();
			props.XAxisType = XAxisTypeEnum.datetime;
			props.XValueFormatString = "dd.MM HH:mm";

			ChartAxisProperties pAx=new ChartAxisProperties();
			pAx.ProcessAuto = false;
			pAx.Auto = true;
			pAx.Min = 0;
			pAx.Max = max;
			pAx.Interval = 10;
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
			PlanSerie.SerieType = ChartSerieType.stepLine;
			PlanSerie.YAxisIndex = 0;
			PlanSerie.Enabled = true;

			props.addSerie(PlanSerie);
			props.addSerie(FaktSerie);
			

			return props;
		}
	}
}
