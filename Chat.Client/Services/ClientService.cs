using Chat.Common;
using Chat.Common.Services;
using System;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Chat.Client.Services
{
	public class ClientService : ClientBaseService
	{
		public event EventHandler OnRoomsChange;

		protected override void Execute()
		{
			this.Subscribe(Constants.LOBBY_TOPIC);
		}

		protected override void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			switch (e.Topic)
			{
				case Constants.LOBBY_TOPIC:
					OnRoomsChange(null, EventArgs.Empty);
					break;
				case Constants.ROOM_TOPIC:
					break;
			}
	}

		public void RoomsChange() =>
			this.Publish("", Constants.LOBBY_TOPIC);
	}
}
