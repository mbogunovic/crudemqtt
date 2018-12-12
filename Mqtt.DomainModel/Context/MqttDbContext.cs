using Mqtt.DomainModel.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Mqtt.DomainModel.Context
{
	public class MqttDbContext : DbContext
	{
		public MqttDbContext() : base("name=domainDb")
		{
		}

		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
		}
	}
}
