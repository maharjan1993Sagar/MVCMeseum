namespace Identityproject.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someChagnes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ImageFiles", "AboutUs_Id", "dbo.AboutUs");
            DropForeignKey("dbo.ImageFiles", "Article_Id", "dbo.Articles");
            DropIndex("dbo.ImageFiles", new[] { "AboutUs_Id" });
            DropIndex("dbo.ImageFiles", new[] { "Article_Id" });
            AddColumn("dbo.AboutUs", "File_Id", c => c.Int());
            AddColumn("dbo.Articles", "File_Id", c => c.Int());
            CreateIndex("dbo.AboutUs", "File_Id");
            CreateIndex("dbo.Articles", "File_Id");
            AddForeignKey("dbo.AboutUs", "File_Id", "dbo.ImageFiles", "Id");
            AddForeignKey("dbo.Articles", "File_Id", "dbo.ImageFiles", "Id");
            DropColumn("dbo.ImageFiles", "AboutUs_Id");
            DropColumn("dbo.ImageFiles", "Article_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImageFiles", "Article_Id", c => c.Int());
            AddColumn("dbo.ImageFiles", "AboutUs_Id", c => c.Int());
            DropForeignKey("dbo.Articles", "File_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.AboutUs", "File_Id", "dbo.ImageFiles");
            DropIndex("dbo.Articles", new[] { "File_Id" });
            DropIndex("dbo.AboutUs", new[] { "File_Id" });
            DropColumn("dbo.Articles", "File_Id");
            DropColumn("dbo.AboutUs", "File_Id");
            CreateIndex("dbo.ImageFiles", "Article_Id");
            CreateIndex("dbo.ImageFiles", "AboutUs_Id");
            AddForeignKey("dbo.ImageFiles", "Article_Id", "dbo.Articles", "Id");
            AddForeignKey("dbo.ImageFiles", "AboutUs_Id", "dbo.AboutUs", "Id");
        }
    }
}
