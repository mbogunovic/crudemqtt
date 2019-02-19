namespace Chat.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        RoomId = c.Guid(nullable: false),
                        SentDate = c.DateTime(nullable: false),
                        Text = c.String(nullable: false),
                        Message_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Message", t => t.Message_Id)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoomId)
                .Index(t => t.Message_Id);
            
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
                        Test = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "UserId", "dbo.User");
            DropForeignKey("dbo.Message", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Room", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Message", "Message_Id", "dbo.Message");
            DropIndex("dbo.Room", new[] { "CreatedById" });
            DropIndex("dbo.Message", new[] { "Message_Id" });
            DropIndex("dbo.Message", new[] { "RoomId" });
            DropIndex("dbo.Message", new[] { "UserId" });
            DropTable("dbo.User");
            DropTable("dbo.Room");
            DropTable("dbo.Message");
        }
    }
}
