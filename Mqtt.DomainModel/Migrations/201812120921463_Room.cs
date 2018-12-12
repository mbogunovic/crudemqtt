namespace Mqtt.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Room : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .Index(t => t.CreatedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Room", "CreatedById", "dbo.User");
            DropIndex("dbo.Room", new[] { "CreatedById" });
            DropTable("dbo.Room");
        }
    }
}
