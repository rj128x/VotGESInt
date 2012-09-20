using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using VotGES;
using VotGES.Rashod;

namespace ModbusLib
{
	public class ModbusCalc
	{
		#region InitClass
		
		public SortedList<string, double> Data { get; set; }
		public SortedList<string, double> ResultData { get; set; }
		public ModbusInitDataArray InitCalc { get; set; }

		public  ModbusCalc() {
			ResultData = new SortedList<string, double>();
		}

		public void call(string name, ModbusInitData data) {
			double val=Double.NaN;
			try {
				MethodInfo mi = typeof(ModbusCalc).GetMethod(name, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
				val=(double)mi.Invoke(this, new object[] { });				
			} catch (Exception e) {
				Logger.Error("Ошибка при расчете метода " + name);
				Logger.Error(e.ToString());
			}
			Data[InitCalc.ID + "_" + data.ID] = val;
			ResultData.Add(data.ID,val);
		}

		public void Init(SortedList<string, double> Data) {
			this.Data = Data;
			ResultData.Clear();
		}

		#endregion
		
		public double P_GTP1() {
			return Data["MB_216"] + Data["MB_266"];
		}

		public double P_GTP2() {
			return Data["MB_316"] + Data["MB_366"] + Data["MB_416"] + Data["MB_466"] + Data["MB_516"] + Data["MB_566"] + Data["MB_616"] + Data["MB_666"];
		}

		public double P_GES() {
			return Data["MB_216"] + Data["MB_266"]+Data["MB_316"] + Data["MB_366"] + Data["MB_416"] + Data["MB_466"] + Data["MB_516"] + Data["MB_566"] + Data["MB_616"] + Data["MB_666"];
		}

		public double Rashod_GES() {
			return Data["MB_238"] + Data["MB_288"] + Data["MB_338"] + Data["MB_388"] + Data["MB_438"] + Data["MB_488"] + Data["MB_538"] + Data["MB_588"] + Data["MB_638"] + Data["MB_688"];
		}

		public double Rashod_GTP1() {
			return Data["MB_238"] + Data["MB_288"];
		}

		public double Rashod_GTP2() {
			return Data["MB_338"] + Data["MB_388"] + Data["MB_438"] + Data["MB_488"] + Data["MB_538"] + Data["MB_588"] + Data["MB_638"] + Data["MB_688"];
		}

		public static List<int>gtp1=new List<int>(new int[] { 1, 2 });
		public static List<int>gtp2=new List<int>(new int[] { 3, 4, 5, 6, 7, 8, 9, 10 });
		public static List<int>ges=new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

		public double OptRashod_GTP1() {
			return RUSA.getOptimRashod(Data["Calc_P_GTP1"], Data["MB_2014"], true, null, gtp1);
		}

		public double OptRashod_GTP2() {
			return RUSA.getOptimRashod(Data["Calc_P_GTP2"], Data["MB_2014"], true, null, gtp2);
		}

		public double OptRashod_GES() {
			return RUSA.getOptimRashod(Data["Calc_P_GES"], Data["MB_2014"], true, null, ges);
		}
		
	}
}
