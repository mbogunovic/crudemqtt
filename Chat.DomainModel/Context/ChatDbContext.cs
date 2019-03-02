using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Chat.DomainModel.Domain;
using Chat.DomainModel.Repository.Definitions;
using Chat.DomainModel.Repository.Interfaces;

namespace Chat.DomainModel.Context
{
	public class ChatDbContext : DbContext
	{
		public ChatDbContext() : base("name=domainDb")
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChatDbContext, Migrations.Configuration>());
		}

		public IUserRepository UsersRepository => new UserRepository(this);
		public IRoomRepository RoomsRepository => new RoomRepository(this);
		public IMessageRepository MessagesRepository => new MessageRepository(this);
		public IRoomUserRepository RoomUsersRepository => new RoomUserRepository(this);

		private DbSet<User> Users { get; set; }
		private DbSet<Room> Rooms { get; set; }
		private DbSet<Message> Messages { get; set; }
		private DbSet<RoomUser> RoomUsers { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
		}
	}
}
