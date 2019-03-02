namespace Chat.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class snapshot : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Message", "Room_Id", "dbo.Room");
            DropIndex("dbo.Message", new[] { "Room_Id" });
            DropColumn("dbo.Message", "Room_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Message", "Room_Id", c => c.Guid());
            CreateIndex("dbo.Message", "Room_Id");
            AddForeignKey("dbo.Message", "Room_Id", "dbo.Room", "Id");
        }
    }
}
