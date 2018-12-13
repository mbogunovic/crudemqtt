using System;
using System.Net;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Mqtt.Common.Domains
{
	public abstract class ClientBase
	{
		protected MqttClient Client { get; }
		protected Guid Id { get; }

		protected ClientBase()
		{
#pragma warning disable CS0618 // Type or member is obsolete
			this.Client = new MqttClient(IPAddress.Parse(Constants.MQTT_BROKER_ADDRESS), Constants.MQTT_BROKER_PORT, false, null, null, MqttSslProtocols.None);
#pragma warning restore CS0618 // Type or member is obsolete

			this.Id = Guid.NewGuid();

			this.Client.MqttMsgPublishReceived += this.MqttMsgPublishReceived;
		}

		public static void Initialize<T>() where T : ClientBase
		{
			var brokerClient = (T)Activator.CreateInstance(typeof(T));
			brokerClient.Connect();
			brokerClient.Execute();
		}

		protected void Connect() =>
			this.Client.Connect(this.Id.ToString());

		protected void Subscribe(string topic) =>
			this.Client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

		protected void Publish(string message, string topic) =>
			this.Client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);

		protected abstract void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e);
		protected abstract void Execute();
	}
}
