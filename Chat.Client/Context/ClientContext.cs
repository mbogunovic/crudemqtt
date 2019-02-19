using Chat.Client.Services;
using System.Windows.Controls;
using Chat.Common.Services;

namespace Chat.Client.Context
{
	public class ClientContext : ContentControl
	{
		public readonly ClientService _service = ClientBaseService.Initialize<ClientService>();
	}
}
