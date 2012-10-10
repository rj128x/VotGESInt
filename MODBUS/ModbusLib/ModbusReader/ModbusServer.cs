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

		protected Master.ExceptionData exceptionEvent;
		protected Master.ResponseData responseEvent;

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
			if (this.modbusMaster != null) {
				this.modbusMaster.OnException -= exceptionEvent;
				this.modbusMaster.OnResponseData -= responseEvent;
			}
			this.modbusMaster = new Master();			
			this.modbusMaster.OnException += exceptionEvent;
			this.modbusMaster.OnResponseData += responseEvent;
			this.modbusMaster.timeout = 1;
		}

		void modbusMaster_OnResponseData(Master obj, ushort id, byte function, byte[] data) {
			if (modbusMaster == obj) {
				if (OnResponse != null) {
					OnResponse(id, function, data);
				}
			}
		}

		void modbusMaster_OnException(Master obj, ushort id, byte function, byte exception) {
			if (modbusMaster == obj) {
				Logger.Error("Ошибка при чтении данных " + IP + ":" + Port);
				if (OnErrorConnect != null) {
					OnErrorConnect();
				}
			}
		}


		public ModbusServer(string ip, ushort port) {
			this.IP = ip;
			this.Port = port;
			exceptionEvent = new Master.ExceptionData(modbusMaster_OnException);
			responseEvent = new Master.ResponseData(modbusMaster_OnResponseData);
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
			StepData = InitArr.IsDiscrete ? (ushort)(100 * 8) : (ushort)50;
			server.OnResponse += new ResponseDataDelegeate(ModbusMaster_OnResponseData);
			server.OnErrorConnect += new ErrorConnectDelegate(server_OnErrorConnect);
			
		}

		protected void error() {
			if (!IsError) {
				IsError = true;
				initRead();
				try { Server.ModbusMaster.disconnect(); } catch { }
				Server.Init();	
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

		protected List<int> getKeysForRead(int start, int end, int step) {
			int sa=0;
			List<int> res=new List<int>();
			while (sa <= end) {
				foreach (ModbusInitData data in InitArr.FullData.Values) {
					if (data.Addr >= sa && data.Addr <= sa + step) {
						res.Add(sa);
						break;
					}
				}
				sa += step;
			}
			return res;
		}

		protected void initRead() {
			StartAddr = 0;

			FinishedPart = new SortedList<int, bool>();
			StartedPart = new SortedList<int, bool>();
			List<int> keys;
			if (!InitArr.IsDiscrete) {
				keys = getKeysForRead(0, CountData * 2, StepData*2);
			} else {
				keys = getKeysForRead(0, CountData , StepData /8);
			}
			foreach (int key in keys) {
				FinishedPart.Add(key, false);
				StartedPart.Add(key, false);
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
					if (!InitArr.IsDiscrete) {
						Server.ModbusMasterCon.ReadInputRegister(StartAddr, StartAddr, (ushort)(StepData * 2));
					} else {
						Server.ModbusMasterCon.ReadDiscreteInputs(StartAddr, StartAddr, (ushort)(StepData / 8));
					}
					
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
				int[] word=null;

				if (!InitArr.IsDiscrete) {
					word = new int[data.Length / 2];
					for (int i=0; i < data.Length; i = i + 2) {
						byte w1=data[i];
						byte w2=data[i + 1];
						byte[] vals=new byte[] { w2, w1 };
						int w=BitConverter.ToInt16(vals, 0);
						word[i / 2] = w;
					}
				} else {
					word = new int[data.Length * 8];
					for (int i=0; i < data.Length; i = i+1) {
						byte w=data[i];
						string str=Convert.ToString(w, 2);
						while (str.Length < 8) {
							str = "0" + str;
						}						
						char[] chars=str.ToCharArray();
						for (int c=0; c < 8; c++) {
							int val=0;
							try {
								val = Int32.Parse(chars[c].ToString());
							} catch {}
							word[i * 8 + (7-c)] = val;
						}
					}
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
