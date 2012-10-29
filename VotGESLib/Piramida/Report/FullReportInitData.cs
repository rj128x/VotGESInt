using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VotGES.Piramida.Report
{
	public class FullReportRoot
	{
		public FullReportRecord RootMain { get; set; }
		public FullReportRecord RootLines { get; set; }
		public FullReportRecord RootSN { get; set; }
	}

	public class FullReportRecord : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
		public List<FullReportRecord> Children { get; set; }

		public string Title { get; set; }
		public string Key { get; set; }
		public bool IsGroup { get; set; }
		public bool Selectable { get; set; }
		protected bool selected;
		public bool Selected { get { return selected; } set { selected = value; NotifyChanged("Selected"); } }

		public FullReportRecord() { }

		public FullReportRecord addChild(FullReportRecord child) {
			Children.Add(child);
			IsGroup = true;
			return child;
		}
	}


	public class FullReportInitData
	{
		public FullReportRecord RootMain { get; set; }
		public FullReportRecord RootLines { get; set; }
		public FullReportRecord RootSN { get; set; }
		public FullReportInitData() {
			CreateMain();
			CreateLines();
			CreateSN();
		}


		protected void CreateMain() {
			FullReportRecord record,childRecord;
			RootMain = GetFullReportRecord("Воткинская ГЭС", "votGES");
			record = RootMain.addChild(GetFullReportRecord("Основные параметры", "mainParams"));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_GES));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_GTP1));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_GTP2));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_SN));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_Nebalans_GES));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_SP));

			record = RootMain.addChild(GetFullReportRecord("Вода", "water"));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_NB));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_QGES));
			record.addChild(GetFullReportRecord(ReportWaterRecords.Water_QGTP1));
			record.addChild(GetFullReportRecord(ReportWaterRecords.Water_QGTP1));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_Temp));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_VB));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_QOptGES));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_QOptGTP1));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_QOptGTP2));
			record.addChild(GetFullReportRecord(ReportWaterRecords.Water_OVER_GES));
			record.addChild(GetFullReportRecord(ReportWaterRecords.Water_OVER_GTP1));
			record.addChild(GetFullReportRecord(ReportWaterRecords.Water_OVER_GTP2));

			childRecord = record.addChild(GetFullReportRecord("Генераторы","qga"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA3));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA4));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA5));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA6));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA7));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA8));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA9));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA10));

			record = RootMain.addChild(GetFullReportRecord("Суточная ведомость", "gsv"));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV2));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV3));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV4));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV5));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV6));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV7));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV8));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV9));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV10));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV11));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV12));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV13));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV14));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV15));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV16));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV17));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV18));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV19));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV20));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV21));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV22));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV23));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV24));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV25));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV26));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV27));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV28));

			record = RootMain.addChild(GetFullReportRecord("Генераторы", "generators"));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA1_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA1_Otd));
			
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA2_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA2_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA2_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA2_Otd));
			
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA3));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA3_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA3_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA3));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA3_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA3_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA4));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA4_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA4_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA4));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA4_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA4_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA5));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA5_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA5_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA5));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA5_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA5_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA6));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA6));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA6_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA6_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA7));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA7_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA7_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA7));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA7_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA7_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA8));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA8_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA8_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA8));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA8_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA8_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA9));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA9_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA9_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA9));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA9_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA9_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA10));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA10_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA10_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA10));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA10_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA10_Otd));
		}

		protected void CreateLines() {
			FullReportRecord record,childRecord, child2,rec0;
			RootLines = GetFullReportRecord("ВЛ", "VL");

			rec0 = RootLines.addChild(GetFullReportRecord("P ВЛ","pvl"));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Berezovka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Berezovka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Berezovka_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Dubovaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Dubovaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Dubovaya_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Ivanovka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Ivanovka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Ivanovka_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Kauchuk));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Kauchuk_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Kauchuk_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_KSHT1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_KSHT1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_KSHT1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_KSHT2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_KSHT2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_KSHT2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Svetlaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Svetlaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Svetlaya_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_TEC));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_TEC_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_TEC_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Vodozabor1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Vodozabor1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Vodozabor1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Vodozabor2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Vodozabor2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Vodozabor2_Priem));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Izhevsk1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Izhevsk1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Izhevsk1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Izhevsk2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Izhevsk2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Izhevsk2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Kauchuk1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Kauchuk1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Kauchuk1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Kauchuk2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Kauchuk2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Kauchuk2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Svetlaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Svetlaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Svetlaya_Priem));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Karmanovo));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Karmanovo_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Karmanovo_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Emelino));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Emelino_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Emelino_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Vyatka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Vyatka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Vyatka_Priem));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.P_KL6_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_KL6_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_KL6_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_KL_Shluz1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Shluz1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Shluz1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_KL_Shluz2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Shluz2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Shluz2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_KL_Filtr1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Filtr1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Filtr1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_KL_Filtr2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Filtr2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Filtr2_Priem));

			rec0 = RootLines.addChild(GetFullReportRecord("Q ВЛ", "qvl"));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Berezovka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Berezovka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Berezovka_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Dubovaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Dubovaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Dubovaya_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Ivanovka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Ivanovka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Ivanovka_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Kauchuk));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Kauchuk_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Kauchuk_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_KSHT1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_KSHT1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_KSHT1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_KSHT2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_KSHT2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_KSHT2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Svetlaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Svetlaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Svetlaya_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_TEC));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_TEC_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_TEC_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Vodozabor1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Vodozabor1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Vodozabor1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Vodozabor2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Vodozabor2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Vodozabor2_Priem));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Izhevsk1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Izhevsk1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Izhevsk1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Izhevsk2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Izhevsk2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Izhevsk2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Kauchuk1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Kauchuk1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Kauchuk1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Kauchuk2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Kauchuk2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Kauchuk2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Svetlaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Svetlaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Svetlaya_Priem));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL500_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL500_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL500_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL500_Karmanovo));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL500_Karmanovo_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL500_Karmanovo_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL500_Emelino));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL500_Emelino_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL500_Emelino_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL500_Vyatka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL500_Vyatka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL500_Vyatka_Priem));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.Q_KL6_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_KL6_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_KL6_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_KL_Shluz1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Shluz1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Shluz1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_KL_Shluz2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Shluz2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Shluz2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_KL_Filtr1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Filtr1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Filtr1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_KL_Filtr2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Filtr2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Filtr2_Priem));


			record = RootLines.addChild(GetFullReportRecord("Главные трансформаторы", "mainTrans"));
			childRecord = record.addChild(GetFullReportRecord("1Т", "1T"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_1T_110));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_110_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_110_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_1T_110));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_56AT_110_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_56AT_110_Priem));

			childRecord = record.addChild(GetFullReportRecord("2АТ", "2AT"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_2AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_2AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_2AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_2AT_500));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_2AT_500_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_2AT_500_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_2AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_2AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_2AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_2AT_500));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_2AT_500_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_2AT_500_Priem));

			childRecord = record.addChild(GetFullReportRecord("3АТ", "3AT"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_3AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_3AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_3AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_3AT_500));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_3AT_500_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_3AT_500_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_3AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_3AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_3AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_3AT_500));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_3AT_500_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_3AT_500_Priem));

			childRecord = record.addChild(GetFullReportRecord("4Т", "4T"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_4T_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_4T_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_4T_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_4T_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_4T_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_4T_220_Priem));

			childRecord = record.addChild(GetFullReportRecord("5-6АТ", "56AT"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_56AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_56AT_110));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_110_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_110_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_56AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_56AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_56AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_56AT_110));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_56AT_110_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_56AT_110_Priem));
		}

		protected void CreateSN() {
			FullReportRecord record,childRecord, child2;
			RootSN = GetFullReportRecord("СН", "SN");


			record = RootSN.addChild(GetFullReportRecord(ReportMainRecords.P_SP));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_Vozb));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA1_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA2_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA3_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA4_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA5_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA6_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA7_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA8_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA9_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA10_Priem));

			childRecord=record.addChild(GetFullReportRecord(ReportGARecords.P_SN_GA));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_11T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_12T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_13T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_14T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_15T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_16T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_17T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_18T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_19T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_20T_Priem));
			record.addChild(GetFullReportRecord(ReportGARecords.P_SK));
			childRecord = record.addChild(GetFullReportRecord(ReportMainRecords.P_Nebalans));

			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_T_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_1T_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_2AT_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_3AT_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_4T_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_56AT_Nebalans));

			child2 = childRecord.addChild(GetFullReportRecord(ReportLinesRecords.P_VL_Nebalans));
			child2.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Nebalans));
			child2.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Nebalans));
			child2.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Nebalans));

			childRecord = record.addChild(GetFullReportRecord(ReportSNRecords.P_SN));

			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_7T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_8T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_1N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_21T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_22T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_2N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_23T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_24T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_3N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_27T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_28T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_7N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_1KU_31T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_32T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_8N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_25T_37T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_38T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_9N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_29T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_30T_31T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_10N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_33T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_34T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_36N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_35T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_36T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_Nasos));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_20NDS1_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_20NDS2_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_TVI_Priem));

			record = RootSN.addChild(GetFullReportRecord("МБ", "mb"));
			record.addChild(GetFullReportRecord(ReportMBRecords.MB_GA1_Istator));
			record.addChild(GetFullReportRecord(ReportMBRecords.MB_GA2_Istator));
			record.addChild(GetFullReportRecord(ReportMBRecords.MB_GA3_Istator));
			record.addChild(GetFullReportRecord(ReportMBRecords.MB_GA4_Istator));
			record.addChild(GetFullReportRecord(ReportMBRecords.MB_GA5_Istator));
			record.addChild(GetFullReportRecord(ReportMBRecords.MB_GA6_Istator));
			record.addChild(GetFullReportRecord(ReportMBRecords.MB_GA7_Istator));
			record.addChild(GetFullReportRecord(ReportMBRecords.MB_GA8_Istator));
			record.addChild(GetFullReportRecord(ReportMBRecords.MB_GA9_Istator));
			record.addChild(GetFullReportRecord(ReportMBRecords.MB_GA10_Istator));
			record.addChild(GetFullReportRecord(ReportMBRecords.MB_U_220));
			record.addChild(GetFullReportRecord(ReportMBRecords.MB_F_220));
			record.addChild(GetFullReportRecord(ReportMBRecords.MB_U_110));
			record.addChild(GetFullReportRecord(ReportMBRecords.MB_F_110));

			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_SHSV_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_SHSV_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_U_1SH_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_F_1SH_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_U_1SH_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_F_1SH_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_U_2SH_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_F_2SH_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_U_2SH_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_F_2SH_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Izhevsk1_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Izhevsk1_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Izhevsk1_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Izhevsk2_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Izhevsk2_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Izhevsk2_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Kauchuk1_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Kauchuk1_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Kauchuk1_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Kauchuk2_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Kauchuk2_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Kauchuk2_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_GES_Zad));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Svetlaya_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Svetlaya_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Svetlaya_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_OVV_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_OVV_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_OVV_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_2AT_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_2AT_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_2AT_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_3AT_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_3AT_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_3AT_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_4T_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_4T_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_4T_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_56AT_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_56AT_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_56AT_220));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA1_Irotor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA1_P));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA1_Q));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA1_IstatorA));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA1_IstatorB));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA1_IstatorC));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA1_Rashod));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA1_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA2_Irotor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA2_P));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA2_Q));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA2_IstatorA));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA2_IstatorB));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA2_IstatorC));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA2_Rashod));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA2_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA3_Irotor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA3_P));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA3_Q));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA3_IstatorA));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA3_IstatorB));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA3_IstatorC));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA3_Rashod));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA3_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA4_Irotor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA4_P));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA4_Q));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA4_IstatorA));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA4_IstatorB));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA4_IstatorC));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA4_Rashod));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA4_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA5_Irotor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA5_P));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA5_Q));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA5_IstatorA));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA5_IstatorB));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA5_IstatorC));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA5_Rashod));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA5_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA6_Irotor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA6_P));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA6_Q));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA6_IstatorA));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA6_IstatorB));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA6_IstatorC));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA6_Rashod));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA6_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA7_Irotor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA7_P));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA7_Q));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA7_IstatorA));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA7_IstatorB));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA7_IstatorC));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA7_Rashod));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA7_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_VB_Sgl));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_NB_Sgl));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Napor_Sgl));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA8_Irotor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA8_P));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA8_Q));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA8_IstatorA));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA8_IstatorB));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA8_IstatorC));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA8_Rashod));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA8_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA9_Irotor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA9_P));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA9_Q));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA9_IstatorA));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA9_IstatorB));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA9_IstatorC));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA9_Rashod));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA9_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA10_Irotor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA10_P));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA10_Q));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA10_IstatorA));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA10_IstatorB));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA10_IstatorC));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA10_Rashod));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_GA10_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Rashod));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_RashodCalc));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_T));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_GES));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_GES));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_KSHT1_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_KSHT1_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_KSHT1_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_KSHT2_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_KSHT2_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_KSHT2_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Kauchuk_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Kauchuk_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Kauchuk_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_TEC_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_TEC_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_TEC_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Berezovka_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Berezovka_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Berezovka_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Dubovaya_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Dubovaya_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Dubovaya_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Vodozabor1_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Vodozabor1_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Vodozabor1_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Vodozabor2_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Vodozabor2_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Vodozabor2_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Svetlaya_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Svetlaya_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Svetlaya_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Ivanovka_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Ivanovka_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Ivanovka_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_OVV_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_OVV_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_OVV_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Emelino_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Emelino_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Emelino_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_U_Emelino_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_F_Emelino_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Karmanovo_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Karmanovo_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Karmanovo_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_U_Karmanovo_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_F_Karmanovo_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_Vyatka_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_Vyatka_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_Vyatka_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_U_Vyatka_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_F_Vyatka_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_1T_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_1T_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_1T_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_56AT_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_56AT_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_56AT_110));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_2AT_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_2AT_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_2AT_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_I_3AT_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_P_3AT_500));
			record.addChild(GetFullReportRecord(PiramidaRecords.MB_Q_3AT_500));
		}


		public static FullReportRecord GetFullReportRecord(string title, string id) {
			FullReportRecord record=new FullReportRecord();
			record.Title = title;
			record.Key = id;
			record.Selectable = false;
			record.Children = new List<FullReportRecord>();
			return record;
		}

		public static FullReportRecord GetFullReportRecord(RecordTypeCalc record, string title = null) {
			FullReportRecord rec=new FullReportRecord();
			rec.Title = String.IsNullOrEmpty(title) ? record.Title : title;
			rec.Key = record.ID;
			rec.Selectable = true;
			rec.Children = new List<FullReportRecord>();
			return rec;
		}

		public static FullReportRecord GetFullReportRecord(PiramidaRecord record, string title = null) {
			FullReportRecord rec=new FullReportRecord();
			rec.Title = String.IsNullOrEmpty(title) ? record.Title : title;
			rec.Key = record.Key;
			rec.Selectable = true;
			rec.Children = new List<FullReportRecord>();
			return rec;
		}


	}
}
