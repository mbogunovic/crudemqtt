using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Chat.DomainModel.Domain
{
	public class Room : BaseModel
	{
		public virtual User User { get; set; }

		[Required]
		[ForeignKey("User")]
		public Guid CreatedById { get; set; }

		[Required]
		public string Name { get; set; }

		public virtual ICollection<Message> Messages { get; set; }
		public virtual ICollection<RoomUser> RoomUsers { get; set; }

		[NotMapped]
		public int NoOfActiveUsers => this.RoomUsers.Where(x => x.IsActive).Count();
		[NotMapped]
		public int NoOfOnlineUsers => this.RoomUsers.Where(x => x.IsChatting).Count();

	}
}
