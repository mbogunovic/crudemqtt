using System;
using System.Collections;
using Mqtt.DomainModel.Domain;
using System.Collections.Generic;

namespace Mqtt.DomainModel.Repository.Interfaces
{
	public interface IRoomRepository : IRepositoryBase<Room>
	{
		IEnumerable<Room> GetAllByUserId(Guid userId);
	}
}
