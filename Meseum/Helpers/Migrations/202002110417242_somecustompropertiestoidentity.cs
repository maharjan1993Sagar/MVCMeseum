namespace Meseum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somecustompropertiestoidentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Role", c => c.String());
            AddColumn("dbo.AspNetUsers", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Status");
            DropColumn("dbo.AspNetUsers", "Role");
        }
    }
}
