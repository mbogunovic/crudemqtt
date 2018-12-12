using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mqtt.DomainModel.Domain
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
	}
}
