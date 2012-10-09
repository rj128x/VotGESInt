using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModbusLib;
using VotGES;
using VotGES.Piramida;
using ModbusLib.ModbusReader;
using ModbusLib.DBWriter;

namespace DBConsole
{
	class Program
	{
		static void Main(string[] args) {

			(new MasterDBWriterRunner()).Run(args);
			Console.ReadLine();
		}
	}
}
