using MQTTnet;
using MQTTnet.Server;
using System.Threading.Tasks;

namespace Chat.BrokerService
{
	internal class Broker
	{
		private IMqttServer service { get; }

		internal Broker()
		{
			service = new MqttFactory().CreateMqttServer();
		}

		internal async Task InitializeAsync() =>
			await service.StartAsync(new MqttServerOptions());

		internal void Stop() =>
			this.service.StopAsync();
	}
}
