using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VotGES.Piramida.Report
{	
	public class SutVedReport : Report
	{
		public SutVedReport(DateTime dateStart, DateTime dateEnd, IntervalReportEnum interval) :
			base(dateStart, dateEnd, interval) {
			int pn=12;
			ReportMBRecords.AddRecordsMB(this, pn, 1, 1, true, false, DBOperEnum.eq, ResultTypeEnum.avg);
			ReportMBRecords.AddCalcRecords(this, true, false, ResultTypeEnum.avg);		
		}


		public override void ReadData() {
			base.ReadData();
		}

	}
}
