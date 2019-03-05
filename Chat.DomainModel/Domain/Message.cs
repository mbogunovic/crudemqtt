using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.DomainModel.Domain
{
	public class Message : BaseModel
	{
		public virtual RoomUser RoomUser { get; set; }

		public Message() { }

		public Message(Guid roomUserId, string text, DateTime sentDate)
		{
			this.RoomUserId = roomUserId;
			this.Text = text;
			this.SentDate = sentDate;
		}

		[Required]
		[ForeignKey("RoomUser")]
		public Guid RoomUserId { get; set; }

		[Required]
		public string Text { get; set; }

		[Required]
		public DateTime SentDate { get; set; }

		[NotMapped]
		public bool IsCurrentUsersMessage => this.CurrentUserId.Equals(this.RoomUser.UserId);
	}
}
