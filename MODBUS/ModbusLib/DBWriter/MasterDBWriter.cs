using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using VotGES;
using VotGES.XMLSer;

namespace ModbusLib
{
	public class LastProcessedData
	{
		public static LastProcessedData Single { get; protected set; }
		static LastProcessedData() {
		}
		protected static void init() {
			Single = new LastProcessedData();
			Single.LastProcessedHH = DateTime.MaxValue;
			Single.LastProcessedMin = DateTime.MaxValue;
		}
		public DateTime LastProcessedMin { get; set; }
		public DateTime LastProcessedHH { get; set; }
		public static void Read() {
			try {
				Single = XMLSer<LastProcessedData>.fromXML("Data\\LastData.xml");
			} catch {
			}
			if (Single == null) {
				init();
			}
		}
		public static void Write() {
			try {
				XMLSer<LastProcessedData>.toXML(Single, "Data\\LastData.xml");
			} catch { }
		}
	}

	public class MasterDBWriter
	{
		public int SleepTimeMin {get;protected set;}
		public int DepthHH {get;protected set;}
		public int DepthMin {get;protected set;}
		public SortedList<string, ModbusInitDataArray> InitArrays { get; protected set; }
		public SortedList<string, DataDBWriter> Writers { get; protected set; }
		public DateTime LastHHDate { get; protected set; }
		
		public MasterDBWriter() {			
			InitArrays = new SortedList<string, ModbusInitDataArray>();
			Writers = new SortedList<string, DataDBWriter>();

			foreach (string fileName in Settings.single.InitFiles) {
				try {
					Logger.Info(String.Format("Чтение настроек modbus из файла '{0}'", fileName));
					ModbusInitDataArray arr = XMLSer<ModbusInitDataArray>.fromXML(fileName);
					arr.processData();
					InitArrays.Add(arr.ID, arr);
					String.Format("===Считано {0} записей", arr.FullData.Count);

					DataDBWriter writer=new DataDBWriter(arr);
					Writers.Add(arr.ID, writer);

				} catch (Exception e) {
					String.Format("===Ошибка при чтении настроек");
					Logger.Error(e.ToString());
				}
			}

			try {
				Logger.Info(String.Format("Чтение настроек modbus из файла '{0}'", Settings.single.InitCalcFile));
				ModbusInitDataArray arr = XMLSer<ModbusInitDataArray>.fromXML(Settings.single.InitCalcFile);
				arr.processData();
				InitArrays.Add(arr.ID, arr);
				String.Format("===Считано {0} записей", arr.FullData.Count);

				DataDBWriter writer=new DataDBWriter(arr);
				Writers.Add(arr.ID, writer);

			} catch (Exception e) {
				String.Format("===Ошибка при чтении настроек");
				Logger.Error(e.ToString());
			}
		}

		public DateTime Process(DateTime needDate, RWModeEnum mode, int depth,DateTime lastProcessed) {
			DateTime DateEnd=needDate.AddMinutes(0);
			DateTime DateStart=needDate.AddMinutes(0);

			if (mode == RWModeEnum.hh) {
				DateEnd = ModbusDataWriter.GetFileDate(DateEnd, RWModeEnum.hh).AddMinutes(-30);
				DateStart = DateEnd.AddMinutes(-depth * 30);
			} else {
				DateEnd = ModbusDataWriter.GetFileDate(DateEnd, RWModeEnum.min).AddMinutes(-1);
				DateStart = DateEnd.AddMinutes(-depth * 1);
			}

			if (lastProcessed < DateStart) {
				DateStart = lastProcessed;
			}
			return processDate( DateStart, DateEnd, mode);			
		}

		public DateTime Process(DateTime DateStart, DateTime DateEnd, RWModeEnum mode) {
			DateTime de=ModbusDataWriter.GetFileDate(DateEnd, mode, false);
			DateTime now=ModbusDataWriter.GetFileDate(DateTime.Now, mode, true);
			DateEnd = de > now ? now : de;
			return processDate( DateStart, DateEnd, mode);
		}
		
		protected DateTime processDate(DateTime DateStart, DateTime DateEnd,RWModeEnum mode) {		
			Logger.Info(String.Format("{0}: {1}  {2} -- {3}",DateTime.Now,mode,DateStart,DateEnd));
			DateTime date=DateStart.AddHours(0);
			SortedList<string,DateTime> dtList=new SortedList<string, DateTime>();
			while (date <= DateEnd) {
				foreach (string idInitArray in InitArrays.Keys) {
					if (!dtList.ContainsKey(idInitArray)) {
						dtList.Add(idInitArray, DateTime.MaxValue);
					}

					Logger.Info(String.Format("=={0} {1}", date, idInitArray));
					try {
						DataDBWriter writer=Writers[idInitArray];
						List<String> fileNames=new List<string>();
						fileNames.Add(ModbusDataWriter.GetFileName(Settings.single.DataPath, InitArrays[idInitArray], mode, date, false));
						foreach (string path in Settings.single.AddDataPath) {
							try {
								fileNames.Add(ModbusDataWriter.GetFileName(path, InitArrays[idInitArray], mode, date, false));
							} catch { }
						}
						bool ready=writer.init(fileNames);
						if (ready) {
							writer.ReadAll();
							writer.writeData(mode);
							dtList[idInitArray] = date;
							Logger.Info("====ok");
						}
					} catch (Exception e) {
						Logger.Error("Ошибка при записи в базу");
						Logger.Info(e.ToString());
					} 
				}
				date = mode == RWModeEnum.hh ? date.AddMinutes(30) : date.AddMinutes(1);
			}
			return dtList.Values.Min();
		}


		public void InitRun(int sleepTimeMin, int depthHH, int depthMin) {
			SleepTimeMin = sleepTimeMin;
			this.DepthHH = depthHH;
			this.DepthMin = depthMin;
		}

		public void Run() {
			LastProcessedData.Read();
			Process(DateTime.Now, RWModeEnum.hh, DepthHH, LastProcessedData.Single.LastProcessedHH);
			while (true) {					
				if (DepthMin >= 0) {
					Logger.Info("====================MIN==================");
					LastProcessedData.Single.LastProcessedMin = Process(DateTime.Now, RWModeEnum.min, DepthMin, LastProcessedData.Single.LastProcessedMin);
				}
				if (DateTime.Now.Minute % 30 < 5 && DateTime.Now.Minute % 30 >= 1 && LastHHDate.AddMinutes(20) < DateTime.Now) {
					Logger.Info("====================HH===================");
					LastProcessedData.Single.LastProcessedHH = Process(DateTime.Now, RWModeEnum.hh, DepthHH, LastProcessedData.Single.LastProcessedHH);
					LastHHDate = DateTime.Now;
				}
				LastProcessedData.Write();
				Thread.Sleep(SleepTimeMin);			
			}
		}


	}
}
