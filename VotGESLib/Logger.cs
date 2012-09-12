using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Config;
using System.Web;

namespace VotGES
{
	public class Logger
	{
		public enum LoggerSource { server, client, service, none }
		public log4net.ILog logger;

		protected static Logger context;

		public Logger() {
			
		}		


		public static  Logger  createFileLogger(string path, string name, Logger newLogger) {
			string fileName=String.Format("{0}/{1}_{2}.txt", path, name, DateTime.Now.ToString("dd_MM_yyyy"));
			PatternLayout layout = new PatternLayout(@"[%d] %-10p %m%n");
			FileAppender appender=new FileAppender();
			appender.Layout = layout;
			appender.File = fileName;
			appender.AppendToFile = true;
			BasicConfigurator.Configure(appender);
			appender.ActivateOptions();
			newLogger.logger = LogManager.GetLogger(name);
			return newLogger;
		}

		public static void init(Logger context) {
			Logger.context = context;
		}

		protected virtual string createMessage(string message, LoggerSource source = LoggerSource.none) {
			if (source != LoggerSource.none) {
				return String.Format("{0,-10} {1}", source.ToString(), message);
			} else {
				return String.Format("{0}", message);
			}
		}

		protected virtual void info(string str, LoggerSource source = LoggerSource.none) {
			logger.Info(createMessage(str, source));
			Console.WriteLine(createMessage(str, source));
		}

		protected virtual void error(string str, LoggerSource source = LoggerSource.none) {
			logger.Error(createMessage(str, source));
			Console.WriteLine(createMessage(str, source));
		}

		protected virtual void debug(string str, LoggerSource source = LoggerSource.none) {
			logger.Debug(createMessage(str, source));
			Console.WriteLine(createMessage(str, source));
		}


		public static void Info(string str, LoggerSource source = LoggerSource.none) {
			context.info(str, source);			
		}

		public static void Error(string str, LoggerSource source = LoggerSource.none) {
			context.info(str, source);
		}

		public static void Debug(string str, LoggerSource source = LoggerSource.none) {
			context.info(str, source);
		}

	}
}
