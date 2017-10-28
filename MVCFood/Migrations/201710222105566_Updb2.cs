namespace MVCFood.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updb2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UnitPrice = c.Int(nullable: false),
                        PromotionPrice = c.Int(nullable: false),
                        Image = c.String(),
                        ShelfLife = c.DateTime(nullable: false),
                        DateEntered = c.DateTime(nullable: false),
                        Quantum = c.Int(nullable: false),
                        TypeProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeProducts", t => t.TypeProductId, cascadeDelete: true)
                .Index(t => t.TypeProductId);
            
            CreateTable(
                "dbo.TypeProducts",
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
            DropForeignKey("dbo.Products", "TypeProductId", "dbo.TypeProducts");
            DropIndex("dbo.Products", new[] { "TypeProductId" });
            DropTable("dbo.TypeProducts");
            DropTable("dbo.Products");
        }
    }
}
