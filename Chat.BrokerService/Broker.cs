using uPLibrary.Networking.M2Mqtt;

namespace Chat.BrokerService
{
	internal class Broker
	{
		private MqttBroker service { get; }

		internal Broker()
		{
			service = new MqttBroker();
		}

		internal void Initialize() =>
			this.service.Start();

		internal void Stop() =>
			this.service.Stop();
	}
}
