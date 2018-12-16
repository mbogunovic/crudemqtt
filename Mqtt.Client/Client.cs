using Mqtt.Common.Domains;
using System;
using System.Collections.Generic;
using uPLibrary.Networking.M2Mqtt.Messages;
using static Mqtt.Common.Constants;

namespace Mqtt.Client
{
	class Client : ClientBase
	{
		private int cursorYPos;
		private IList<string> Messages { get; set; } = new List<string>();
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
						Console.Clear();
						var createdMessage = $"Room successfully created, room id is: {this.Id}";
						var createdMessage1 = "To exit a room enter \\q as message";
						Console.WriteLine(createdMessage);
						Console.WriteLine(createdMessage1);
						Messages.Add(createdMessage);
						Messages.Add(createdMessage1);
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
						Console.Clear();
						var joinedMessage = $"You have successfully joined a room with id: {roomId}";
						Console.WriteLine(joinedMessage);
						Messages.Add(joinedMessage);
						this.Chatroom(roomId);
						break;
					case '3':
						Environment.Exit(0);
						break;
					default:
						Console.Clear();
						Console.WriteLine("\nWrong input. Please choose a valid option.");
						break;
				}
			}
		}

		private void Chatroom(Guid roomId)
		{
			bool flag = true;
			Console.Write("You:");
			while (flag)
			{
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
			Messages.Add($"Anonymous({DateTime.Now.ToString("HH:mm:ss")}):{System.Text.Encoding.UTF8.GetString(e.Message)}");
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			cursorYPos = Console.CursorTop;

			foreach (var message in Messages)
			{
				Console.SetCursorPosition(0, cursorYPos++);
				Console.WriteLine(message);
			}

			Console.Write("You:");
		}
	}
}
