using Chat.Common.Services;
using Chat.DomainModel.Context;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Chat.Client.Services
{
	public class ClientService : ClientBaseService
	{
		protected override void Execute()
		{
		}

		protected override void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
		}
	}
}
