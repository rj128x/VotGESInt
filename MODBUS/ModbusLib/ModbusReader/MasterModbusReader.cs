using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using VotGES;
using VotGES.XMLSer;

namespace ModbusLib
{
	public class MasterModbusReader
	{
		private int sleepTime;
		public int SleepTime {
			get { return sleepTime; }
			set { sleepTime = value; }
		}

		private SortedList<string,ModbusInitDataArray> initArrays;
		public SortedList<string, ModbusInitDataArray> InitArrays {
			get { return initArrays; }
			set { initArrays = value; }
		}

		private ModbusInitDataArray initCalc;
		public ModbusInitDataArray InitCalc {
			get { return initCalc; }
			set { initCalc = value; }
		}

		private SortedList<string,ModbusDataReader> readers;
		public SortedList<string, ModbusDataReader> Readers {
			get { return readers; }
			set { readers = value; }
		}

		private SortedList<string,ModbusDataWriter>writersHH;
		public SortedList<string, ModbusDataWriter> WritersHH {
			get { return writersHH; }
			set { writersHH = value; }
		}

		private SortedList<string,ModbusDataWriter>writersMin;
		public SortedList<string, ModbusDataWriter> WritersMin {
			get { return writersMin; }
			set { writersMin = value; }
		}

		private SortedList<string,bool>finishReading;
		public SortedList<string, bool> FinishReading {
			get { return finishReading; }
			set { finishReading = value; }
		}
		public SortedList<string, double> FullResultData { get; set; }
		public List<string> ResultKeys { get; set; }

		public ModbusCalc Calc { get; protected set; }
		
		public MasterModbusReader(int sleepTime) {
			SleepTime = sleepTime;
			InitArrays = new SortedList<string, ModbusInitDataArray>();
			Readers = new SortedList<string, ModbusDataReader>();
			WritersHH = new SortedList<string, ModbusDataWriter>();
			WritersMin = new SortedList<string, ModbusDataWriter>();
			FinishReading = new SortedList<string, bool>();
			FullResultData = new SortedList<string, double>();
			ResultKeys = new List<string>();
			foreach (string fileName in Settings.single.InitFiles) {
				try {
					Logger.Info(String.Format("Чтение настроек modbus из файла '{0}'", fileName));
					ModbusInitDataArray arr = XMLSer<ModbusInitDataArray>.fromXML(fileName);
					arr.processData();
					InitArrays.Add(arr.ID, arr);
					String.Format("===Считано {0} записей", arr.FullData.Count);

					Logger.Info(String.Format("Создание объекта чтения данных"));
					ModbusServer sv=new ModbusServer(arr.IP, (ushort)arr.Port);
					ModbusDataReader reader=new ModbusDataReader(sv, arr);
					reader.OnFinish += new FinishEvent(reader_OnFinish);
					readers.Add(arr.ID, reader);
					String.Format("===Объект создан");

					if (arr.WriteMin) {
						Logger.Info(String.Format("Создание объекта записи данных в файл (минуты)"));
						ModbusDataWriter writer=new ModbusDataWriter(arr, RWModeEnum.min);
						writersMin.Add(arr.ID, writer);
						String.Format("===Объект создан");
					}

					if (arr.WriteHH) {
						Logger.Info(String.Format("Создание объекта записи данных в файл (получасовки)"));
						ModbusDataWriter writer=new ModbusDataWriter(arr, RWModeEnum.hh);
						writersHH.Add(arr.ID, writer);
						String.Format("===Объект создан");
					}

					foreach (KeyValuePair<int,ModbusInitData> de in arr.FullData) {
						FullResultData.Add(arr.ID + "_" + de.Value.Addr,0);
						ResultKeys.Add(arr.ID + "_" + de.Value.Addr);
					}

					FinishReading.Add(arr.ID, false);

				} catch (Exception e) {
					String.Format("===Ошибка при чтении настроек");
					Logger.Error(e.ToString());
				}
			}

			try {
				Logger.Info(String.Format("Чтение настроек modbus из файла '{0}'", Settings.single.InitCalcFile));
				InitCalc = XMLSer<ModbusInitDataArray>.fromXML(Settings.single.InitCalcFile);
				InitCalc.processData();
				String.Format("===Считано {0} записей", InitCalc.FullData.Count);

				if (InitCalc.WriteMin) {
					Logger.Info(String.Format("Создание объекта записи данных в файл (минуты)"));
					ModbusDataWriter writer=new ModbusDataWriter(InitCalc, RWModeEnum.min);
					writersMin.Add(InitCalc.ID, writer);
					String.Format("===Объект создан");
				}

				if (InitCalc.WriteHH) {
					Logger.Info(String.Format("Создание объекта записи данных в файл (получасовки)"));
					ModbusDataWriter writer=new ModbusDataWriter(InitCalc, RWModeEnum.hh);
					writersHH.Add(InitCalc.ID, writer);
					String.Format("===Объект создан");
				}

				foreach (KeyValuePair<int,ModbusInitData> de in InitCalc.FullData) {
					FullResultData.Add(InitCalc.ID + "_" + de.Value.Addr, 0);
					ResultKeys.Add(InitCalc.ID + "_" + de.Value.Addr);
				}

			} catch (Exception e) {
				String.Format("===Ошибка при чтении настроек");
				Logger.Error(e.ToString());
			}
			Calc = new ModbusCalc();
			Calc.InitCalc = InitCalc;			
		}

		public void Read() {
			foreach (string key in InitArrays.Keys) {
				FinishReading[key] = false;
			}

			foreach (string key in ResultKeys) {
				FullResultData[key] = 0;
			}
						
			foreach (KeyValuePair<string,ModbusDataReader> de in Readers) {
				de.Value.readData();
			}			
		}
				

		public void reader_OnFinish(string InitArrayID, SortedList<int, double> ResultData) {
			Console.Write(DateTime.Now+" "+ InitArrayID + "  read");
			ModbusInitDataArray init=initArrays[InitArrayID];
			if (init.WriteMin) {
				WritersMin[InitArrayID].writeData(ResultData);
			}
			if (init.WriteHH) {
				WritersHH[InitArrayID].writeData(ResultData);
			}
			foreach(KeyValuePair<int,double> de in ResultData){
				FullResultData[InitArrayID + "_" + de.Key] = de.Value;
			}
			FinishReading[InitArrayID] = true;
			
			if (!FinishReading.Values.Contains(false)) {
				Console.Write("-calc  ");
				Calc.Init(FullResultData);
				foreach (ModbusInitData initData in InitCalc.FullData.Values) {
					Calc.call(initData.FuncName, initData);
				}
				if (InitCalc.WriteHH) {
					WritersHH[InitCalc.ID].writeData(Calc.ResultData);
				}
				if (InitCalc.WriteMin) {
					WritersMin[InitCalc.ID].writeData(Calc.ResultData);
				}
				Console.WriteLine("-ok  ");
				Thread.Sleep(SleepTime);
				Read();
			}
			
		}


	}
}
