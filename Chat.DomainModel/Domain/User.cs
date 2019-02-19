using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chat.DomainModel.Domain
{
	public class User : BaseModel
	{
		public User() { }

		public User(Guid id, string displayName)
		{
			this.Id = id;
			this.DisplayName = displayName;
		}

		[Required]
		public string DisplayName { get; set; }

		public virtual ICollection<Room> CreatedRooms { get; set; }
	}
}
