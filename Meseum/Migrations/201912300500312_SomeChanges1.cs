namespace Identityproject.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeChanges1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Locations", "UpdatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "UpdatedBy");
            DropColumn("dbo.Locations", "UpdatedAt");
        }
    }
}
