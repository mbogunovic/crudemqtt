using System;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Mqtt.Common
{
	public abstract class ClientBase
	{
		protected MqttClient client { get; }
		protected string Id { get; }


		protected ClientBase()
		{
#pragma warning disable CS0618 // Type or member is obsolete
			this.client = new MqttClient(IPAddress.Parse(Constants.MQTT_BROKER_ADDRESS), Constants.MQTT_BROKER_PORT, false, null, null, MqttSslProtocols.None);
#pragma warning restore CS0618 // Type or member is obsolete
			this.Id = Guid.NewGuid().ToString();

			this.client.MqttMsgPublishReceived += this.MqttMsgPublishReceived;
		}

		public static void Initialize<T>() where T : ClientBase
		{
			var brokerClient = (T)Activator.CreateInstance(typeof(T));
			brokerClient.Connect();
			brokerClient.Subscribe(new string[] { Constants.MANAGEMENT_TOPIC });
			brokerClient.Execute();
		}

		protected void Connect() =>
			this.client.Connect(this.Id);

		protected void Subscribe(string[] topics) =>
			this.client.Subscribe(topics, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

		protected abstract void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e);
		protected abstract void Execute();
	}
}
