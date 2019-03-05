using Chat.Common;
using Chat.Common.Services;
using System;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Chat.Client.Services
{
	public class ClientService : ClientBaseService
	{
		public event EventHandler OnRoomsChange;
		public event EventHandler OnChatChange;

		protected override void Execute()
		{
			this.Subscribe(Constants.LOBBY_TOPIC);
			this.Subscribe(Constants.CHAT_TOPIC);
		}

		protected override void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			switch (e.Topic)
			{
				case Constants.LOBBY_TOPIC:
					OnRoomsChange(null, EventArgs.Empty);
					break;
				case Constants.CHAT_TOPIC:
					if (e.Message != null && Guid.TryParse(System.Text.Encoding.UTF8.GetString(e.Message), out Guid roomId))
						OnChatChange(roomId, EventArgs.Empty);
					break;
			}
	}

		public void RoomsChange() =>
			this.Publish("", Constants.LOBBY_TOPIC);

		public void ChatChange(Guid roomId) =>
			this.Publish(roomId.ToString(), Constants.CHAT_TOPIC);
	}
}
