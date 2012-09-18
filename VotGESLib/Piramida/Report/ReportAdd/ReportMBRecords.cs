using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report
{
	public class ReportMBRecords
	{
		public static RecordTypeCalc MB_GA1_Istator=new RecordTypeCalc("MB_GA1_Istator", "ГА-1 ток статора", null);
		public static RecordTypeCalc MB_GA2_Istator=new RecordTypeCalc("MB_GA2_Istator", "ГА-2 ток статора", null);
		public static RecordTypeCalc MB_GA3_Istator=new RecordTypeCalc("MB_GA3_Istator", "ГА-3 ток статора", null);
		public static RecordTypeCalc MB_GA4_Istator=new RecordTypeCalc("MB_GA4_Istator", "ГА-4 ток статора", null);
		public static RecordTypeCalc MB_GA5_Istator=new RecordTypeCalc("MB_GA5_Istator", "ГА-5 ток статора", null);
		public static RecordTypeCalc MB_GA6_Istator=new RecordTypeCalc("MB_GA6_Istator", "ГА-6 ток статора", null);
		public static RecordTypeCalc MB_GA7_Istator=new RecordTypeCalc("MB_GA7_Istator", "ГА-7 ток статора", null);
		public static RecordTypeCalc MB_GA8_Istator=new RecordTypeCalc("MB_GA8_Istator", "ГА-8 ток статора", null);
		public static RecordTypeCalc MB_GA9_Istator=new RecordTypeCalc("MB_GA9_Istator", "ГА-9 ток статора", null);
		public static RecordTypeCalc MB_GA10_Istator=new RecordTypeCalc("MB_GA10_Istator", "ГА-10 ток статора", null);
		public static RecordTypeCalc MB_U_220=new RecordTypeCalc("MB_U_220", "U 220", null);
		public static RecordTypeCalc MB_U_110=new RecordTypeCalc("MB_U_110", "U 110", null);
		public static RecordTypeCalc MB_U_500=new RecordTypeCalc("MB_U_500", "U 500", null);
		public static RecordTypeCalc MB_F_220=new RecordTypeCalc("MB_F_220", "F 220", null);
		public static RecordTypeCalc MB_F_110=new RecordTypeCalc("MB_F_110", "F 110", null);


		public static void CreateMB() {
			MB_GA1_Istator.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_GA1_IstatorA.Key] + 
						report[date, PiramidaRecords.MB_GA1_IstatorB.Key] + 
						report[date, PiramidaRecords.MB_GA1_IstatorC.Key])/3;
				});

			MB_GA2_Istator.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_GA2_IstatorA.Key] +
						report[date, PiramidaRecords.MB_GA2_IstatorB.Key] +
						report[date, PiramidaRecords.MB_GA2_IstatorC.Key]) / 3;
				});

			MB_GA3_Istator.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_GA3_IstatorA.Key] +
						report[date, PiramidaRecords.MB_GA3_IstatorB.Key] +
						report[date, PiramidaRecords.MB_GA3_IstatorC.Key]) / 3;
				});

			MB_GA4_Istator.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_GA4_IstatorA.Key] +
						report[date, PiramidaRecords.MB_GA4_IstatorB.Key] +
						report[date, PiramidaRecords.MB_GA4_IstatorC.Key]) / 3;
				});

			MB_GA5_Istator.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_GA5_IstatorA.Key] +
						report[date, PiramidaRecords.MB_GA5_IstatorB.Key] +
						report[date, PiramidaRecords.MB_GA5_IstatorC.Key]) / 3;
				});

			MB_GA6_Istator.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_GA6_IstatorA.Key] +
						report[date, PiramidaRecords.MB_GA6_IstatorB.Key] +
						report[date, PiramidaRecords.MB_GA6_IstatorC.Key]) / 3;
				});

			MB_GA7_Istator.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_GA7_IstatorA.Key] +
						report[date, PiramidaRecords.MB_GA7_IstatorB.Key] +
						report[date, PiramidaRecords.MB_GA7_IstatorC.Key]) / 3;
				});

			MB_GA8_Istator.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_GA8_IstatorA.Key] +
						report[date, PiramidaRecords.MB_GA8_IstatorB.Key] +
						report[date, PiramidaRecords.MB_GA8_IstatorC.Key]) / 3;
				});

			MB_GA9_Istator.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_GA9_IstatorA.Key] +
						report[date, PiramidaRecords.MB_GA9_IstatorB.Key] +
						report[date, PiramidaRecords.MB_GA9_IstatorC.Key]) / 3;
				});

			MB_GA10_Istator.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_GA10_IstatorA.Key] +
						report[date, PiramidaRecords.MB_GA10_IstatorB.Key] +
						report[date, PiramidaRecords.MB_GA10_IstatorC.Key]) / 3;
				});

			MB_U_110.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_U_1SH_110.Key] + report[date, PiramidaRecords.MB_U_2SH_110.Key]) / 2;
				});

			MB_F_110.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_F_1SH_110.Key] + report[date, PiramidaRecords.MB_F_2SH_110.Key]) / 2;
				});

			MB_U_220.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_U_1SH_220.Key] + report[date, PiramidaRecords.MB_U_2SH_220.Key]) / 2;
				});

			MB_F_220.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_F_1SH_220.Key] + report[date, PiramidaRecords.MB_F_2SH_220.Key]) / 2;
				});

			MB_U_500.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return (report[date, PiramidaRecords.MB_U_Emelino_500.Key] + report[date, PiramidaRecords.MB_U_Karmanovo_500.Key]+
						report[date, PiramidaRecords.MB_U_Vyatka_500.Key]) / 3;
				});
		}

		static ReportMBRecords() {
			CreateMB();
		}

		public static void AddCalcRecords(Report report, bool visible, bool toChart, ResultTypeEnum oper) {
			report.AddRecordType(new RecordTypeCalc(MB_GA1_Istator, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(MB_GA2_Istator, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(MB_GA3_Istator, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(MB_GA4_Istator, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(MB_GA5_Istator, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(MB_GA6_Istator, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(MB_GA7_Istator, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(MB_GA8_Istator, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(MB_GA9_Istator, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(MB_GA10_Istator, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(MB_U_110, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(MB_U_220, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(MB_F_110, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(MB_F_220, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(MB_U_500, toChart, visible, oper));
		}

		public static void AddRecordsMB(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart, DBOperEnum oper, ResultTypeEnum result) {
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_SHSV_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_SHSV_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_U_1SH_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_F_1SH_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_U_1SH_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_F_1SH_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_U_2SH_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_F_2SH_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_U_2SH_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_F_2SH_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Izhevsk1_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Izhevsk1_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Izhevsk1_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Izhevsk2_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Izhevsk2_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Izhevsk2_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Kauchuk1_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Kauchuk1_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Kauchuk1_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Kauchuk2_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Kauchuk2_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Kauchuk2_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_GES_Zad, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Svetlaya_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Svetlaya_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Svetlaya_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_OVV_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_OVV_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_OVV_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_2AT_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_2AT_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_2AT_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_3AT_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_3AT_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_3AT_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_4T_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_4T_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_4T_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_56AT_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_56AT_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_56AT_220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA1_Irotor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA1_P, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA1_Q, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA1_IstatorA, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA1_IstatorB, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA1_IstatorC, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA1_Rashod, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA1_Napor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA2_Irotor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA2_P, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA2_Q, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA2_IstatorA, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA2_IstatorB, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA2_IstatorC, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA2_Rashod, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA2_Napor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA3_Irotor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA3_P, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA3_Q, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA3_IstatorA, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA3_IstatorB, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA3_IstatorC, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA3_Rashod, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA3_Napor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA4_Irotor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA4_P, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA4_Q, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA4_IstatorA, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA4_IstatorB, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA4_IstatorC, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA4_Rashod, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA4_Napor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA5_Irotor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA5_P, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA5_Q, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA5_IstatorA, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA5_IstatorB, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA5_IstatorC, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA5_Rashod, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA5_Napor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA6_Irotor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA6_P, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA6_Q, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA6_IstatorA, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA6_IstatorB, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA6_IstatorC, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA6_Rashod, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA6_Napor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA7_Irotor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA7_P, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA7_Q, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA7_IstatorA, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA7_IstatorB, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA7_IstatorC, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA7_Rashod, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA7_Napor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_VB_Sgl, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_NB_Sgl, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Napor_Sgl, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA8_Irotor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA8_P, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA8_Q, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA8_IstatorA, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA8_IstatorB, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA8_IstatorC, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA8_Rashod, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA8_Napor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA9_Irotor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA9_P, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA9_Q, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA9_IstatorA, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA9_IstatorB, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA9_IstatorC, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA9_Rashod, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA9_Napor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA10_Irotor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA10_P, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA10_Q, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA10_IstatorA, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA10_IstatorB, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA10_IstatorC, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA10_Rashod, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_GA10_Napor, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Rashod, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_RashodCalc, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_GES, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_GES, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_KSHT1_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_KSHT1_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_KSHT1_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_KSHT2_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_KSHT2_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_KSHT2_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Kauchuk_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Kauchuk_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Kauchuk_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_TEC_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_TEC_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_TEC_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Berezovka_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Berezovka_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Berezovka_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Dubovaya_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Dubovaya_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Dubovaya_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Vodozabor1_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Vodozabor1_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Vodozabor1_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Vodozabor2_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Vodozabor2_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Vodozabor2_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Svetlaya_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Svetlaya_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Svetlaya_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Ivanovka_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Ivanovka_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Ivanovka_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_OVV_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_OVV_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_OVV_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Emelino_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Emelino_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Emelino_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_U_Emelino_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_F_Emelino_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Karmanovo_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Karmanovo_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Karmanovo_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_U_Karmanovo_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_F_Karmanovo_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_Vyatka_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_Vyatka_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_Vyatka_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_U_Vyatka_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_F_Vyatka_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_1T_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_1T_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_1T_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_56AT_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_56AT_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_56AT_110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_2AT_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_2AT_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_2AT_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_I_3AT_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_P_3AT_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.MB_Q_3AT_500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));

		}


	}
}
