using Newtonsoft.Json;
using System;
using System.IO;

namespace Chat.Common.Models
{
	public class ClientSettingsModel
	{
		public Guid ClientId { get; set; }
		public string DisplayName { get; set; }

		public static ClientSettingsModel GetSettings(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new Exception("Path is empty!");
			}

			ClientSettingsModel settings;

			if (!File.Exists(path))
			{
				using (StreamReader r = new StreamReader(File.Create(path)))
				{
					settings = JsonConvert.DeserializeObject<ClientSettingsModel>(r.ReadToEnd());
				}

			}
			else
			{
				using (StreamReader r = new StreamReader(path))
				{
					settings = JsonConvert.DeserializeObject<ClientSettingsModel>(r.ReadToEnd());
				}
			}

			if (settings == null)
				settings = new ClientSettingsModel();

			if (settings.ClientId.Equals(Guid.Empty))
			{
				settings.ClientId = Guid.NewGuid();
				settings.DisplayName = "Anon" + settings.ClientId.ToString();
				settings.SaveChanges(path);
			}

			return settings;
		}

		private void SaveChanges(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new Exception("Path is empty!");
			}

			using (StreamWriter w = File.CreateText(path))
			{
				new JsonSerializer().Serialize(w, this);
			}
		}
	}
}
