using System;
using System.Collections.Generic;

namespace Mqtt.DomainModel.Domain
{
	public class User : BaseModel
	{
		public User() { }

		public User(Guid id)
		{
			this.Id = id;
		}

		public virtual ICollection<Room> CreatedRooms { get; set; }
	}
}
