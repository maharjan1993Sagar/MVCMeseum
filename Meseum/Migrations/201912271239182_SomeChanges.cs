namespace Identityproject.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Categories", "UpdatedBy", c => c.String());
            AddColumn("dbo.Inventories", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Inventories", "UpdatedBy", c => c.String());
            AddColumn("dbo.NewsEvents", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.NewsEvents", "UpdatedBy", c => c.String());
            AddColumn("dbo.Postures", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Postures", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Inventories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.NewsEvents", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.NewsEvents", "Category", c => c.String(nullable: false));
            AlterColumn("dbo.Postures", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Postures", "Name", c => c.String());
            AlterColumn("dbo.NewsEvents", "Category", c => c.String());
            AlterColumn("dbo.NewsEvents", "Title", c => c.String());
            AlterColumn("dbo.Locations", "Name", c => c.String());
            AlterColumn("dbo.Inventories", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
            DropColumn("dbo.Postures", "UpdatedBy");
            DropColumn("dbo.Postures", "UpdatedAt");
            DropColumn("dbo.NewsEvents", "UpdatedBy");
            DropColumn("dbo.NewsEvents", "UpdatedAt");
            DropColumn("dbo.Inventories", "UpdatedBy");
            DropColumn("dbo.Inventories", "UpdatedAt");
            DropColumn("dbo.Categories", "UpdatedBy");
            DropColumn("dbo.Categories", "UpdatedAt");
        }
    }
}
