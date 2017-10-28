namespace MVCFood.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updb6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "Note");
            DropColumn("dbo.Products", "ShelfLife");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ShelfLife", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customers", "Note", c => c.String());
        }
    }
}
