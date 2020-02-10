namespace Identityproject.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class locCat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "LocationId", c => c.Int());
            CreateIndex("dbo.Categories", "LocationId");
            AddForeignKey("dbo.Categories", "LocationId", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "LocationId", "dbo.Locations");
            DropIndex("dbo.Categories", new[] { "LocationId" });
            DropColumn("dbo.Categories", "LocationId");
        }
    }
}
