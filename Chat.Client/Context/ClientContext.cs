using Chat.Client.Services;
using System.Windows.Controls;
using Chat.Common.Services;
using Chat.DomainModel.Context;

namespace Chat.Client.Context
{
	public class ClientContext : ContentControl
	{
		public ClientContext()
		{
			DatabaseContext = new ChatDbContext();
		}

		private ChatDbContext dbContext;
		public readonly ClientService Client = ClientBaseService.Initialize<ClientService>();
		public readonly ChatDbContext DatabaseContext;
	}

	
}
