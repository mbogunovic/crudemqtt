using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.DomainModel.Domain
{
	public class RoomUser : BaseModel
	{
		public virtual User User { get; set; }
		public virtual Room Room { get; set; }

		[Required]
		[ForeignKey("User")]
		public Guid UserId { get; set; }

		[Required]
		[ForeignKey("Room")]
		public Guid RoomId { get; set; }

		public bool IsActive { get; set; }
		public bool IsChatting { get; set; }

		public virtual ICollection<Message> Messages { get; set; }
	}
}
