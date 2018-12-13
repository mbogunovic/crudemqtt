using Mqtt.Common.Domains;
using System;
using System.Threading;
using uPLibrary.Networking.M2Mqtt.Messages;
using static Mqtt.Common.Constants;

namespace Mqtt.Client
{
	class Client : ClientBase
	{
		private int cursorYPos;
		protected override void Execute()
		{
			Console.SetWindowSize(80, 30);
			Console.WriteLine("Welcome to anonymous chat, please choose one of the following options:");
			while (true)
			{
				Console.WriteLine("1. Create a room");
				Console.WriteLine("2. Join a room");
				Console.WriteLine("3. Exit");
				switch (Console.ReadKey().KeyChar)
				{
					case '1':
						this.Subscribe(ROOM_TOPIC + $"/{this.Id}");
						Console.WriteLine($"\nRoom successfully created, room id is: {this.Id}");
						this.Chatroom(this.Id);
						break;
					case '2':
						Guid roomId;
						while (true)
						{
							Console.Write("\nPlease enter valid room id:");
							if (Guid.TryParse(Console.ReadLine(), out roomId))
								break;
						}

						this.Subscribe(ROOM_TOPIC + $"/{roomId}");
						Console.WriteLine($"You have successfully joined a room with id: {roomId}");
						this.Chatroom(roomId);
						break;
					case '3':
						Environment.Exit(0);
						break;
					default:
						Console.WriteLine("\nWrong input. Please choose a valid option.");
						break;
				}
			}
		}

		private void Chatroom(Guid roomId)
		{
			Console.WriteLine("To exit a room enter \\q as message");
			bool flag = true;
			cursorYPos = Console.CursorTop;
			while (flag)
			{
				Thread.Sleep(200);
				Console.Write("You:");
				var message = Console.ReadLine();
				switch (message)
				{
					case "\\q":
						flag = false;
						break;
					default:
						this.Publish(message, ROOM_TOPIC + $"/{roomId}");
						break;
				}
			}
		}

		protected override void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			Console.SetCursorPosition(0, ++cursorYPos);
			Console.WriteLine($"Anonymous({DateTime.Now.ToString("HH:mm")}):{System.Text.Encoding.UTF8.GetString(e.Message)}");
		}
	}
}
