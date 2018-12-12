using System;
using Mqtt.DomainModel.Context;
using Mqtt.DomainModel.Repository.Interfaces;
using Mqtt.DomainModel.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Mqtt.DomainModel.Repository.Definitions
{
	public class RoomRepository : RepositoryBase<Room>, IRoomRepository
	{
		public RoomRepository(MqttDbContext context) : base(context)
		{
		}

		public IEnumerable<Room> GetAllByUserId(Guid userId) =>
			_context.Set<Room>().Where(x => x.CreatedById == userId);
	}
}
