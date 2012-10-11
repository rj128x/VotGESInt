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
		public int SleepTime { get; protected set; }
		public SortedList<string, ModbusInitDataArray> InitArrays { get; protected set; }
		public ModbusInitDataArray InitCalc { get; protected set; }
		public SortedList<string, ModbusDataReader> Readers { get; protected set; }
		public SortedList<string, ModbusDataWriter> WritersHH { get; protected set; }
		public SortedList<string, ModbusDataWriter> WritersMin { get; protected set; }
		public SortedList<string, bool> FinishReading { get; protected set; }
		public SortedList<string, double> FullResultData { get; protected set; }
		public List<string> ResultKeys { get;protected set; }
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
					ModbusServer sv=new ModbusServer(arr.ServerInfoArray);
					ModbusDataReader reader=new ModbusDataReader(sv, arr);
					reader.OnFinish += new FinishEvent(reader_OnFinish);
					Readers.Add(arr.ID, reader);
					String.Format("===Объект создан");

					if (arr.WriteMin) {
						Logger.Info(String.Format("Создание объекта записи данных в файл (минуты)"));
						ModbusDataWriter writer=new ModbusDataWriter(arr, RWModeEnum.min);
						WritersMin.Add(arr.ID, writer);
						String.Format("===Объект создан");
					}

					if (arr.WriteHH) {
						Logger.Info(String.Format("Создание объекта записи данных в файл (получасовки)"));
						ModbusDataWriter writer=new ModbusDataWriter(arr, RWModeEnum.hh);
						WritersHH.Add(arr.ID, writer);
						String.Format("===Объект создан");
					}

					foreach (KeyValuePair<string,ModbusInitData> de in arr.FullData) {
						FullResultData.Add(arr.ID + "_" + de.Value.ID,0);
						ResultKeys.Add(arr.ID + "_" + de.Value.ID);
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
					WritersMin.Add(InitCalc.ID, writer);
					String.Format("===Объект создан");
				}

				if (InitCalc.WriteHH) {
					Logger.Info(String.Format("Создание объекта записи данных в файл (получасовки)"));
					ModbusDataWriter writer=new ModbusDataWriter(InitCalc, RWModeEnum.hh);
					WritersHH.Add(InitCalc.ID, writer);
					String.Format("===Объект создан");
				}

				foreach (KeyValuePair<string,ModbusInitData> de in InitCalc.FullData) {
					FullResultData.Add(InitCalc.ID + "_" + de.Value.ID, 0);
					ResultKeys.Add(InitCalc.ID + "_" + de.Value.ID);
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
				FullResultData[key] = double.NaN;
			}
						
			/*foreach (KeyValuePair<string,ModbusDataReader> de in Readers) {
				de.Value.readData();
			}*/
			Readers.First().Value.readData();
		}

		public void ProcessFinish() {
			if (!FinishReading.Values.Contains(false)) {
				Logger.Info("===calc  ");
				Calc.Init(FullResultData);
				foreach (ModbusInitData initData in InitCalc.Data) {
					Calc.call(initData.FuncName, initData);
				}
				if (InitCalc.WriteHH) {
					WritersHH[InitCalc.ID].writeData(Calc.ResultData);
				}
				if (InitCalc.WriteMin) {
					WritersMin[InitCalc.ID].writeData(Calc.ResultData);
				}
				Logger.Info("====ok  ");
				Thread.Sleep(SleepTime);
				Read();
			} else {
				string next=FinishReading.First(de => de.Value == false).Key;
				Readers[next].readData();
			}
		}

		public void reader_OnFinish(string InitArrayID, SortedList<string, double> ResultData) {
			ModbusInitDataArray init=InitArrays[InitArrayID];
			if (ResultData == null) {
				Logger.Info("==" + DateTime.Now + " " + InitArrayID + " error read");
				FinishReading[InitArrayID] = true;
				foreach (KeyValuePair<string,ModbusInitData> de in init.FullData) {
					FullResultData[InitArrayID + "_" + de.Key] = double.NaN;
				}
				ProcessFinish();
			} else {
				Logger.Info("==" + DateTime.Now + " " + InitArrayID + " finish read");
				if (init.WriteMin) {
					WritersMin[InitArrayID].writeData(ResultData);
				}
				if (init.WriteHH) {
					WritersHH[InitArrayID].writeData(ResultData);
				}
				foreach (KeyValuePair<string,double> de in ResultData) {
					FullResultData[InitArrayID + "_" + de.Key] = de.Value;
				}
				FinishReading[InitArrayID] = true;

				ProcessFinish();
			}
			
		}


	}
}
