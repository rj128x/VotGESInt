using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;
using System.Threading;

namespace ModbusLib
{
	public delegate void ErrorDelegate();
	public delegate void ResponseDataDelegeate(ushort id, byte function, byte[] data);
	public delegate void ConnectDelegate();
	public class ModbusServer
	{
		public event ErrorDelegate OnError;
		public event ResponseDataDelegeate OnResponse;
		public List<ServerInfo> Servers { get; protected set; }
		public ServerInfo CurrentServer { get; protected set; }

		protected Master.ExceptionData exceptionEvent;
		protected Master.ResponseData responseEvent;

		private Master modbusMaster;
		public Master ModbusMaster {
			get {
				return modbusMaster;
			}
			protected set { modbusMaster = value; }
		}

		protected void TryConnect() {
			ServerInfo serv=CurrentServer;
			while (!modbusMaster.connected) {
				try {
					modbusMaster.connect(serv.IP, serv.Port);
				} catch (Exception e) {
					Logger.Error("Ошибка при подключении " + serv.IP + ":" + serv.Port + "  " + e.Message);
					if (serv == Servers.Last()) {
						serv = Servers.First();
					} else {
						serv = Servers[Servers.IndexOf(serv) + 1];
					}
					if (serv == CurrentServer) {
						throw new Exception("Не отвечают все сервера");
					}
				}				
			}
			CurrentServer = serv;
		}

		public void ProcessConnect(ConnectDelegate OnConnect) {
			if (!modbusMaster.connected) {
				try {
					TryConnect();
				} catch (Exception e) {
					Logger.Error("Ошибка при подключении " + e.Message);
					if (OnError != null) {
						OnError();
					}
					return;
				}
			}			
			if (modbusMaster.connected) {
				if (OnConnect != null) {
					OnConnect();
				}
			}
		}
				
		public void Init() {
			if (this.modbusMaster != null) {
				this.modbusMaster.OnException -= exceptionEvent;
				this.modbusMaster.OnResponseData -= responseEvent;
				try { modbusMaster.disconnect(); } catch { }
			}
			this.modbusMaster = new Master();			
			this.modbusMaster.OnException += exceptionEvent;
			this.modbusMaster.OnResponseData += responseEvent;
			this.modbusMaster.timeout = 500;
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
				Logger.Error("Ошибка при чтении данных " + CurrentServer.IP + ":" + CurrentServer.Port);
				if (OnError != null) {
					OnError();
				}
			}
		}


		public ModbusServer(List<ServerInfo> Servers) {
			this.Servers = Servers;
			this.CurrentServer = Servers.First();
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
			server.OnError += new ErrorDelegate(server_OnError);
			
		}

		protected void error() {
			if (!IsError) {
				IsError = true;
				//initRead();				
				Server.Init();	
				if (OnFinish != null) {
					OnFinish(InitArr.ID, null);
				}
			}
		}

		void server_OnError() {			
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
			//Logger.Info("Init");
		}

		public void readData() {
			Logger.Info(DateTime.Now + " " + InitArr.ID + "   start read");
			IsError = false;
			initRead();
			continueRead();

		}

		protected void ProcessConnect() {
			StartAddr = (ushort)StartedPart.First((KeyValuePair<int, bool> de) => { return de.Value == false; }).Key;
			if (!InitArr.IsDiscrete) {
				Server.ModbusMaster.ReadInputRegister(StartAddr, StartAddr, (ushort)(StepData * 2));
			} else {
				Server.ModbusMaster.ReadDiscreteInputs(StartAddr, StartAddr, (ushort)(StepData / 8));
			}
			StartedPart[StartAddr] = true;
		}

		protected void continueRead() {
			if (!IsError) {
				if (StartedPart.Values.Contains(false)) {					
					Server.ProcessConnect(ProcessConnect);										
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
				Logger.Error(String.Format("Сбой при чтении данных"));
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
