using Chat.DomainModel.Context;
using Chat.DomainModel.Repository.Interfaces;
using Chat.DomainModel.Domain;

namespace Chat.DomainModel.Repository.Definitions
{
	public class RoomUserRepository : RepositoryBase<RoomUser>, IRoomUserRepository
	{
		public RoomUserRepository(ChatDbContext context) : base(context)
		{
		}
	}
}
