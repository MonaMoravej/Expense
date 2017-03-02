namespace Expense.Data.DbContexts.ExpenseMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Expense.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        StartBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OpenDate = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                        AccountType = c.Int(nullable: false),
                        Color = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Expense.Transactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        PayeeId = c.Guid(),
                        AccountId = c.Guid(),
                        CategoryId = c.Guid(),
                        ToAccountId = c.Guid(),
                        FromAccountId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Expense.Payees", t => t.PayeeId)
                .ForeignKey("Expense.Categories", t => t.CategoryId)
                .ForeignKey("Expense.Accounts", t => t.FromAccountId)
                .ForeignKey("Expense.Accounts", t => t.ToAccountId)
                .ForeignKey("Expense.Accounts", t => t.AccountId)
                .Index(t => t.PayeeId)
                .Index(t => t.AccountId)
                .Index(t => t.CategoryId)
                .Index(t => t.ToAccountId)
                .Index(t => t.FromAccountId);
            
            CreateTable(
                "Expense.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Type = c.Int(nullable: false),
                        UserId = c.Guid(),
                        ParentId = c.Guid(),
                        IconId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Expense.Categories", t => t.ParentId)
                .ForeignKey("Expense.Icons", t => t.IconId)
                .Index(t => t.ParentId)
                .Index(t => t.IconId);
            
            CreateTable(
                "Expense.Budgets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YearMonth = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Expense.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "Expense.Icons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        name = c.String(nullable: false, maxLength: 100),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Expense.Payees",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        name = c.String(nullable: false, maxLength: 100),
                        Memo = c.String(),
                        UserId = c.Guid(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Expense.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Expense.Transactions", "AccountId", "Expense.Accounts");
            DropForeignKey("Expense.Transactions", "ToAccountId", "Expense.Accounts");
            DropForeignKey("Expense.Transactions", "FromAccountId", "Expense.Accounts");
            DropForeignKey("Expense.Transactions", "CategoryId", "Expense.Categories");
            DropForeignKey("Expense.Transactions", "PayeeId", "Expense.Payees");
            DropForeignKey("Expense.Payees", "CategoryId", "Expense.Categories");
            DropForeignKey("Expense.Categories", "IconId", "Expense.Icons");
            DropForeignKey("Expense.Categories", "ParentId", "Expense.Categories");
            DropForeignKey("Expense.Budgets", "CategoryId", "Expense.Categories");
            DropIndex("Expense.Payees", new[] { "CategoryId" });
            DropIndex("Expense.Budgets", new[] { "CategoryId" });
            DropIndex("Expense.Categories", new[] { "IconId" });
            DropIndex("Expense.Categories", new[] { "ParentId" });
            DropIndex("Expense.Transactions", new[] { "FromAccountId" });
            DropIndex("Expense.Transactions", new[] { "ToAccountId" });
            DropIndex("Expense.Transactions", new[] { "CategoryId" });
            DropIndex("Expense.Transactions", new[] { "AccountId" });
            DropIndex("Expense.Transactions", new[] { "PayeeId" });
            DropTable("Expense.Payees");
            DropTable("Expense.Icons");
            DropTable("Expense.Budgets");
            DropTable("Expense.Categories");
            DropTable("Expense.Transactions");
            DropTable("Expense.Accounts");
        }
    }
}
