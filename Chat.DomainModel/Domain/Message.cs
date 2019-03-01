using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.DomainModel.Domain
{
	public class Message : BaseModel
	{
		public virtual RoomUser RoomUser { get; set; }

		[Required]
		[ForeignKey("RoomUser")]
		public Guid RoomUserId { get; set; }

		[Required]
		public string Text { get; set; }

		[Required]
		public DateTime SentDate { get; set; }
	}
}
