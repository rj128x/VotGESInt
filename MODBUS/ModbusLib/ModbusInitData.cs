using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;

namespace ModbusLib
{
	public enum RWModeEnum { hh, min }
	
	public class ModbusInitData
	{
		[System.Xml.Serialization.XmlAttribute]
		public string Name { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int Addr { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public double Scale { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public bool WriteToDBMin { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public bool WriteToDBHH { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int ParNumberMin { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int ParNumberHH { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int Obj { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int ObjType { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public string DBNameMin { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public string DBNameHH { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int Item { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public string FuncName { get; set; }


		public ModbusInitData() {
			WriteToDBMin = false;
			WriteToDBHH = false;
			ParNumberMin = -1;
			ParNumberHH = -1;
			Obj = -1;
			ObjType = -1;
			DBNameMin=null;
			DBNameHH = null;
			Item = -1;
			FuncName = null;
		}
	}

	public class ModbusInitDataArray
	{
		public string ID { get; set; }
		public string IP { get; set; }
		public int Port { get; set; }
		public List<ModbusInitData> Data { get; set; }
		public bool WriteMin { get; set; }
		public bool WriteHH { get; set; }

		[System.Xml.Serialization.XmlIgnore]
		public SortedList<int, ModbusInitData> FullData { get; set; }

		[System.Xml.Serialization.XmlIgnore]
		public int MaxAddr { get; set; }

		public int ParNumberMin { get; set; }

		public int ParNumberHH { get; set; }

		public int Obj { get; set; }
		public int ObjType { get; set; }
		public string DBNameMin { get; set; }
		public string DBNameHH { get; set; }

		
		public void processData() {
			FullData = new SortedList<int, ModbusInitData>();
			MaxAddr = 0;
			foreach (ModbusInitData init in Data) {
				try {
					init.ParNumberHH = init.ParNumberHH == -1 ? ParNumberHH : init.ParNumberHH;
					init.ParNumberMin = init.ParNumberMin == -1 ? ParNumberMin : init.ParNumberMin;
					init.Obj = init.Obj == -1 ? Obj : init.Obj;
					init.ObjType = init.ObjType == -1 ? ObjType : init.ObjType;
					init.Item = init.Item == -1 ? init.Addr : init.Item;
					init.DBNameHH = init.DBNameHH == null ? DBNameHH : init.DBNameHH;
					init.DBNameMin = init.DBNameMin == null ? DBNameMin : init.DBNameMin;

					FullData.Add(init.Addr, init);
					if (MaxAddr < init.Addr) {
						MaxAddr = init.Addr;
					}
				} catch {
					Logger.Error(String.Format("Ошибка при добавлении записи Addr={0} Name={1}", init.Addr, init.Name));
				}
			}
			
		}

	}

}
