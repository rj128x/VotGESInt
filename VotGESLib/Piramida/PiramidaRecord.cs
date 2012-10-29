using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida
{
	public class PiramidaRecord
	{
		public int ObjType{get;protected set;}
		public int Obj { get; protected set; }
		public int Item { get; protected set; }
		public string Title { get; protected set; }
		public string Key { get; protected set; }
		public PiramidaRecord(int objType, int obj, int item, string title,string addId=""){
			ObjType = objType;
			Obj = obj;
			Item = item;
			Title = title;

			Key = String.Format("{0}-{1}-{2}", ObjType, Obj, Item);
			if (!String.IsNullOrEmpty(addId)) {
				Key += "-" + addId;
			}
		}
		
	}
}
