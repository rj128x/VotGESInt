using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using VotGES.PBR;

namespace VotGES.Piramida.Report
{	
	public class SutVedReport : Report
	{
		public SortedList<DateTime, double> PZad { get; set; }
		public PBRDataHH PBR { get; set; }
		public SutVedReport(DateTime dateStart, DateTime dateEnd, IntervalReportEnum interval) :
			base(dateStart, dateEnd, interval) {
			int pn=12;
			ReportMBRecords.AddRecordsMB(this, pn, 1, 1, true, false, DBOperEnum.eq, ResultTypeEnum.avg);
			ReportMBRecords.AddCalcRecords(this, true, false, ResultTypeEnum.avg);


			RecordTypeDB vbAvg=new RecordTypeDB(PiramidaRecords.MB_VB_Sgl, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.avg, dbOper: DBOperEnum.avg);
			vbAvg.ID = "VB_AVG";			
			RecordTypeDB nbAvg=new RecordTypeDB(PiramidaRecords.MB_NB_Sgl, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.avg, dbOper: DBOperEnum.avg);
			nbAvg.ID = "NB_AVG";
			RecordTypeDB rashodAvg=new RecordTypeDB(PiramidaRecords.MB_Rashod, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.avg, dbOper: DBOperEnum.avg);
			rashodAvg.ID = "RASHOD_AVG";
			RecordTypeDB tAvg=new RecordTypeDB(PiramidaRecords.MB_T, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.avg, dbOper: DBOperEnum.avg);
			tAvg.ID = "T_AVG";
			RecordTypeDB naporAvg=new RecordTypeDB(PiramidaRecords.MB_Napor_Sgl, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.avg, dbOper: DBOperEnum.avg);
			naporAvg.ID = "NAPOR_AVG";			
			this.AddRecordType(vbAvg);
			this.AddRecordType(nbAvg);
			this.AddRecordType(rashodAvg);
			this.AddRecordType(tAvg);
			this.AddRecordType(naporAvg);
	
		}


		public override void ReadData() {
			base.ReadData();
			PBR = new PBRDataHH(DateStart, DateEnd, 0);
			PBR.InitData();

			PZad = new SortedList<DateTime, double>();
			List<int> items=new List<int>();
			items.Add(91);
			List<PiramidaEnrty> records=PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 3, 2, 13, items, true, false, "PSV");
			foreach (PiramidaEnrty rec in records) {
				PZad.Add(rec.Date, rec.Value0);
			}

		}

	}
}

