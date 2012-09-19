using System;
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
				Logger.InitFileLogger(Settings.single.LogPath, "logW");

				MasterDBWriter writer=new MasterDBWriter();
				writer.InitRun(30000, 3, -1);
				writer.Run();
				
				Console.ReadLine();
			} catch (Exception e) {
				//Logger.Error(e.ToString());
				Logger.Error(e.ToString());
			}
		}

		static void Main1(string[] args) {

			try {
				Settings.init();
				DBSettings.init();
				Logger.InitFileLogger(Settings.single.LogPath, "logW");

				MasterDBWriter writer=new MasterDBWriter();
				writer.Process(DateTime.Now, RWModeEnum.hh, 300);

				Console.ReadLine();
			} catch (Exception e) {
				//Logger.Error(e.ToString());
				Console.WriteLine(e.ToString());
			}
		}

		static void Test(string[] args) {

			try {
				Settings.init();
				DBSettings.init();
				Logger.InitFileLogger(Settings.single.LogPath, "logDB");

				DateTime DateStart=new DateTime(2012, 09, 01);
				DateTime DateEnd=new DateTime(2012, 09, 07);

				
				MasterDBWriter master=new MasterDBWriter();
				Random rand=new Random();
				foreach (ModbusInitDataArray init in master.InitArrays.Values) {
					List<int> headers=new List<int>();
					foreach (ModbusInitData dat in init.FullData.Values) {
						if (dat.WriteToDBHH) {
							headers.Add(dat.Addr);
						}
					}
					DateTime date=DateStart.AddMinutes(30);
					while (date < DateEnd) {
						Console.WriteLine(date);
						DataDBWriter writer=new DataDBWriter(init);
						writer.Data = new SortedList<int, DataDBRecord>();
						foreach (int header in headers) {
							DataDBRecord data=new DataDBRecord(header);
							data.Avg = rand.Next(1, 100);
							data.Min = rand.Next(1, 20);
							data.Max = rand.Next(80, 100);
							data.Eq = rand.Next(1, 100);
							writer.Data.Add(header, data);
						}
						writer.Date = date;
						writer.writeData(RWModeEnum.hh);
						date = date.AddMinutes(30);						
					}

					
				}

				Console.ReadLine();
			} catch (Exception e) {
				//Logger.Error(e.ToString());
				Console.WriteLine(e.ToString());
			}
		}
	}
}
