using System.Linq;
using Chat.Client.Context;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chat.DomainModel.Domain;
using System.Collections.Generic;
using System;

namespace Chat.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public readonly ClientContext _context = Application.Current.FindResource("context") as ClientContext;

		public MainWindow()
		{
			if (_context.Client == null)
				Application.Current.Shutdown();

			InitializeComponent();

			tbDisplayName.Text = $"Hello {_context.Client.DisplayName}";
			UpdateHomeDataGrid();
			_context.Client.OnRoomsChange += OnRoomsChange;
		}

		#region [Events]

		private void OnRoomsChange(object sender, EventArgs e) =>
			UpdateHomeDataGrid();

		#endregion

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

				this._context.Client.RoomCreated();

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

		private void btnMyRoomsSave_Click(object sender, RoutedEventArgs e) =>
			this._context.DatabaseContext.RoomsRepository.Update(this.dgMyRooms.CurrentItem as Room);

		private void btnMyRoomsDelete_Click(object sender, RoutedEventArgs e)
		{
			this._context.DatabaseContext.RoomsRepository.Delete(this.dgMyRooms.CurrentItem as Room);
			UpdateMyRoomsDataGrid();
		}

		private void btnMyRoomsSearch_Click(object sender, RoutedEventArgs e) =>
			UpdateMyRoomsDataGrid(this.tbxMyRoomsSearch.Text);

		#endregion

		private void btnHome_MouseDown(object sender, MouseButtonEventArgs e)
		{
			UpdateHomeDataGrid();
		}

		private void UpdateHomeDataGrid(string name = null) =>
			this.Dispatcher.Invoke(() => dgHome.ItemsSource =
				this._context.DatabaseContext.RoomsRepository.GetAll()
					.Where(x => string.IsNullOrWhiteSpace(name) || x.Name.ToLower().Contains(name.ToLower()))
					.ToList());
	}
}
