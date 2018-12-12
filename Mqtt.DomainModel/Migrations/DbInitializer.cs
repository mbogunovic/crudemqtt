using Mqtt.DomainModel.Context;
using System.Data.Entity;

namespace Mqtt.DomainModel.Migrations
{
	public class DbInitializer : DropCreateDatabaseIfModelChanges<MqttDbContext>
	{
		protected override void Seed(MqttDbContext context) =>
			base.Seed(context);
	}
}
