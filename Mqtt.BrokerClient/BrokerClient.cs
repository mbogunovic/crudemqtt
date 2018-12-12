using Mqtt.Common.Domains;
using Mqtt.Common.Models;
using Mqtt.DomainModel.Domain;
using Newtonsoft.Json;
using System;
using System.Linq;
using Mqtt.DomainModel.Context;
using uPLibrary.Networking.M2Mqtt.Messages;
using static Mqtt.Common.Constants;

namespace Mqtt.BrokerClient
{
	public class BrokerClient : ClientBase
	{
		private MqttDbContext _context { get; set; }

		protected override void Execute()
		{
			this._context = new MqttDbContext();
		}

		protected override void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			switch (e.Topic)
			{
				case ROOM_TOPIC:
					var roomAction = JsonConvert.DeserializeObject<RoomActionModel>(System.Text.Encoding.UTF8.GetString(e.Message));
					switch (roomAction.Command)
					{
						case RoomActions.CreateRoom:
							Console.WriteLine($"Id:{roomAction.Room.CreatedById}\n Room:{roomAction.Room.Name}");
							break;
					}
					Console.WriteLine($"At {DateTime.Now.ToString()}, Anonymous:{System.Text.Encoding.UTF8.GetString(e.Message)}");
					break;
				case USER_TOPIC:
					var userAction = JsonConvert.DeserializeObject<UserActionModel>(System.Text.Encoding.UTF8.GetString(e.Message));

					if (_context.Users.GetById(userAction.User.Id) == null)
					{
						_context.Users.Add(userAction.User);
					};

					var s = _context.Rooms.GetAllByUserId(userAction.User.Id);

					break;
			}
		}


	}
}
