using Chat.Client.Context;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Chat.Client
{
	/// <summary>
	/// Interaction logic for EditDisplayNameWindow.xaml
	/// </summary>
	public partial class EditDisplayNameWindow : Window
	{
		public readonly ClientContext s = Application.Current.FindResource("context") as ClientContext;

		private TextBlock tblDisplayName;

		public EditDisplayNameWindow(ref TextBlock tblDisplayName)
		{
			this.tblDisplayName = tblDisplayName;
			InitializeComponent();
			this.tbxDisplayName.Text = s.Client.DisplayName;
		}

		private void TbxDisplayName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				s.Client.SetDisplayName(this.tbxDisplayName.Text);
				this.tblDisplayName.Text = "Hello " + this.tbxDisplayName.Text;
				this.Close();
			}
		}
	}
}
