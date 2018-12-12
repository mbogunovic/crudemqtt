using System.Data.Entity.Migrations;

namespace Mqtt.DomainModel.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<Context.MqttDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			ContextKey = "Mqtt.DomainModel.Context.MqttDbContext";
		}
	}
}
