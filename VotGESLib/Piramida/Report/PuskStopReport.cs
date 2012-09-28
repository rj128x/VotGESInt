using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace VotGES.Piramida.Report
{
	public class PuskStopRecord
	{
		public int GA { get; set; }
		public int CountPusk { get; set; }
		public int CountStop { get; set; }
		public int CountPuskGen { get; set; }
		public int CountPuskSK { get; set; }
	}

	public class PuskStopReport
	{
		public DateTime DateStart { get; protected set; }
		public DateTime DateEnd { get; protected set; }
		public SortedList<int, PuskStopRecord> Data { get; protected set; }
		public PuskStopRecord SumRecord { get; protected set; }

		public PuskStopReport(DateTime DateStart, DateTime DateEnd) {
			this.DateEnd = DateEnd;
			this.DateStart = DateStart;
			
		}
		
		public void ReadData() {
			this.Data=new SortedList<int,PuskStopRecord>();
			for (int ga=1;ga<=10;ga++){
				PuskStopRecord rec=new PuskStopRecord();
				rec.GA=ga;
				Data.Add(ga,rec);
			}
			SumRecord = new PuskStopRecord();

			SqlConnection con=null;			
			try {
				string sel=String.Format("SELECT item, Value0, COUNT(VALUE0) FROM DATA WHERE Parnumber=13 and object=30 and objtype=2 and item>=1 and item<=30 and data_date>=@dateStart and data_date<=@dateEnd group by item, value0");
				con = PiramidaAccess.getConnection("PSV");
				con.Open();
				SqlCommand command=con.CreateCommand();
				command.CommandText = sel;
				command.Parameters.AddWithValue("@dateStart", DateStart);
				command.Parameters.AddWithValue("@dateEnd", DateEnd);

				SqlDataReader reader=command.ExecuteReader();
				while (reader.Read()) {
					int item=Convert.ToInt32(reader[0]);
					int value0=Convert.ToInt32(reader[1]);
					int cnt=Convert.ToInt32(reader[2]);
					int ga=item % 10;
					ga = ga == 0 ? 10 : ga;

					if (item <= 10) {
						if (value0 == 1) {
							Data[ga].CountPuskGen = cnt;
						}
					}else	if (item <= 20) {
						if (value0 == 1) {
							Data[ga].CountPuskSK = cnt;
						}
					}else	if (item <= 30) {
						if (value0 == 1) {
							Data[ga].CountPusk = cnt;
						}
						if (value0 == 0) {
							Data[ga].CountStop = cnt;
						}
					}
				}
			} catch (Exception e) {
				Logger.Error("Ошибка при получении пусков-остановов");
				Logger.Error(e.ToString());
			} finally { try { con.Close(); } catch { } }
			for (int ga=1; ga <= 10; ga++) {
				SumRecord.CountPusk += Data[ga].CountPusk;
				SumRecord.CountStop += Data[ga].CountStop;
				SumRecord.CountPuskSK += Data[ga].CountPuskSK;
				SumRecord.CountPuskGen += Data[ga].CountPuskGen;
			}

		}
	}
}
