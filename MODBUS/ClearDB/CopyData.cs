﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;
using VotGES.Piramida;
using VotGES.Rashod;
using System.Data.SqlClient;

namespace ClearDB
{
	public class CopyData
	{
		public static void WriteCopy(DateTime dateStart, DateTime dateEnd, List<int> parnumbers) {			
			Logger.Info(String.Format("{0} - {1}", dateStart, dateEnd));
			string sel=String.Format("SELECT * FROM DATA where parnumber in ({2}) and data_date>='{0}' and data_date<='{1}'", 
				dateStart.ToString(DBClass.DateFormat), dateEnd.ToString(DBClass.DateFormat),String.Join(",",parnumbers));

			SqlConnection con=null;
			List<string> insertsStrings=new List<string>();
			List<string> delCond=new List<string>();
			try {
				con = PiramidaAccess.getConnection("P2000");
				con.Open();
				SqlCommand command=con.CreateCommand();
				command.CommandText = sel;
				SqlDataReader reader=command.ExecuteReader();				

				while (reader.Read()) {
					DateTime date=DateTime.Parse(reader["data_date"].ToString());
					string dt=date.ToString(DBClass.DateFormat);
					string ins=String.Format(DBClass.InsertInfoFormat, reader["parnumber"], reader["object"], reader["item"], reader["value0"], reader["objType"],
							dt, DateTime.Now.ToString(DBClass.DateFormat), 0);
					string del=String.Format("(object={0} and objtype={1} and parnumber={2} and item={3})",
						reader["object"], reader["objType"],reader["parnumber"], reader["item"]);
					if (!insertsStrings.Contains(ins)) {
						insertsStrings.Add(ins);
					}
					if (!delCond.Contains(del)) {
						delCond.Add(del);
					}
				}
			} catch (Exception e) {
				Logger.Info(e.ToString());
			} finally {
				try { con.Close(); } catch { };
			}

			Logger.Info("==selected "+insertsStrings.Count);
			string delStr=String.Format("DELETE FROM DATA WHERE ({2}) and DATA_DATE >= '{0}' and data_date<='{1}'", 
				dateStart.ToString(DBClass.DateFormat), dateEnd.ToString(DBClass.DateFormat), String.Join(" OR ", delCond));

			con=PiramidaAccess.getConnection("P3000");
			con.Open();
			SqlTransaction transact=con.BeginTransaction();

			if (insertsStrings.Count > 0) {
				foreach (string del in delCond) {
					delStr=String.Format("DELETE FROM DATA WHERE ({2}) and DATA_DATE >= '{0}' and data_date<='{1}'",
					dateStart.ToString(DBClass.DateFormat), dateEnd.ToString(DBClass.DateFormat), del);
					DBClass.Run(delStr, transact);
				}
				
				
				Logger.Info("==deleted");
				DBClass.AddData(insertsStrings, DBClass.InsertIntoHeader, transact);
				Logger.Info("==ok");
				try {
					transact.Commit();
				} catch (Exception e) {
					Logger.Info(e.ToString());
				} finally {
					try { transact.Connection.Close(); } catch { }
				}
			}
			
		}

	}
}
