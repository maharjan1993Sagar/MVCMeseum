namespace Identityproject.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class banner : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Caption = c.String(),
                        UploadedBy = c.String(),
                        UploadedDate = c.DateTime(nullable: false),
                        Image_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageFiles", t => t.Image_Id)
                .Index(t => t.Image_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Banners", "Image_Id", "dbo.ImageFiles");
            DropIndex("dbo.Banners", new[] { "Image_Id" });
            DropTable("dbo.Banners");
        }
    }
}
