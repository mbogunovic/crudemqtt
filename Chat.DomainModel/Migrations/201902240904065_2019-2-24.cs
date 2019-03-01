namespace Chat.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2019224 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Test", c => c.String(nullable: false));
        }
    }
}
