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
		private readonly RoomUserRepository _roomUserRepository;

		public RoomRepository(ChatDbContext context) : base(context)
		{
			this._roomUserRepository = new RoomUserRepository(context);
		}

		public IEnumerable<Room> GetAllByUserId(Guid userId) =>
			_context.Set<Room>().Where(x => x.CreatedById == userId);

		public override Room Add(Room item)
		{
			Room r = base.Add(item);

			RoomUser ru = new RoomUser()
			{
				RoomId = r.Id,
				UserId = r.CreatedById,
				IsActive = true,
			};

			_roomUserRepository.Add(ru);

			return r; 
		}
	}
}
