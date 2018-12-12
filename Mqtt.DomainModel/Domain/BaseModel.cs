using System;
using System.ComponentModel.DataAnnotations;

namespace Mqtt.DomainModel.Domain
{
	public abstract class BaseModel
	{
		[Key]
		public Guid Id { get; set; }
	}
}
