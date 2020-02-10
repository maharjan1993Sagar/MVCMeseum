namespace Identityproject.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filesToNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageFiles", "NewsEvent_Id", c => c.Int());
            CreateIndex("dbo.ImageFiles", "NewsEvent_Id");
            AddForeignKey("dbo.ImageFiles", "NewsEvent_Id", "dbo.NewsEvents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageFiles", "NewsEvent_Id", "dbo.NewsEvents");
            DropIndex("dbo.ImageFiles", new[] { "NewsEvent_Id" });
            DropColumn("dbo.ImageFiles", "NewsEvent_Id");
        }
    }
}
