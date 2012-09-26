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
using ModbusLib.DBWriter;
using ModbusLib.ModbusReader;

namespace ModbusConsole
{
	class Program
	{

		static void Main(string[] args) {
			
			(new ModbusReaderRunner()).Run();

			Console.ReadLine();
		}

	}
}

