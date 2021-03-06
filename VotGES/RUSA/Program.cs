﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;
using VotGES.Rashod;

namespace RUSA
{
	class Program
	{




		static void Main(string[] args) {

			Logger.init(new ConsoleLogger());

			List<double>powers=new List<double>();
			for (double power=50; power <= 1000; power += 50) {
				powers.Add(power);
			}

			List<double>powersAll=new List<double>();
			for (double power=50; power <= 1000; power++) {
				powersAll.Add(power);
			}

			List<double> napors=new List<double>();
			for (double napor=17; napor <= 23; napor += 1) {
				napors.Add(napor);
			}


			List<double> naporsAll=new List<double>();
			for (double napor=17; napor <= 22; napor += 0.1) {
				naporsAll.Add(napor);
			}


			CheckRusa.fn = "CHECK_RUSA.html";

			int[] gas=new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			//RUSADiffPowerFull.getMinRashod(gas.ToList(), 20, 300);
			CheckRusa.calc();

			//calcFull(powers, napors, "RUSA_FULL");
			//compareFull(powersAll, napors.ToList(), "RUSA_COMPARE_FULL");
			
			/*createMaxKPD(napors, "KPD_MAX.html");
			createMaxKPD1(napors, "KPD_MAX1.html");*/
			//createKPD(napors,powersAll, "KPD");


			/*calc(powersAll, naporsAll, true, @"d:\RUSA_BY_POWER.html", powers);
			calc(naporsAll, powersAll, false, @"d:\RUSA_BY_NAPOR.html", napors);*/


		}

		protected static string ArrToStr(SortedList<int, double> arr) {
			List<string> arrStr=new List<string>();
			foreach (KeyValuePair<int,double> de in arr) {
				if (de.Value > 0) {
					arrStr.Add(String.Format("{0}={1:0}", de.Key, de.Value));
				}
			}
			return String.Join(" ", arrStr);

		}

		protected static string ArrToTR(SortedList<int, double> arr) {
			string str="";
			for (int ga=1;ga<=10;ga++){
				double val=arr.Keys.Contains(ga) ? arr[ga] : 0;
				str += "<td>" + val + "</td>";				
			}
			return str;
		}
				

		public static void createMaxKPD(List<double> napors, string fn) {
			string res="";
			res += "<tr><th>h</th>";
			for (int ga=1; ga <= 10; ga++) {
				res += String.Format("<th>GA-{0}</th>", ga);
			}
			res += "</tr>";
			foreach (double napor in napors) {
				res += String.Format("<tr><th>{0}</th>", napor);
				Dictionary<int,double> kpdArr=RashodTable.KPDArr(napor);
				for (int ga=1; ga <= 10; ga++) {
					res += String.Format("<td>{0:0.00} ({1:0.00})</td>", kpdArr[ga], RashodTable.KPD(kpdArr[ga], napor, RashodTable.getRashod(ga, kpdArr[ga], napor)) * 100);
				}
				res += "</tr>";
			}
			res = String.Format("<table border='1'>{0}</table>", res);
			System.IO.File.WriteAllText(fn, res);
		}

		public static void createMaxKPD1(List<double> napors, string fn) {
			string res="";
			foreach (double napor in napors) {
				res += String.Format("<tr><th>{0}</th>", napor);
				Dictionary<int,double> kpdArr=RashodTable.KPDArr(napor);
				foreach (KeyValuePair<int,double> de in kpdArr) {
					res += String.Format("<td>{0}<br>{1:0.00}<br>{2:0.00}</td>", de.Key, de.Value, RashodTable.KPD(de.Value, napor, RashodTable.getRashod(de.Key, de.Value, napor)) * 100);
				}
				res += "</tr>";
			}
			res = String.Format("<table border='1'>{0}</table>", res);
			System.IO.File.WriteAllText(fn, res);
		}

