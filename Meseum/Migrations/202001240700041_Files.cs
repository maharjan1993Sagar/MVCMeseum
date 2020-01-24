namespace Identityproject.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Files : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "UploadedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Files", "UploadedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "UploadedBy");
            DropColumn("dbo.Files", "UploadedDate");
        }
    }
}
