using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;
using System.Threading;

namespace ModbusLib
{
	public delegate void ErrorConnectDelegate();
	public delegate void ResponseDataDelegeate(ushort id, byte function, byte[] data);
	public class ModbusServer
	{
		public event ErrorConnectDelegate OnErrorConnect;
		public event ResponseDataDelegeate OnResponse;
		public string IP { get; protected set; }
		public ushort Port { get; protected set; }

		private Master modbusMaster;
		public Master ModbusMaster {
			get {
				return modbusMaster;
			}
			protected set { modbusMaster = value; }
		}

		public Master ModbusMasterCon {
			get {
				if (!modbusMaster.connected) {
					try {
						modbusMaster.connect(IP, Port);
					} catch {
						Logger.Error("Ошибка при подключении " + IP + ":" + Port);
						if (OnErrorConnect != null) {
							OnErrorConnect();
						}
					}
				}
				return modbusMaster;
			}
		}

		public void Init() {
			this.modbusMaster = new Master();
			this.modbusMaster.OnException += new Master.ExceptionData(modbusMaster_OnException);
			this.modbusMaster.OnResponseData += new Master.ResponseData(modbusMaster_OnResponseData);
		}

		void modbusMaster_OnResponseData(ushort id, byte function, byte[] data) {
			if (OnResponse != null) {
				OnResponse(id, function, data);
			}
		}

		void modbusMaster_OnException(ushort id, byte function, byte exception) {
			Logger.Error("Ошибка при чтении данных " + IP + ":" + Port);
			if (OnErrorConnect != null) {
				OnErrorConnect();
			}
		}


		public ModbusServer(string ip, ushort port) {
			this.IP = ip;
			this.Port = port;
			Init();
		}

	}

	public delegate void FinishEvent(string InitArrayID, SortedList<string, double> ResultData);

	public class ModbusDataReader
	{
		public event FinishEvent OnFinish;
		public SortedList<string, double> Data { get; protected set; }

		public int CountData { get; protected set; }
		public ushort StepData { get; protected set; }
		public ModbusServer Server { get; protected set; }
		public ModbusInitDataArray InitArr { get; protected set; }

		public ModbusDataReader(ModbusServer server, ModbusInitDataArray initArr) {
			this.Server = server;
			this.CountData = initArr.MaxAddr;
			this.InitArr = initArr;
			Data = new SortedList<string, double>(CountData);
			StepData = 50;
			server.OnResponse += new ResponseDataDelegeate(ModbusMaster_OnResponseData);
			server.OnErrorConnect += new ErrorConnectDelegate(server_OnErrorConnect);
			
		}

		protected void error() {
			if (!IsError) {
				IsError = true;
				initRead();
				try { Server.ModbusMaster.disconnect(); } catch { }
				try {	Server.Init();	} catch { }
				if (OnFinish != null) {
					OnFinish(InitArr.ID, null);
				}
			}
		}

		void server_OnErrorConnect() {			
			error();
		}
				

		protected ushort StartAddr {  get;  set; }
		protected bool IsError{ get;  set; }
		protected SortedList<int,bool> FinishedPart{ get;  set; }
		protected SortedList<int, bool> StartedPart { get;  set; }

		protected void initRead() {
			StartAddr = 0;

			FinishedPart = new SortedList<int, bool>();
			StartedPart = new SortedList<int, bool>();
			int sa=0;
			while (sa < CountData * 2) {
				FinishedPart.Add(sa, false);
				StartedPart.Add(sa, false);
				sa += (ushort)(StepData * 2);
			}
			Data.Clear();
		}

		public void readData() {
			Logger.Info(DateTime.Now + " " + InitArr.ID + "   start read");
			IsError = false;
			initRead();
			continueRead();

		}

		protected void continueRead() {
			if (!IsError) {
				if (StartedPart.Values.Contains(false)) {
					StartAddr = (ushort)StartedPart.First((KeyValuePair<int, bool> de) => { return de.Value == false; }).Key;
					StartedPart[StartAddr] = true;
					Server.ModbusMasterCon.ReadInputRegister(StartAddr, StartAddr, (ushort)(StepData * 2));
				} else if (FinishedPart.Values.Contains(false)) {
					Logger.Error("Не все данные считаны");
					error();
				} else {
					try {
						Server.ModbusMaster.disconnect();
					} catch { }
					SortedList<string, double> ResultData=getResultData();
					if (OnFinish != null) {
						OnFinish(InitArr.ID, ResultData);
					}
				}
			}
		}

		void ModbusMaster_OnResponseData(ushort id, byte function, byte[] data) {
			if (!IsError && StartAddr == id) {
				FinishedPart[id] = true;

				int[] word=new int[data.Length / 2];
				for (int i=0; i < data.Length; i = i + 2) {
					//word[i / 2] = data[i] * 256 + data[i + 1];
					byte w1=data[i];
					byte w2=data[i + 1];
					byte[] vals=new byte[] { w2, w1 };
					int w=BitConverter.ToInt16(vals, 0);
					word[i / 2] = w;
				}

				ushort startAddr=id;
				foreach (int w in word) {
					InitArr.WriteVal(startAddr, w, Data);
					startAddr++;
				}

				continueRead();
			} else if (!IsError){
				Logger.Error(String.Format("Сбой при чтении данных id={0} StartAddr={1} Started[id]={2}",id,StartAddr,StartedPart[id]));
				error();
			}
		}

		public SortedList<string, double> getResultData() {
			SortedList<string, double> ResultData=new SortedList<string, double>(CountData);
			double val=0;
			string nm;
			foreach (KeyValuePair<string,double> de in Data) {
				val = de.Value;
				nm = de.Key + "_FLAG";
				if (Data.ContainsKey(nm) && Data[nm] < 0) {
					val = Double.NaN;
				}
				ResultData.Add(de.Key, val);
			}
			return ResultData;
		}

	}

}
