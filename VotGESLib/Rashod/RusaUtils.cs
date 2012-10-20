using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VotGES.Rashod
{
	public class RusaUtils
	{
		public static void createRusaOptim(bool diff=false) {
			SortedList<double,SortedList<double,double>> DATA=new SortedList<double,SortedList<double,double>>();
			List<double> napors=new List<double>();
			List<double> powers=new List<double>();
			double napor=RashodTable.getRashodTable(1).minNapor;
			for (int power=35; power <= 1020; power++) {
				powers.Add(power);
				DATA.Add(power, new SortedList<double, double>());
			}
			List<int>gens=(new int[]{1,2,3,4,5,6,7,8,9,10}).ToList();

			while (napor <= RashodTable.getRashodTable(1).maxNapor) {
				napors.Add(napor);
				foreach(double power in powers) {
					//double rashod= RUSADiffPower.getMinRashod(gens, napor, power, 1);
					double rashod=RUSA.getOptimRashod(power, napor);
					DATA[power].Add(napor, rashod);
				}
				napor += 0.1;
			}

			TextWriter writer=new StreamWriter("C:/avg.csv",false);
			writer.WriteLine("X;" + string.Join(";", napors));
			foreach (double power in powers) {
				writer.WriteLine(power.ToString() + ";" + string.Join(";", DATA[power].Values));
			}
			writer.Close();
		}
	}
}
