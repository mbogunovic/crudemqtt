using System;
using Chat.DomainModel.Context;
using Chat.DomainModel.Repository.Interfaces;
using Chat.DomainModel.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Chat.DomainModel.Repository.Definitions
{
	public class RoomRepository : RepositoryBase<Room>, IRoomRepository
	{
		public RoomRepository(ChatDbContext context) : base(context)
		{
		}

		public IEnumerable<Room> GetAllByUserId(Guid userId) =>
			_context.Set<Room>().Where(x => x.CreatedById == userId);
	}
}
