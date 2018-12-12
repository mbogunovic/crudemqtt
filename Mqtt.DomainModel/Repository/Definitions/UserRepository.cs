using Mqtt.DomainModel.Context;
using Mqtt.DomainModel.Repository.Interfaces;
using Mqtt.DomainModel.Domain;

namespace Mqtt.DomainModel.Repository.Definitions
{
	public class UserRepository : RepositoryBase<User>, IUserRepository
	{
		public UserRepository(MqttDbContext context) : base(context)
		{
		}
	}
}
