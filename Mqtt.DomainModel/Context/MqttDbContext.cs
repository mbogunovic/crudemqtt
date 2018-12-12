using Mqtt.DomainModel.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Mqtt.DomainModel.Repository.Definitions;
using Mqtt.DomainModel.Repository.Interfaces;

namespace Mqtt.DomainModel.Context
{
	public class MqttDbContext : DbContext
	{
		public MqttDbContext() : base("name=domainDb")
		{
		}

		public IUserRepository Users => new UserRepository(this);
		public IRoomRepository Rooms => new RoomRepository(this);
		public IMessageRepository Messages => new MessageRepository(this);

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
		}
	}
}
