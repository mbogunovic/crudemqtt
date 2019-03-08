using Chat.Client.Context;
using Chat.DomainModel.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
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
			room.CurrentUserId = this._context.Client.Id;

			Room = room;
			InitializeComponent();
			this.DataContext = this;

			_context.Client.OnChatChange += OnChatChange;
		}

		#region [Window Functionalities]

		private void Toolbar_MouseDown(object sender, MouseButtonEventArgs e) =>
			DragMove();

		private void Close_MouseDown(object sender, MouseButtonEventArgs e) =>
			this.Close();

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

		#region [Events]

		private void OnChatChange(object sender, EventArgs e)
		{
			if (sender != null
				&& Guid.TryParse(sender.ToString(), out Guid roomId)
				&& roomId.Equals(this.Room.Id))
			{
				this.Room.RoomUsers = this._context.DatabaseContext.RoomsRepository.GetById(this.Room.Id).RoomUsers;
				this.Room.RaisePropertyChanged(nameof(Room.Messages));
				this.Dispatcher.Invoke(() => this.scvMessages.ScrollToBottom());
			}
		}

		private void BtnSend_Click(object sender, RoutedEventArgs e) =>
			SendMessage();

		private void tbMessage_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
				SendMessage();
		}

		#endregion

		#region [Methods]

		private void SendMessage()
		{
			var message = this.tbMessage.Text;
			if (!string.IsNullOrWhiteSpace(message))
			{

				var roomUserId = this.Room.RoomUsers.FirstOrDefault(x => x.UserId.Equals(this._context.Client.Id)).Id;
				if (roomUserId == Guid.Empty)
					throw new NullReferenceException($"One of the following references are not set: {nameof(roomUserId)}.");

				this._context.DatabaseContext.MessagesRepository.Add(new Message(roomUserId, message, DateTime.Now));

				this._context.Client.ChatChange(this.Room.Id);
				this.tbMessage.Text = string.Empty;
			}
		}

		#endregion
	}
}
