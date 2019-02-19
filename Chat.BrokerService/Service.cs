using System.ServiceProcess;

namespace Chat.BrokerService
{
	partial class Service : ServiceBase
	{
		private Broker broker;

		public Service()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			broker = new Broker();
			broker.InitializeAsync();
		}

		protected override void OnStop()
		{
			broker.Stop();
		}
	}
}
