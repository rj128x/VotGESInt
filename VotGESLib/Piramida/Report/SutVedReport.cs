using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using VotGES.PBR;
using System.Data.SqlClient;

namespace VotGES.Piramida.Report
{	
	public class SutVedReport : Report
	{
		public SortedList<DateTime, double> PZad { get; set; }
		public double LastP { get; set; }
		public PBRDataHH PBR { get; set; }
		public PuskStopReport PuskStop { get; set; }
		public int AddHours { get; set; }
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

			SqlConnection con=null;;
			double lastP=Double.NaN;
			try {
				string sel=String.Format("SELECT TOP 1 VALUE0 FROM DATA WHERE Parnumber=13 and object=3 and objtype=2 and item=91 and data_date<@date order by data_date desc");
				con = PiramidaAccess.getConnection("PSV");
				con.Open();
				SqlCommand command=con.CreateCommand();
				command.CommandText = sel;
				command.Parameters.AddWithValue("@date", DateStart);
				lastP = (double)command.ExecuteScalar();
			} catch {
				Logger.Error("Ошибка при получении последнего задания мощности");
			} 
			finally { try { con.Close(); } catch { } }

			LastP = lastP;

			PuskStop = new PuskStopReport(DateStart,DateEnd);
			PuskStop.ReadData();
		}

	}
}

