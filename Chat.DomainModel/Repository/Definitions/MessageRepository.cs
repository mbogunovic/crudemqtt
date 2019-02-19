using Chat.DomainModel.Context;
using Chat.DomainModel.Repository.Interfaces;
using Chat.DomainModel.Domain;

namespace Chat.DomainModel.Repository.Definitions
{
	public class MessageRepository : RepositoryBase<Message>, IMessageRepository
	{
		public MessageRepository(ChatDbContext context) : base(context)
		{
		}
	}
}
