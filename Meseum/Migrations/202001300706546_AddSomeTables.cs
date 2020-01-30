namespace Identityproject.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSomeTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MenuName = c.String(),
                        UploadedDate = c.DateTime(nullable: false),
                        UploadedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImageFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Size = c.Int(),
                        path = c.String(),
                        UploadedDate = c.DateTime(nullable: false),
                        UploadedBy = c.String(),
                        AboutUs_Id = c.Int(),
                        Article_Id = c.Int(),
                        Events_Id = c.Int(),
                        Gallery_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AboutUs", t => t.AboutUs_Id)
                .ForeignKey("dbo.Articles", t => t.Article_Id)
                .ForeignKey("dbo.Events", t => t.Events_Id)
                .ForeignKey("dbo.Galleries", t => t.Gallery_Id)
                .Index(t => t.AboutUs_Id)
                .Index(t => t.Article_Id)
                .Index(t => t.Events_Id)
                .Index(t => t.Gallery_Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        UploadedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Organizer = c.String(),
                        Location = c.String(),
                        Description = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        PostedDate = c.DateTime(nullable: false),
                        UploadedBy = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortDetails = c.String(),
                        UploadedBy = c.String(),
                        UploadedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LinkUrl = c.String(),
                        UploadedDate = c.DateTime(nullable: false),
                        UploadedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Queries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Title = c.String(),
                        Message = c.String(),
                        UploadedDate = c.DateTime(nullable: false),
                        UploadedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageFiles", "Gallery_Id", "dbo.Galleries");
            DropForeignKey("dbo.ImageFiles", "Events_Id", "dbo.Events");
            DropForeignKey("dbo.ImageFiles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.ImageFiles", "AboutUs_Id", "dbo.AboutUs");
            DropIndex("dbo.ImageFiles", new[] { "Gallery_Id" });
            DropIndex("dbo.ImageFiles", new[] { "Events_Id" });
            DropIndex("dbo.ImageFiles", new[] { "Article_Id" });
            DropIndex("dbo.ImageFiles", new[] { "AboutUs_Id" });
            DropTable("dbo.Queries");
            DropTable("dbo.Links");
            DropTable("dbo.Galleries");
            DropTable("dbo.Events");
            DropTable("dbo.Articles");
            DropTable("dbo.ImageFiles");
            DropTable("dbo.AboutUs");
        }
    }
}