		public static void createKPD(List<double> napors, List<double> powers, string fn) {
			for (int ga=1; ga <= 10; ga++) {
				string res="";
				res += "<tr><th>h</th>";

				foreach (double napor in napors) {
					res += String.Format("<th>{0}m</th>", napor);
				}
				res += "</tr>";
				for (double power=1; power <= 110; power++) {
					res += String.Format("<tr><th>{0}</th>", power);
					foreach (double napor in napors) {
						res += String.Format("<td>{0:0.00}", RashodTable.KPD(power, napor, RashodTable.getRashod(ga, power, napor)) * 100);
					}
					res += "</tr>";
				}
				res = String.Format("<table border='1'>{0}</table>", res);
				System.IO.File.WriteAllText(fn + "_" + ga + ".html", res);
			}
		}
		
		protected static void calcFull(List<double> powers, List<double> napors, string fn) {
			fn = fn + "_" + Guid.NewGuid().ToString() + ".html";
			List<int> sostav=new List<int>();

			double eq=0;
			double diff=0;
			double ideal=0;
			int[] allGAArr=new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			List<int> allGA=allGAArr.ToList();


			string res=String.Format("<table border='1'><tr><th>h</th><th>p</th><th>eq</th><th>Diff</th><th>kpdEq</th><th>kpdDiff</th><th>sostavEq</th><th>pEq</th><th>1</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th><th>8</th><th>9</th><th>10</th></tr>");
			System.IO.File.WriteAllText(fn, res);
			string str="";
			foreach (double power in powers) {				
				foreach (double napor in napors) {
					List<RUSADiffPower.RusaChoice> choices=RUSADiffPower.getChoices(allGA, napor, power);
					foreach (RUSADiffPower.RusaChoice choice in choices) {
						ideal = 1000 * power / (9.81 * napor);
						Console.Write(String.Format("{0,-3} {1,-3}", napor, power));
						eq = VotGES.Rashod.RUSA.getOptimRashod(power, napor, true, sostav);
						Console.Write(String.Format(" e={0:0.00} k={1:0.00} [{2}]", eq, ideal / eq * 100, String.Join("-", sostav)));
						sostav.Sort();
						diff = choice.rashod;
						SortedList<int,double> sostavOpt=choice.sostav;

						double q=0;
						foreach (KeyValuePair<int,double> de in sostavOpt) {
							q += RashodTable.getRashod(de.Key, de.Value, napor);
						}
						Console.Write("====" + (diff - q) + "=====");

						Console.WriteLine(String.Format(" d={0:0.00} k={1:0.00} [{2}]", diff, ideal / diff * 100, ArrToStr(sostavOpt)));

						str = String.Format("<tr><td>{0}</td><td>{1}</td><td>{2:0.00}</td><td>{3:0.00}</td><td>{4:0.0000}</td><td>{5:0.0000}</td><td>{6}</td><td>{7:0.00}</td>{8}</tr>",
							napor, power, eq, diff, ideal / eq * 100, ideal / diff * 100, String.Join(" ", sostav), power / sostav.Count, ArrToTR(sostavOpt));


						Console.WriteLine();
						System.IO.File.AppendAllText(fn, str);
					}
				}
			}
			System.IO.File.AppendAllText(fn, "</table>");
		}

