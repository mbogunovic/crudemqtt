using Mqtt.Common.Domains;

namespace Mqtt.Client
{
	class Program
	{
		static void Main() =>
			ClientBase.Initialize<Client>();
	}
}
