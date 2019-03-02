using System;
using Chat.DomainModel.Domain;
using System.Collections.Generic;

namespace Chat.DomainModel.Repository.Interfaces
{
	public interface IRoomRepository : IRepositoryBase<Room>
	{
		IEnumerable<Room> GetAllByUserId(Guid userId);
		IEnumerable<Room> GetAllAvailableByUserId(Guid userId);
	}
}
