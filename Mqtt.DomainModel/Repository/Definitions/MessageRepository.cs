using Mqtt.DomainModel.Context;
using Mqtt.DomainModel.Repository.Interfaces;
using Mqtt.DomainModel.Domain;

namespace Mqtt.DomainModel.Repository.Definitions
{
	public class MessageRepository : RepositoryBase<Message>, IMessageRepository
	{
		public MessageRepository(MqttDbContext context) : base(context)
		{
		}
	}
}
