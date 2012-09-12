using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusLib
{

	public class ModbusServer
	{
		public string IP { get; protected set; }
		public ushort Port { get; protected set; }

		private Master modbusMaster;
		public Master ModbusMaster {
			get { 
				if (!modbusMaster.connected){
					modbusMaster.connect(IP, Port);
				}
				return modbusMaster;
			}
			protected set { modbusMaster = value; }
		}

		public ModbusServer(string ip, ushort port) {
			this.IP = ip;
			this.Port = port;
			this.modbusMaster = new Master(ip, port);
		}		
		
	}

	public delegate void FinishEvent(string InitArrayID,SortedList<int, double> ResultData);

	public class ModbusDataReader
	{
		public event FinishEvent OnFinish;
		public SortedList<int, int> Data { get; protected set; }

		public int CountData { get; protected set; }
		public ushort StepData { get; protected set; }
		public ModbusServer Server { get; protected set; }
		public ModbusInitDataArray InitArr { get; protected set; }

		public ModbusDataReader(ModbusServer server, ModbusInitDataArray initArr) {
			this.Server = server;
			this.CountData = initArr.MaxAddr;
			this.InitArr = initArr;
			Data = new SortedList<int, int>(CountData);
			StepData = 50;
			server.ModbusMaster.OnResponseData += new Master.ResponseData(ModbusMaster_OnResponseData);
		}

		protected ushort startAddr;
		protected bool finished;

		public void readData() {
			startAddr=0;
			finished = false;
			Data.Clear();
			continueRead();
		}

		protected void continueRead() {
			Server.ModbusMaster.ReadInputRegister(startAddr, startAddr, (ushort)(StepData * 2));
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
				if (Data.ContainsKey(startAddr)) {
					Data[startAddr] = w;
				} else {
					Data.Add(startAddr, w);
				}
				startAddr++;
			}

			if (!finished) {
				continueRead();
			} else {
				SortedList<int, double> ResultData=getResultData();
				if (OnFinish != null) {
					OnFinish(InitArr.ID, ResultData);
				}
			}
		}

		public SortedList<int, double> getResultData() {
			SortedList<int, double> ResultData=new SortedList<int,double>(CountData);
			foreach (ModbusInitData initData in InitArr.Data) {
				if (Data.ContainsKey(initData.Addr)) {
					ResultData.Add(initData.Addr, Data[initData.Addr] * initData.Scale);
				}
			}
			return ResultData;
		}

	}

}
