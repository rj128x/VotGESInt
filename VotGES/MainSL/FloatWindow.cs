using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MainSL
{
	public class FloatWindow
	{
		public static void OpenWindow(string url) {
			string host=Application.Current.Host.Source.Host;
			int port=Application.Current.Host.Source.Port;
			Uri uri=new Uri(String.Format("http://{0}:{1}/{2}", host, port,url));
			System.Windows.Browser.HtmlPopupWindowOptions options=new System.Windows.Browser.HtmlPopupWindowOptions();			
			options.Resizeable=true;
			options.Width=1100;
			options.Height=600;
			options.Menubar=true;
			options.Directories=true;
			options.Toolbar=true;
			options.Status=true;			
			System.Windows.Browser.HtmlPage.PopupWindow(uri, "", options);			
		}
	}
}
