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
		public void Run() {
			try {
				Settings.init();
				DBSettings.init();
				Logger.InitFileLogger(Settings.single.LogPath, "logW");

				MasterDBWriter writer=new MasterDBWriter();
				writer.InitRun(30000, 3, -1);
				//writer.Process(new DateTime(2012, 10, 1, 7, 30, 0), new DateTime(2012, 10, 1, 7, 30, 0), RWModeEnum.hh);
				//writer.Process(new DateTime(2012, 10, 1, 8, 0, 0), new DateTime(2012, 10, 8, 13, 30, 0), RWModeEnum.hh);
				writer.Run();

			} catch (Exception e) {
				Logger.Error(e.ToString());
			}
			
		}
	}
}
