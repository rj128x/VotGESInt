﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModbusLib;
using VotGES;
using VotGES.Piramida;

namespace DBConsole
{
	class Program
	{
		static void Main(string[] args) {

			try {
				Settings.init();
				DBSettings.init();
				Logger.init(Logger.createFileLogger(Settings.single.LogPath, "logW", new Logger()));

				MasterDBWriter writer=new MasterDBWriter();
				writer.InitRun(30000, 3, -1);
				writer.Run();
				
				Console.ReadLine();
			} catch (Exception e) {
				//Logger.Error(e.ToString());
				Console.WriteLine(e.ToString());
			}
		}
	}
}
