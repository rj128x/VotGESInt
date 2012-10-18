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
			RecordTypeDB pAvg=new RecordTypeDB(PiramidaRecords.MB_P_GES, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.avg, dbOper: DBOperEnum.avg);
			pAvg.ID = "P_AVG";

			RecordTypeDB vbMin=new RecordTypeDB(PiramidaRecords.MB_VB_Sgl, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.min, dbOper: DBOperEnum.min);
			vbMin.ID = "VB_MIN";
			RecordTypeDB nbMin=new RecordTypeDB(PiramidaRecords.MB_NB_Sgl, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.min, dbOper: DBOperEnum.min);
			nbMin.ID = "NB_MIN";
			RecordTypeDB rashodMin=new RecordTypeDB(PiramidaRecords.MB_Rashod, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.min, dbOper: DBOperEnum.min);
			rashodMin.ID = "RASHOD_MIN";
			RecordTypeDB tMin=new RecordTypeDB(PiramidaRecords.MB_T, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.min, dbOper: DBOperEnum.min);
			tMin.ID = "T_MIN";
			RecordTypeDB naporMin=new RecordTypeDB(PiramidaRecords.MB_Napor_Sgl, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.min, dbOper: DBOperEnum.min);
			naporMin.ID = "NAPOR_MIN";
			RecordTypeDB pMin=new RecordTypeDB(PiramidaRecords.MB_P_GES, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.min, dbOper: DBOperEnum.min);
			pMin.ID = "P_MIN";

			RecordTypeDB vbMax=new RecordTypeDB(PiramidaRecords.MB_VB_Sgl, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.max, dbOper: DBOperEnum.max);
			vbMax.ID = "VB_MAX";
			RecordTypeDB nbMax=new RecordTypeDB(PiramidaRecords.MB_NB_Sgl, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.max, dbOper: DBOperEnum.max);
			nbMax.ID = "NB_MAX";
			RecordTypeDB rashodMax=new RecordTypeDB(PiramidaRecords.MB_Rashod, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.max, dbOper: DBOperEnum.max);
			rashodMax.ID = "RASHOD_MAX";
			RecordTypeDB tMax=new RecordTypeDB(PiramidaRecords.MB_T, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.max, dbOper: DBOperEnum.max);
			tMax.ID = "T_MAX";
			RecordTypeDB naporMax=new RecordTypeDB(PiramidaRecords.MB_Napor_Sgl, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.max, dbOper: DBOperEnum.max);
			naporMax.ID = "NAPOR_MAX";
			RecordTypeDB pMax=new RecordTypeDB(PiramidaRecords.MB_P_GES, parNumber: pn, visible: true, toChart: false, divParam: 1, multParam: 1, resultType: ResultTypeEnum.max, dbOper: DBOperEnum.max);
			pMax.ID = "P_MAX";




			this.AddRecordType(vbAvg);
			this.AddRecordType(nbAvg);
			this.AddRecordType(rashodAvg);
			this.AddRecordType(tAvg);
			this.AddRecordType(naporAvg);
			this.AddRecordType(pAvg);

			this.AddRecordType(vbMin);
			this.AddRecordType(nbMin);
			this.AddRecordType(rashodMin);
			this.AddRecordType(tMin);
			this.AddRecordType(naporMin);
			this.AddRecordType(pMin);

			this.AddRecordType(vbMax);
			this.AddRecordType(nbMax);
			this.AddRecordType(rashodMax);
			this.AddRecordType(tMax);
			this.AddRecordType(naporMax);
			this.AddRecordType(pMax);
	
		}


		public override void ReadData() {
			base.ReadData();
			PBR = new PBRDataHH(DateStart, DateEnd, GTPEnum.ges);
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
		}

	}
}

