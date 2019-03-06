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
			this.DatabaseContext = new ChatDbContext();
		}

		public readonly ClientService Client = ClientBaseService.Initialize<ClientService>();
		public ChatDbContext DatabaseContext { get; }
	}

	
}
