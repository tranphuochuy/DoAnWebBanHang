namespace MVCFood.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updb7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "CustomerId", "dbo.Customers");
            DropIndex("dbo.OrderDetails", new[] { "CustomerId" });
            AddColumn("dbo.OrderDetails", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "OrderId");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            DropColumn("dbo.OrderDetails", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "CustomerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropColumn("dbo.OrderDetails", "OrderId");
            CreateIndex("dbo.OrderDetails", "CustomerId");
            AddForeignKey("dbo.OrderDetails", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
