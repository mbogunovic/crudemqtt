using Chat.DomainModel.Context;
using Chat.DomainModel.Repository.Interfaces;
using Chat.DomainModel.Domain;

namespace Chat.DomainModel.Repository.Definitions
{
	public class UserRepository : RepositoryBase<User>, IUserRepository
	{
		public UserRepository(ChatDbContext context) : base(context)
		{
		}
	}
}
