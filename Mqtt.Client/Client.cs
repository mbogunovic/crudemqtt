using Mqtt.Common;
using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Mqtt.Client
{
	class Client : ClientBase
	{
		protected override void Execute()
		{
			client.Publish(Constants.MANAGEMENT_TOPIC, Encoding.UTF8.GetBytes("test"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
			Console.ReadKey();
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
