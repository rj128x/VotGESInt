﻿using System;
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
		public static void AddData(List<string> insertsStrings, string insertIntoHeader, string conName) {
			List<string>ins=new List<string>();
			SqlConnection con=null;
			try {
				con = PiramidaAccess.getConnection(conName);
				con.Open();
				int i=0;
				foreach (string insert in insertsStrings) {
					i++;
					ins.Add(insert);
					if (ins.Count % 20 == 0 || i == insertsStrings.Count) {
						string insertsSQL = String.Join("\nUNION ALL\n", ins);
						string insertSQL = String.Format("{0}\n{1}", insertIntoHeader, insertsSQL);
						SqlCommand commandIns=con.CreateCommand();
						commandIns.CommandText = insertSQL;
						commandIns.ExecuteNonQuery();
						ins.Clear();
					}
				}
			} catch (Exception e) {
				Logger.Info(e.ToString());
			} finally {
				try { con.Close(); } catch { }
			}
		}

		public static void Run(string com, string conName) {
			List<string>ins=new List<string>();
			SqlConnection con=null;
			try {
				con = PiramidaAccess.getConnection(conName);
				con.Open();
				SqlCommand commandDel=con.CreateCommand();
				commandDel.CommandText = com;
				//Logger.Info(delStr);
				commandDel.ExecuteNonQuery();								
			} catch (Exception e) {
				Logger.Info(e.ToString());
			} finally {
				try { con.Close(); } catch { }
			}
		}
	}
}