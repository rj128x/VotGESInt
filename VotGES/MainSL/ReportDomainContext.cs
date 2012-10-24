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
using System.Reflection;
using System.ServiceModel.DomainServices.Client;
using System.ServiceModel;
using MainSL.Logging;

namespace VotGES.Web.Services
{
	public partial class ReportBaseDomainContext
	{
		partial void OnCreated() {
			PropertyInfo channelFactoryProperty = DomainClient.GetType()
																	.GetProperty("ChannelFactory");
			if (channelFactoryProperty == null) {
				Logger.info("There is no 'ChannelFactory' property on the DomainClient.");
			} else {
				ChannelFactory factory = (ChannelFactory)channelFactoryProperty.GetValue(DomainClient, null);

				factory.Endpoint.Binding.SendTimeout = new TimeSpan(0, 10, 0);
			}
		}
	}
}
