using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModbusLib;
using System.Net;
using System.IO;
using System.Threading;
using VotGES;
using VotGES.Piramida;

namespace ModbusConsole
{
	class Program
	{

		static void Main(string[] args) {
			Settings.init();
			DBSettings.init();
			/*Settings.single.InitFiles = new List<string>();
			Settings.single.InitFiles.Add("fsd");
			XMLSer<Settings>.toXML(Settings.single,"c:\\out1.xml");*/
			Logger.InitFileLogger(Settings.single.LogPath, "logR");

			try {
				MasterModbusReader reader=new MasterModbusReader(5000);
				reader.Read();
			}catch(Exception e){
				Logger.Error(e.ToString());
			}


			Console.ReadLine();
		}

	}
}

