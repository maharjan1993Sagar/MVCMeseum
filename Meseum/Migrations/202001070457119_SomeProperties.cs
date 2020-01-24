namespace Identityproject.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventories", "DetailStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Inventories", "PhotoStatus", c => c.Boolean(nullable: false));
            DropColumn("dbo.Inventories", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inventories", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.Inventories", "PhotoStatus");
            DropColumn("dbo.Inventories", "DetailStatus");
        }
    }
}
