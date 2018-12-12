using Mqtt.DomainModel.Domain;
using static Mqtt.Common.Constants;

namespace Mqtt.Common.Models
{
	public class RoomActionModel
	{
		public RoomActionModel(RoomActions command, Room room)
		{
			Command = command;
			Room = room;
		}

		public RoomActions Command { get; }
		public Room Room { get; }
	}
}
