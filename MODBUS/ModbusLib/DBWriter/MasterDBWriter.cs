﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using VotGES;
using VotGES.XMLSer;

namespace ModbusLib
{
	public class MasterDBWriter
	{
		private int sleepTimeMin;
		public int SleepTimeMin {
			get { return sleepTimeMin; }
			set { sleepTimeMin = value; }
		}

		private int depthHH;
		public int DepthHH {
			get { return depthHH; }
			set { depthHH = value; }
		}

		private int depthMin;
		public int DepthMin {
			get { return depthMin; }
			set { depthMin = value; }
		}

		private SortedList<string,ModbusInitDataArray> initArrays;
		public SortedList<string, ModbusInitDataArray> InitArrays {
			get { return initArrays; }
			set { initArrays = value; }
		}

		private SortedList<string,DataDBWriter> writers;
		public SortedList<string, DataDBWriter> Writers {
			get { return writers; }
			set { writers = value; }
		}

		private DateTime lastHHDate;
		public DateTime LastHHDate {
			get { return lastHHDate; }
			set { lastHHDate = value; }
		}
		
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

		public void Process(DateTime needDate, RWModeEnum mode, int depth) {
			DateTime DateEnd=needDate.AddMinutes(0);
			DateTime DateStart=needDate.AddMinutes(0);

			if (mode == RWModeEnum.hh) {
				DateEnd = ModbusDataWriter.GetFileDate(DateEnd, RWModeEnum.hh).AddMinutes(-30);
				DateStart = DateEnd.AddMinutes(-depth * 30);
			} else {
				DateEnd = ModbusDataWriter.GetFileDate(DateEnd, RWModeEnum.min).AddMinutes(-1);
				DateStart = DateEnd.AddMinutes(-depth * 1);
			}

			foreach (string id in InitArrays.Keys) {
				processDate(id, DateStart, DateEnd, mode);
			}
			
		}

		public void Process(DateTime DateStart, DateTime DateEnd, RWModeEnum mode) {
			DateTime de=ModbusDataWriter.GetFileDate(DateEnd, mode, false);
			DateTime now=ModbusDataWriter.GetFileDate(DateTime.Now, mode, true);
			DateEnd = de > now ? now : de;
			foreach (string id in InitArrays.Keys) {
				processDate(id, DateStart, DateEnd, mode);
			}
		}

		protected void processDate(string idInitArray, DateTime DateStart, DateTime DateEnd,RWModeEnum mode) {		
			Console.WriteLine(String.Format("{0}: {1}   {2} -- {3}",DateTime.Now,mode,DateStart,DateEnd));
			DateTime date=DateStart.AddHours(0);
			while (date <= DateEnd) {
				try {					
					DataDBWriter writer=Writers[idInitArray];
					bool ready=writer.init(ModbusDataWriter.GetFileName(InitArrays[idInitArray], mode, date, false));
					if (ready) {
						Console.Write(String.Format("=={0}",date));
						writer.ReadAll();
						writer.writeData(mode);			
						Console.WriteLine("-ok");
					}
				} catch (Exception e) {
					Logger.Error("Ошибка при записи в базу");
					Logger.Info(e.ToString());					
				} finally {
					date = date.AddMinutes(30);
				}
			}
		}


		public void InitRun(int sleepTimeMin, int depthHH, int depthMin) {
			SleepTimeMin = sleepTimeMin;
			this.DepthHH = depthHH;
			this.DepthMin = depthMin;
		}

		public void Run() {			
			Process(DateTime.Now, RWModeEnum.hh, DepthHH);
			while (true) {					
				if (DepthMin >= 0) {
					Process(DateTime.Now, RWModeEnum.min, depthMin);
				}				
				if (DateTime.Now.Minute % 30 < 5 && LastHHDate.AddMinutes(20)<DateTime.Now) {					
					Process(DateTime.Now, RWModeEnum.hh, DepthHH);
					LastHHDate = DateTime.Now;
				}
				Thread.Sleep(SleepTimeMin);			
			}
		}


	}
}
