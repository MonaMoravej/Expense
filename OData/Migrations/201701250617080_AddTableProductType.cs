namespace OData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableProductType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "OData.ProductTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("OData.Products", "ProductTypeId", c => c.Int(nullable: false));
            CreateIndex("OData.Products", "ProductTypeId");
            AddForeignKey("OData.Products", "ProductTypeId", "OData.ProductTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("OData.Products", "ProductTypeId", "OData.ProductTypes");
            DropIndex("OData.Products", new[] { "ProductTypeId" });
            DropColumn("OData.Products", "ProductTypeId");
            DropTable("OData.ProductTypes");
        }
    }
}
