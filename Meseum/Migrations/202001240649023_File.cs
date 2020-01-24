namespace Identityproject.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class File : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Size = c.Int(),
                        path = c.String(),
                        InventoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inventories", t => t.InventoryId, cascadeDelete: true)
                .Index(t => t.InventoryId);
            
            AddColumn("dbo.Inventories", "IsPublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "InventoryId", "dbo.Inventories");
            DropIndex("dbo.Files", new[] { "InventoryId" });
            DropColumn("dbo.Inventories", "IsPublic");
            DropTable("dbo.Files");
        }
    }
}
