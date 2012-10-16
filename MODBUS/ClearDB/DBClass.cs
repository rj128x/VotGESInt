using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;
using VotGES.Piramida;
using System.Data.SqlClient;

namespace ClearDB
{
	class DBClass
	{
		public static string InsertIntoHeader="INSERT INTO Data (parnumber,object,item,value0,objtype,data_date,rcvstamp,season)";
		public static string InsertInfoFormat="SELECT {0}, {1}, {2}, {3}, {4}, '{5}', '{6}', {7}";
		public static string DateFormat="yyyy-MM-dd HH:mm:ss";
		public static void AddData(List<string> insertsStrings, string insertIntoHeader, SqlTransaction transact) {
			List<string>ins=new List<string>();
			try {
				int i=0;
				foreach (string insert in insertsStrings) {
					i++;
					ins.Add(insert);
					if (ins.Count % 20 == 0 || i == insertsStrings.Count) {
						string insertsSQL = String.Join("\nUNION ALL\n", ins);
						string insertSQL = String.Format("{0}\n{1}", insertIntoHeader, insertsSQL);
						SqlCommand commandIns=transact.Connection.CreateCommand();
						commandIns.CommandText = insertSQL;
						commandIns.Transaction = transact;
						commandIns.ExecuteNonQuery();
						ins.Clear();
					}
				}
			} catch (Exception e) {
				Logger.Info(e.ToString());
			} finally {
				try {} catch { }
			}
		}

		public static void Run(string com, SqlTransaction transact) {
			List<string>ins=new List<string>();
			try {
				SqlCommand commandDel=transact.Connection.CreateCommand();
				commandDel.CommandText = com;
				commandDel.Transaction = transact;
				//Logger.Info(delStr);
				commandDel.ExecuteNonQuery();								
			} catch (Exception e) {
				Logger.Info(e.ToString());
			} finally {
				try {  } catch { }
			}
		}
	}
}
