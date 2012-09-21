using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;

namespace ModbusLib
{
	public delegate void ErrorConnectDelegate();
	public class ModbusServer
	{
		public event ErrorConnectDelegate OnErrorConnect;
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
					}catch{
						Logger.Error("Ошибка при подключении "+IP+":"+Port);
						if (OnErrorConnect != null) {
							OnErrorConnect();
						}
					}
				}
				return modbusMaster;
			}
		}


		public ModbusServer(string ip, ushort port) {
			this.IP = ip;
			this.Port = port;
			this.modbusMaster = new Master();
		}		
		
	}

	public delegate void FinishEvent(string InitArrayID,SortedList<string, double> ResultData);

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
			server.ModbusMaster.OnResponseData += new Master.ResponseData(ModbusMaster_OnResponseData);
			server.ModbusMaster.OnException += new Master.ExceptionData(ModbusMaster_OnException);
			server.OnErrorConnect += new ErrorConnectDelegate(server_OnErrorConnect);
		}

		void server_OnErrorConnect() {
			if (OnFinish != null) {
				OnFinish(InitArr.ID, null);
			}
		}

		void ModbusMaster_OnException(ushort id, byte function, byte exception) {
			if (OnFinish != null) {
				OnFinish(InitArr.ID, null);
			}
		}

		protected ushort startAddr;
		protected bool finished;

		public void readData() {
			Logger.Info(DateTime.Now + " " + InitArr.ID + "   start read");

			startAddr =0;
			finished = false;
			Data.Clear();
			continueRead();
		}

		protected void continueRead() {			
			Server.ModbusMasterCon.ReadInputRegister(startAddr, startAddr, (ushort)(StepData * 2));
			startAddr += (ushort)(StepData * 2);
			finished = (startAddr > CountData * 2);
		}

		void ModbusMaster_OnResponseData(ushort id, byte function, byte[] data) {

			int[] word=new int[data.Length / 2];
			for (int i=0; i < data.Length; i = i + 2) {
				//word[i / 2] = data[i] * 256 + data[i + 1];
				byte w1=data[i];
				byte w2=data[i + 1];
				byte[] vals=new byte[]{w2,w1};
				int w=BitConverter.ToInt16(vals, 0);
				word[i / 2] = w;
			}

			ushort startAddr=id;
			foreach (int w in word) {
				InitArr.WriteVal(startAddr, w, Data);
				startAddr++;
			}

			if (!finished) {
				continueRead();
			} else {
				try {
					Server.ModbusMaster.disconnect();
				}catch{}
				SortedList<string, double> ResultData=getResultData();
				if (OnFinish != null) {
					OnFinish(InitArr.ID, ResultData);
				}
			}
		}

		public SortedList<string, double> getResultData() {
			SortedList<string, double> ResultData=new SortedList<string,double>(CountData);
			double val=0;
			string nm;
			foreach (KeyValuePair<string,double> de in Data){
				val=de.Value;
				nm = de.Key + "_FLAG";
				if (Data.ContainsKey(nm) && Data[nm] != 0) {
					val = Double.NaN;
				}
				ResultData.Add(de.Key,val);
			}			
			return ResultData;
		}

	}

}
