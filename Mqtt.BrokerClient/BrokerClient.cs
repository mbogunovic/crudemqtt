using Mqtt.Common;
using System;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Mqtt.BrokerClient
{
	public class BrokerClient : ClientBase
	{
		protected override void Execute()
		{
			// place for additional execution options
		}

		protected override void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			if (e.Topic.Equals(Constants.MANAGEMENT_TOPIC))
			{
				Console.WriteLine($"At {DateTime.Now.ToString()}, Anonymous:{System.Text.Encoding.UTF8.GetString(e.Message)}");
			}
		}


	}
}
