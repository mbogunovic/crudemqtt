namespace Chat.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class snapshot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoomUserId = c.Guid(nullable: false),
                        Text = c.String(nullable: false),
                        SentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomUser", t => t.RoomUserId)
                .Index(t => t.RoomUserId);
            
            CreateTable(
                "dbo.RoomUser",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        RoomId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsChatting = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisplayName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "RoomUserId", "dbo.RoomUser");
            DropForeignKey("dbo.RoomUser", "UserId", "dbo.User");
            DropForeignKey("dbo.RoomUser", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Room", "CreatedById", "dbo.User");
            DropIndex("dbo.Room", new[] { "CreatedById" });
            DropIndex("dbo.RoomUser", new[] { "RoomId" });
            DropIndex("dbo.RoomUser", new[] { "UserId" });
            DropIndex("dbo.Message", new[] { "RoomUserId" });
            DropTable("dbo.User");
            DropTable("dbo.Room");
            DropTable("dbo.RoomUser");
            DropTable("dbo.Message");
        }
    }
}
