using Mqtt.DomainModel.Domain;
using static Mqtt.Common.Constants;

namespace Mqtt.Common.Models
{
	public class UserActionModel
	{
		public UserActionModel(UserActions command, User user)
		{
			Command = command;
			User = user;
		}

		public UserActions Command { get; }
		public User User { get; }
	}
}
