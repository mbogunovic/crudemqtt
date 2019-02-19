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
		private TextBlock tblDisplayName;

		public EditDisplayNameWindow(ref TextBlock tblDisplayName)
		{
			this.tblDisplayName = tblDisplayName;
			InitializeComponent();
		}

		private void TbxDisplayName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.tblDisplayName.Text = "Hello " + this.tbxDisplayName.Text;
				this.Close();
			}
		}
	}
}
