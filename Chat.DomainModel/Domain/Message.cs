using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.DomainModel.Domain
{
	public class Message : BaseModel
	{
		public virtual User User { get; set; }
		public virtual Room Room { get; set; }

		[Required]
		[ForeignKey("User")]
		public Guid UserId { get; set; }

		[Required]
		[ForeignKey("Room")]
		public Guid RoomId { get; set; }

		[Required]
		public DateTime SentDate { get; set; }

		[Required]
		public string Text { get; set; }

		public virtual ICollection<Message> Messages { get; set; }
	}
}
