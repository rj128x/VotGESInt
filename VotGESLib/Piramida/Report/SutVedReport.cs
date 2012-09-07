using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report
{
	class SutVedReport : Report
	{
		public SutVedReport(DateTime dateStart, DateTime dateEnd, IntervalReportEnum interval) :
			base(dateStart, dateEnd, interval) {
			int pn=12;
			ReportMBRecords.AddRecordsMB(this, pn, 1, 1, true, false, DBOperEnum.eq, ResultTypeEnum.avg);						
		}


		public override void ReadData() {
			base.ReadData();
		}


	}
}
