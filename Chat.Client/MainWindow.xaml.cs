using System.Linq;
using Chat.Client.Context;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chat.DomainModel.Domain;
using System.Collections.Generic;
using System;
using Chat.DomainModel.Context;

namespace Chat.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public readonly ClientContext _context = Application.Current.FindResource("context") as ClientContext;
		private List<RoomWindow> roomWindows = new List<RoomWindow>();

		public MainWindow()
		{
			if (_context.Client == null)
				Environment.Exit(0);

			InitializeComponent();

			tbDisplayName.Text = $"Hello {_context.Client.DisplayName}";
			UpdateHomeDataGrid();

			_context.Client.OnRoomsChange += OnRoomsChange;
		}

		#region [Events]

		private void OnRoomsChange(object sender, EventArgs e) =>
			UpdateHomeDataGrid();

		#endregion

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

		#region [MyRooms]

		private void btnCreateRoom_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(this.tbxRoomName.Text))
			{
				this._context.DatabaseContext.RoomsRepository.Add(new Room()
				{
					CreatedById = this._context.Client.Id,
					Name = this.tbxRoomName.Text
				});

				this._context.Client.RoomsChange();

				UpdateMyRoomsDataGrid();
			}
		}

		private void btnMyRooms_MouseDown(object sender, MouseButtonEventArgs e) =>
			UpdateMyRoomsDataGrid();

		private void UpdateMyRoomsDataGrid(string name = null) =>
			dgMyRooms.ItemsSource =
				this._context.DatabaseContext.RoomsRepository.GetAllByUserId(this._context.Client.Id)
					.Where(x => string.IsNullOrWhiteSpace(name) || x.Name.ToLower().Contains(name.ToLower()))
					.ToList();

		private void btnMyRoomsSave_Click(object sender, RoutedEventArgs e) {
			this._context.Client.RoomsChange();
			this._context.DatabaseContext.RoomsRepository.Update(this.dgMyRooms.CurrentItem as Room);
		}

		private void btnMyRoomsDelete_Click(object sender, RoutedEventArgs e)
		{
			this._context.DatabaseContext.RoomsRepository.Delete(this.dgMyRooms.CurrentItem as Room);
			this._context.Client.RoomsChange();
			UpdateMyRoomsDataGrid();
		}

		private void btnMyRoomsSearch_Click(object sender, RoutedEventArgs e) =>
			UpdateMyRoomsDataGrid(this.tbxMyRoomsSearch.Text);

		#endregion

		#region [Home]

		#region [Events]

		private void btnHome_MouseDown(object sender, MouseButtonEventArgs e)
		{
			UpdateHomeDataGrid();
		}

		private void TbDisplayName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			var popup = new EditDisplayNameWindow(ref this.tbDisplayName);
			popup.ShowDialog();
		}

		private void dgHome_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dgHome.SelectedItem != null)
				Clipboard.SetText((dgHome.SelectedItem as Room).Id.ToString());
		}

		private void dgHome_MouseDown(object sender, MouseButtonEventArgs e)
		{
			var selectedRoom = dgHome.SelectedItem as Room;
			if (selectedRoom != null)
			{
				JoinOrEnterRoom(selectedRoom.Id);
			}
		}

		private void btnJoinRoom_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(this.tbxJoinRoom.Text) && Guid.TryParse(this.tbxJoinRoom.Text, out Guid roomId))
				JoinOrEnterRoom(roomId);
		}

		private void BtnHomeSearch_Click(object sender, RoutedEventArgs e) =>
			UpdateHomeDataGrid(this.tbxHomeSearch.Text);

		#endregion

		#region [Methods]

		private void JoinOrEnterRoom(Guid roomId)
		{
			using (ChatDbContext dbContext = _context.DatabaseContext)
			{
				Guid currentUserId = _context.Client.Id;
				Room room = dbContext.RoomsRepository.GetById(roomId);
				RoomUser roomUser = room.RoomUsers
					.FirstOrDefault(x => x.RoomId.Equals(roomId)
										 && x.UserId.Equals(currentUserId));
				if (roomUser == null)
				{
					roomUser = new RoomUser()
					{
						IsActive = true,
						IsChatting = true,
						RoomId = roomId,
						UserId = currentUserId
					};

					dbContext.RoomUsersRepository.Add(roomUser);
				}
				else
				{
					roomUser.IsChatting = true;
					dbContext.RoomUsersRepository.Update(roomUser);
				}

				RoomWindow roomWindow = new RoomWindow(room);
				roomWindow.Show();
				roomWindows.Add(roomWindow);
				_context.Client.RoomsChange();
			}
		}

		private void UpdateHomeDataGrid(string query = null) =>
		this.Dispatcher.Invoke(() => dgHome.ItemsSource =
			this._context.DatabaseContext.RoomsRepository.GetAllAvailableByUserId(this._context.Client.Id)
				.Where(x => string.IsNullOrWhiteSpace(query) 
					|| x.Name.ToLower().Contains(query.ToLower())
					|| x.User.DisplayName.ToLower().Contains(query.ToLower()))
				.ToList());
		#endregion

		#endregion
	}
}
