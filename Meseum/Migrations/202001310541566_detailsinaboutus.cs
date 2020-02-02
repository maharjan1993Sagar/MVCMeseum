namespace Identityproject.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class detailsinaboutus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AboutUs", "Details", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AboutUs", "Details");
        }
    }
}
