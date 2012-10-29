using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report
{
	public enum FullReportMembersType { min, max, avg, def,eq }
	public class FullReport : Report
	{
		public FullReportMembersType MBType { get; set; }
		public FullReport(DateTime dateStart, DateTime dateEnd, IntervalReportEnum interval, FullReportMembersType mbType = FullReportMembersType.def) :
			base(dateStart, dateEnd, interval)
		{
			int parNumber=12;
			int scaleDiv=2;
			if (mbType != FullReportMembersType.def) {
				scaleDiv = 1;
			}
			MBType = mbType;
			ResultTypeEnum result=ResultTypeEnum.sum;
			DBOperEnum oper=DBOperEnum.sum;
			if (interval == IntervalReportEnum.minute) {
				parNumber = 4;
				oper = DBOperEnum.avg;
				result = ResultTypeEnum.sum;
				scaleDiv = 1;
			}
			bool otherType=false;
			if (mbType != FullReportMembersType.def) {
				otherType=true;
				switch (mbType) {
					case FullReportMembersType.avg:
						oper = DBOperEnum.avg;
						result = ResultTypeEnum.avg;
						break;
					case FullReportMembersType.min:
						oper = DBOperEnum.min;
						result = ResultTypeEnum.min;
						break;
					case FullReportMembersType.max:
						oper = DBOperEnum.max;
						result = ResultTypeEnum.max;
						break;
					case FullReportMembersType.eq:
						oper = DBOperEnum.eq;
						result = ResultTypeEnum.avg;
						break;	
				}
			}

			ReportGARecords.AddPRecordsGAP(this, parNumber, 1, scaleDiv, false, false,oper,result);
			ReportGARecords.AddPRecordsGAQ(this, parNumber, 1, scaleDiv, false, false,oper,result);
			ReportGARecords.AddPRecordsGAAdd(this, parNumber, 1, scaleDiv, false, false, oper,result);
			ReportGARecords.AddCalcRecords(this, false, false, result);

			ReportLinesRecords.AddLineRecordsP(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportLinesRecords.AddLineRecordsQ(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportLinesRecords.AddCalcRecords(this, false, false, result);

			ReportGlTransformRecords.AddGLTransformRecordsP(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportGlTransformRecords.AddGLTransformRecordsQ(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportGlTransformRecords.AddPRecordsForNebalans(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportGlTransformRecords.AddCalcRecords(this, false, false, result);

			ReportSNRecords.AddPRecordsSN(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportSNRecords.AddCalcRecords(this, false, false, result);

			ReportWaterRecords.AddPRecordsWater(this, parNumber, 1, 1, false, false, otherType ? oper : DBOperEnum.avg, otherType ? result : ResultTypeEnum.avg);
			ReportWaterRecords.AddGSVRecords(this, 26, 1, 1, false, false, otherType ? oper : DBOperEnum.avg, otherType ? result : ResultTypeEnum.avg);
			ReportWaterRecords.AddCalcRecords(this, false, false, otherType ? result : ResultTypeEnum.avg);

			ReportMainRecords.AddPRecords(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportMainRecords.AddCalcRecords(this, false, false, result);

			ReportMBRecords.AddRecordsMB(this, parNumber, 1, 1, false, false, otherType ? oper : DBOperEnum.avg, otherType ? result : ResultTypeEnum.avg);
			ReportMBRecords.AddCalcRecords(this, false, false, ResultTypeEnum.avg);
		}

		public void InitNeedData(List<String> selected) {
			foreach (String key in selected) {
				bool notAdd=(MBType == FullReportMembersType.max || MBType == FullReportMembersType.min) && RecordTypes[key] is RecordTypeCalc;
				if (!notAdd) {
					RecordTypes[key].Visible = true;
					RecordTypes[key].ToChart = true;
				}
			}
		}

	}
}
