using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;
using VotGES;

namespace ModbusLib.DBWriter
{
	public class MasterDBWriterRunner
	{
		public void Run(string[] args) {
			try {
				Settings.init();
				DBSettings.init();
				Logger.InitFileLogger(Settings.single.LogPath, "logW");
				MasterDBWriter writer=new MasterDBWriter();

				if (args.Length > 0) {
					DateTime dateStart=DateTime.Parse(args[0]);
					writer.Process(dateStart, DateTime.Now, (RWModeEnum)Enum.Parse(typeof(RWModeEnum), args[1]));					
				} else {
					writer.InitRun(60000, 3, 5);					
					writer.Run();
				}

			} catch (Exception e) {
				Logger.Error(e.ToString());
			}
			
		}
	}
}
