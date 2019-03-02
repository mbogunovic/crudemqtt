using Chat.Client.Context;
using Chat.DomainModel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chat.Client
{
    /// <summary>
    /// Interaction logic for RoomWindow.xaml
    /// </summary>
    public partial class RoomWindow : Window
    {
		public readonly ClientContext _context = Application.Current.FindResource("context") as ClientContext;
		public Room Room { get; set; }

        public RoomWindow(Room room)
        {
			Room = room;
            InitializeComponent();
			this.DataContext = this;
		}

		#region [Window Functionalities]

		private void Close_MouseDown(object sender, MouseButtonEventArgs e) =>
			Environment.Exit(0);

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

		#endregion

		private void BtnSend_Click(object sender, RoutedEventArgs e)
		{
			var msg = new Message()
			{
				RoomUserId = this.Room.RoomUsers.FirstOrDefault(x => x.UserId.Equals(this._context.Client.Id)).Id,
				SentDate = DateTime.Now,
				Text = this.tbMessage.Text
			};

			this._context.DatabaseContext.MessagesRepository.Add(msg);
			this.Room = this._context.DatabaseContext.RoomsRepository.GetById(this.Room.Id);
			this.Room.RaisePropertyChanged(nameof(Room.Messages));
			this.tbMessage.Text = string.Empty;
		}
	}
}
