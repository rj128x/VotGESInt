using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using VotGES;

namespace ModbusLib
{
	public class ModbusCalc
	{
		public SortedList<string, double> Data { get; set; }
		public SortedList<int, double> ResultData { get; set; }
		public ModbusInitDataArray InitCalc { get; set; }

		public  ModbusCalc() {
			ResultData = new SortedList<int, double>();
		}

		public void call(string name, ModbusInitData data) {
			try {
				MethodInfo mi = typeof(ModbusCalc).GetMethod(name, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
				double val=(double)mi.Invoke(this, new object[] { });
				Data[InitCalc.ID + "_" + data.Addr] = val;
				ResultData.Add(data.Addr,val);
			} catch (Exception e) {
				Logger.Error("Ошибка при расчете метода " + name);
				Logger.Error(e.ToString());
			}
		}

		public void Init(SortedList<string, double> Data) {
			this.Data = Data;
			ResultData.Clear();
		}


		public double P_GTP1() {
			return Data["MB_216"] + Data["MB_266"];
		}

		public double P_GTP2() {
			return Data["MB_316"] + Data["MB_366"] + Data["MB_416"] + Data["MB_466"] + Data["MB_516"] + Data["MB_566"] + Data["MB_616"] + Data["MB_666"];
		}
		
	}
}
