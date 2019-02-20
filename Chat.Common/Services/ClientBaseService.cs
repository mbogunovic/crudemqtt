using Chat.Common.Models;
using Chat.DomainModel.Context;
using System;
using System.Net;
using System.Text;
using Chat.DomainModel.Domain;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Chat.Common.Services
{
	public abstract class ClientBaseService
	{
		protected MqttClient Client { get; }

		public Guid Id => this.Settings.ClientId;
		public string DisplayName => this.Settings.DisplayName;

		public void SetDisplayName(string displayName)
		{
			this.Settings.DisplayName = displayName;
			this.Settings.SaveChanges("settings.json", false);

			using (var db = new ChatDbContext())
			{
				db.UsersRepository.Update(new User(this.Id, displayName));
			}
		}

		private ClientSettingsModel Settings { get; }


		protected ClientBaseService()
		{
#pragma warning disable CS0618 // Type or member is obsolete
			Client = new MqttClient(IPAddress.Parse(Constants.MQTT_BROKER_ADDRESS), Constants.MQTT_BROKER_PORT, false, null, null, MqttSslProtocols.None);
#pragma warning restore CS0618 // Type or member is obsolete

			Client.MqttMsgPublishReceived += MqttMsgPublishReceived;

			this.Settings = ClientSettingsModel.GetSettings("settings.json");
		}

		public static T Initialize<T>() where T : ClientBaseService
		{
			T instance = (T)Activator.CreateInstance(typeof(T));

			try
			{
				if (!instance.Client.IsConnected)
					instance.Connect();

				instance.Execute();
				return instance;
			}
			catch(Exception e)
			{
				if (instance.Client.IsConnected)
					instance.Client.Disconnect();
			}

			return null;
		}

		protected void Connect() =>
			Client.Connect(this.Id.ToString());

		protected void Subscribe(string topic) =>
			Client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

		protected void Publish(string message, string topic) =>
			Client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);

		protected abstract void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e);
		protected abstract void Execute();
	}
}
