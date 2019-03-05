using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

		public virtual ICollection<RoomUser> RoomUsers { get; set; }

		[NotMapped]
		public ObservableCollection<Message> Messages => GetMessages();


		private ObservableCollection<Message> GetMessages()
		{
			var result = this.RoomUsers
			.SelectMany(x => x.Messages)
			.OrderBy(x => x.SentDate)
			.ToList() ?? new List<Message>();

			result.ForEach(x => x.CurrentUserId = this.CurrentUserId);

			return new ObservableCollection<Message>(result);
		}

		[NotMapped]
		public int NoOfActiveUsers => this.RoomUsers.Where(x => x.IsActive).Count();
		[NotMapped]
		public int NoOfOnlineUsers => this.RoomUsers.Where(x => x.IsChatting).Count();

	}
}
