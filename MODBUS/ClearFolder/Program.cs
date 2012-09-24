using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ClearFolder
{
	class Program
	{
		public int CountDays{get;set;}
		public string Folder { get; set; }
		static void Main(string[] args) {
			(new Program()).Run(args);
		}

		public void Run(string[] args) {
			try {
				Folder = args[0];
				CountDays = Int32.Parse(args[1]);
				DirectoryInfo dirInfo=new DirectoryInfo(Folder);
				if (dirInfo.Exists) {
					ProcessFolder(dirInfo);
				}
			} catch { }
		}

		public void ProcessFolder(DirectoryInfo dirInfo) {
			DirectoryInfo[] dirs=dirInfo.GetDirectories();
			foreach (DirectoryInfo childDir in dirs) {
				ProcessFolder(childDir);
			}
			FileInfo[] files=dirInfo.GetFiles();
			foreach (FileInfo file in files) {
				if (file.LastWriteTime.Date.AddDays(CountDays) < DateTime.Now.Date) {
					try {
						file.Delete();
					} catch { }
				}
			}
			dirs=dirInfo.GetDirectories();
			files=dirInfo.GetFiles();
			if (files.Length == 0 && dirs.Length == 0) {
				try {
					dirInfo.Delete();
				} catch { }
			}
		}
	}
}
