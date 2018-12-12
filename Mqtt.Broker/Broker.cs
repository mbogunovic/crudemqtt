using Mqtt.BrokerClient;
using System;
using uPLibrary.Networking.M2Mqtt;

namespace CRUDE.Mqtt.Broker
{
	internal class Broker
	{
		private MqttBroker service { get; }

		private Broker()
		{
			service = new MqttBroker();
		}

		internal static void Initialize()
		{
			Broker broker = new Broker();
			broker.service.Start();

			BrokerClient.Initialize<BrokerClient>();
			Console.WriteLine("Press any key to stop the service");
			Console.ReadKey();

			broker.service.Stop();
		}
	}
}
