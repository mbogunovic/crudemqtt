using Newtonsoft.Json;
using System;
using System.IO;

namespace Mqtt.Common.Models
{
	public class ClientSettingsModel
	{
		public Guid ClientId { get; set; }

		public static ClientSettingsModel GetSettings()
		{
			using (StreamReader r = new StreamReader("settings.json"))
			{
				return JsonConvert.DeserializeObject<ClientSettingsModel>(r.ReadToEnd());
			}
		}

		public void SaveChanges()
		{
			using (StreamWriter w = File.CreateText("settings.json"))
			{
				new JsonSerializer().Serialize(w, this);
			}
		}
	}
}