		protected static void compareFull(List<double> powers, List<double> napors, string fn) {
			fn = fn + "_" + Guid.NewGuid().ToString() + ".html";
			List<int> sostav=new List<int>();

			double eq=0;
			double diff=0;
			double ideal=0;
			int[] allGAArr=new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			List<int> allGA=allGAArr.ToList();

			string res=String.Format("<table border='1'><tr><th>p</th>");
			res += "</tr>";
			
			System.IO.File.WriteAllText(fn, res);
			string str="";
			foreach (double power in powers) {
				str="";
				str+=String.Format("<th>{0}</th>",power);
				foreach (double napor in napors) {
					ideal = 1000 * power / (9.81 * napor);
					Console.Write(String.Format("{0,-3} {1,-3}", napor, power));
					eq = VotGES.Rashod.RUSA.getOptimRashod(power, napor, true, sostav);
					Console.Write(String.Format(" e={0:0.00} k={1:0.00} [{2}]", eq, ideal / eq * 100, String.Join("-", sostav)));
					sostav.Sort();
					diff = RUSADiffPower.getMinRashod(allGA, napor, power);
					SortedList<int,double> sostavOpt=RUSADiffPower.getMinSostav(allGA, napor, power);

					double q=0;
					foreach (KeyValuePair<int,double> de in sostavOpt) {
						q += RashodTable.getRashod(de.Key, de.Value, napor);
					}
					Console.Write("====" + (diff - q) + "=====");

					Console.WriteLine(String.Format(" d={0:0.00} k={1:0.00} [{2}]", diff, ideal / diff * 100, ArrToStr(sostavOpt)));

					//str += String.Format("<td>{0}</td>",(ideal / diff * 100)-(ideal/eq*100));
					str += String.Format("<td>{0}</td>", eq-diff);
					
					Console.WriteLine();					
				}
				str += "</tr>";
				System.IO.File.AppendAllText(fn, str);
			}
			System.IO.File.AppendAllText(fn, "</table>");
		}

		protected static void calc(List<double> X, List<double> Y, bool isFirstPower, string fn, List<double> XReport) {
			double minX=XReport.First();
			SortedList<double,SortedList<int,double>> result=new SortedList<double, SortedList<int, double>>();
			foreach (double x in XReport) {
				result.Add(x, new SortedList<int, double>());
				for (int ga=1; ga <= 10; ga++) {
					result[x].Add(ga, 0);
				}
			}

			double power=0;
			double napor=0;
			double rashod;

			SortedList<int,int>countByGA=new SortedList<int, int>();
			for (int ga=1; ga <= 10; ga++) {
				countByGA.Add(ga, 0);
			}

			SortedList<double,int>countByX=new SortedList<double, int>();
			foreach (double x in XReport) {
				countByX.Add(x, 0);
			}

			double allCount=0;
			List<int> sostav=new List<int>();

			string res="";
			double xRep;

			foreach (double x in X) {
				xRep = x < minX ? minX : XReport.Last(i => i <= x);

				foreach (double y in Y) {
					if (isFirstPower) {
						power = x;
						napor = y;
					} else {
						power = y;
						napor = x;
					}

					Console.Write(String.Format("{2} === {0:0.00} {1:0.00}", napor, power, xRep));
					rashod = VotGES.Rashod.RUSA.getOptimRashod(power, napor, true, sostav);
					Console.WriteLine(String.Format(" e={0:00000.00}, {1}", rashod, String.Join("-", sostav)));

					foreach (int ga in sostav) {
						result[xRep][ga] += 1;
						countByX[xRep] += 1;
						allCount++;
						countByGA[ga]++;
					}
				}

			}


			res = "<tr><th>X</th>";
			for (int ga=1; ga <= 10; ga++) {
				res += String.Format("<th>GA-{0}</th>", ga);
			}
			res += "</tr>";

			foreach (double x in XReport) {
				res += String.Format("<tr><th>{0}</th>", x);
				for (int ga=1; ga <= 10; ga++) {
					res += String.Format("<td>{0}</td>", result[x][ga] / (double)countByX[x] * 100.0);
				}
				res += "</tr>";
			}
			res += String.Format("<tr><th>Sum</th>");
			for (int ga=1; ga <= 10; ga++) {
				res += String.Format("<td>{0}</td>", (double)countByGA[ga] / (double)allCount * 100.0);
			}
			res += "</tr>";

			res = String.Format("<table>{0}</table>", res);
			System.IO.File.WriteAllText(fn, res);
		}
	}

}
