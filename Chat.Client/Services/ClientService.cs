using Chat.Common.Services;
using Chat.DomainModel.Context;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Chat.Client.Services
{
	public class ClientService : ClientBaseService
	{
		protected override void Execute()
		{
			using(var db = new ChatDbContext())
			{
				var user = db.UsersRepository.GetById(this.Id);

				if (user == null)
				{
					App.Current.MainWindow = new MainWindow();
					App.Current.MainWindow.Show();
				}
			}
		}

		protected override void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{

		}
	}
}
