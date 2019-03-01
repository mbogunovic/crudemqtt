namespace Chat.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Message", "Message_Id", "dbo.Message");
            DropForeignKey("dbo.Message", "UserId", "dbo.User");
            DropIndex("dbo.Message", new[] { "UserId" });
            DropIndex("dbo.Message", new[] { "RoomId" });
            DropIndex("dbo.Message", new[] { "Message_Id" });
            RenameColumn(table: "dbo.Message", name: "RoomId", newName: "Room_Id");
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
            
            AddColumn("dbo.Message", "RoomUserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Message", "Room_Id", c => c.Guid());
            CreateIndex("dbo.Message", "RoomUserId");
            CreateIndex("dbo.Message", "Room_Id");
            AddForeignKey("dbo.Message", "RoomUserId", "dbo.RoomUser", "Id");
            DropColumn("dbo.Message", "UserId");
            DropColumn("dbo.Message", "Message_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Message", "Message_Id", c => c.Guid());
            AddColumn("dbo.Message", "UserId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Message", "RoomUserId", "dbo.RoomUser");
            DropForeignKey("dbo.RoomUser", "UserId", "dbo.User");
            DropForeignKey("dbo.RoomUser", "RoomId", "dbo.Room");
            DropIndex("dbo.RoomUser", new[] { "RoomId" });
            DropIndex("dbo.RoomUser", new[] { "UserId" });
            DropIndex("dbo.Message", new[] { "Room_Id" });
            DropIndex("dbo.Message", new[] { "RoomUserId" });
            AlterColumn("dbo.Message", "Room_Id", c => c.Guid(nullable: false));
            DropColumn("dbo.Message", "RoomUserId");
            DropTable("dbo.RoomUser");
            RenameColumn(table: "dbo.Message", name: "Room_Id", newName: "RoomId");
            CreateIndex("dbo.Message", "Message_Id");
            CreateIndex("dbo.Message", "RoomId");
            CreateIndex("dbo.Message", "UserId");
            AddForeignKey("dbo.Message", "UserId", "dbo.User", "Id");
            AddForeignKey("dbo.Message", "Message_Id", "dbo.Message", "Id");
        }
    }
}
