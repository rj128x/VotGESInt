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
			this.AddRecordType(vbAvg);
			this.AddRecordType(nbAvg);
			this.AddRecordType(rashodAvg);
			this.AddRecordType(tAvg);

			RecordTypeDB pPlan=new RecordTypeDB(PiramidaRecords.P_GES, parNumber: 212, visible: true, toChart: false, divParam: 1000, multParam: 1, resultType: ResultTypeEnum.avg, dbOper: DBOperEnum.avg);
			pPlan.ID = "P_PLAN_AVG";
			this.AddRecordType(pPlan);

			RecordTypeDB pFakt=new RecordTypeDB(PiramidaRecords.P_GES, parNumber: 12, visible: true, toChart: false, divParam: 1000, multParam: 1, resultType: ResultTypeEnum.avg, dbOper: DBOperEnum.avg);
			pFakt.ID = "P_FAKT_AVG";
			this.AddRecordType(pFakt);

			
		}


		public override void ReadData() {
			base.ReadData();
			PBR = new PBRDataHH(DateStart, DateEnd, 0);
			PBR.InitData();
		}

	}
}
