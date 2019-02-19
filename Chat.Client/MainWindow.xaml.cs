using Chat.Client.Context;
using System.Windows;
using System.Windows.Input;

namespace Chat.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public readonly ClientContext s = Application.Current.FindResource("context") as ClientContext;

		public MainWindow()
		{
			InitializeComponent();

			if (s._service == null)
				Application.Current.Shutdown();
		}


		private void TbDisplayName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			var popup = new EditDisplayNameWindow(ref this.tbDisplayName);
			popup.ShowDialog();
		}

		private void Close_MouseDown(object sender, MouseButtonEventArgs e) =>
			Application.Current.Shutdown();

		private void Maximize_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.WindowState = WindowState.Maximized;
			this.maximize.Visibility = Visibility.Hidden;
			this.restoreDown.Visibility = Visibility.Visible;
		}

		private void RestoreDown_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.WindowState = WindowState.Normal;
			this.maximize.Visibility = Visibility.Visible;
			this.restoreDown.Visibility = Visibility.Hidden;
		}

		private void Minimize_MouseDown(object sender, MouseButtonEventArgs e) =>
			this.WindowState = WindowState.Minimized;
	}
}
