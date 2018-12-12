using Mqtt.Common.Domains;
using Mqtt.DomainModel.Domain;
using Newtonsoft.Json;
using System;
using Mqtt.Common.Models;
using uPLibrary.Networking.M2Mqtt.Messages;
using static Mqtt.Common.Constants;

namespace Mqtt.Client
{
	class Client : ClientBase
	{
		protected override void Execute()
		{
			var room = new Room() {CreatedById = this.Id, Name = "First room!" };

			this.Publish(JsonConvert.SerializeObject(new RoomActionModel(RoomActions.CreateRoom, room)), ROOM_TOPIC);

			Console.ReadKey();
		}

		protected override void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			if (e.Topic.Equals(ROOM_TOPIC))
			{
				Console.WriteLine($"At {DateTime.Now.ToString()}, Anonymous:{System.Text.Encoding.UTF8.GetString(e.Message)}");
			}
		}
	}
}
