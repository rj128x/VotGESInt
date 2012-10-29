using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report
{
	public class ReportWaterRecords
	{
		public static RecordTypeCalc Water_QGTP1=new RecordTypeCalc("Water_QGTP1", "Расход ГТП-1", null);
		public static RecordTypeCalc Water_QGTP2=new RecordTypeCalc("Water_QGTP2", "Расход ГТП-2", null);
		public static RecordTypeCalc Water_OVER_GES=new RecordTypeCalc("Water_OVER_GES", "О ВЭР ГЭС", null);
		public static RecordTypeCalc Water_OVER_GTP1=new RecordTypeCalc("Water_OVER_GTP1", "О ВЭР ГТП1", null);
		public static RecordTypeCalc Water_OVER_GTP2=new RecordTypeCalc("Water_OVER_GTP2", "О ВЭР ГТП2", null);

		protected static double getProc(double plan, double fakt) {
			if (fakt > 0) {
				return plan / fakt * 100;
			} else {
				if (plan == 0) {
					return 100;
				} else {
					return 0;
				}
			}
		}

		public static void CreateWater() {
			Water_QGTP1.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Water_Q_GA1.Key] +
						 report[date, PiramidaRecords.Water_Q_GA2.Key];
			});

			Water_QGTP2.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Water_Q_GA3.Key] +
						 report[date, PiramidaRecords.Water_Q_GA4.Key] +
						 report[date, PiramidaRecords.Water_Q_GA5.Key] +
						 report[date, PiramidaRecords.Water_Q_GA6.Key] +
						 report[date, PiramidaRecords.Water_Q_GA7.Key] +
						 report[date, PiramidaRecords.Water_Q_GA8.Key] +
						 report[date, PiramidaRecords.Water_Q_GA9.Key] +
						 report[date, PiramidaRecords.Water_Q_GA10.Key];
			});

			Water_OVER_GTP1.CalcFunction = new RecordCalcDelegate((report, date) => {
				return getProc(report[date, PiramidaRecords.Water_QOptGTP1.Key], report[date, ReportWaterRecords.Water_QGTP1.ID]);
			});

			Water_OVER_GTP2.CalcFunction = new RecordCalcDelegate((report, date) => {
				return getProc(report[date, PiramidaRecords.Water_QOptGTP2.Key], report[date, ReportWaterRecords.Water_QGTP2.ID]);
			});

			Water_OVER_GES.CalcFunction = new RecordCalcDelegate((report, date) => {
				return getProc(report[date, PiramidaRecords.Water_QOptGES.Key], report[date, PiramidaRecords.Water_QGES.Key]);
			});
		}

		static ReportWaterRecords() {
			CreateWater();

		}

		public static void AddCalcRecords(Report report, bool visible, bool toChart, ResultTypeEnum oper) {
			report.AddRecordType(new RecordTypeCalc(Water_QGTP1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Water_QGTP2, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Water_OVER_GES, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Water_OVER_GTP1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Water_OVER_GTP2, toChart, visible, oper));
		}

		public static void AddPRecordsWater(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart, DBOperEnum oper, ResultTypeEnum result) {
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_Napor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_NB, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_Q_GA1, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_Q_GA10, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_Q_GA2, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_Q_GA3, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_Q_GA4, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_Q_GA5, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_Q_GA6, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_Q_GA7, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_Q_GA8, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_Q_GA9, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_QGES, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_QOptGES, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_QOptGTP1, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_QOptGTP2, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_Temp, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Water_VB, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
		}

		public static void AddGSVRecords(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart, DBOperEnum oper, ResultTypeEnum result) {
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV2, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV3, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV4, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV5, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV6, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV7, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV8, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV9, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV10, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV11, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV12, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV13, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV14, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV15, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV16, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV17, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV18, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV19, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV20, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV21, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV22, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV23, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV24, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV25, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV26, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV27, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.GSV28, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
		}

	}
}
