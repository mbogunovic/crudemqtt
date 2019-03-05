using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.DomainModel.Domain
{
	public abstract class BaseModel : BaseViewModel
	{
		[Key]
		public Guid Id { get; set; }

		[NotMapped]
		public Guid CurrentUserId { get; set; }
	}
}
