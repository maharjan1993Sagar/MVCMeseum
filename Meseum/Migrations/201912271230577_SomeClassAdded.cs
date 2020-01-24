namespace Identityproject.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeClassAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortDetail = c.String(),
                        LongDetail = c.String(),
                        ShortName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Material = c.String(),
                        ObjectCode = c.String(),
                        size = c.String(),
                        OriginOf = c.String(),
                        MadeBy = c.String(),
                        ShortDetail = c.String(),
                        LongDetail = c.String(),
                        Status = c.Boolean(nullable: false),
                        Long = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Latit = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortDetail = c.String(),
                        LongDetail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Category = c.String(),
                        Description = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Postures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inventories", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Inventories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Inventories", new[] { "LocationId" });
            DropIndex("dbo.Inventories", new[] { "CategoryId" });
            DropTable("dbo.Postures");
            DropTable("dbo.NewsEvents");
            DropTable("dbo.Locations");
            DropTable("dbo.Inventories");
            DropTable("dbo.Categories");
        }
    }
}
