namespace Expense.Data.DbContexts.IdentityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdIntoAllTables : DbMigration
    {
        public override void Up()
        {

            
            CreateIndex("Expense.Accounts", "UserId");
            AddForeignKey("Expense.Accounts", "UserId", "Identity.UsersInfo", "Id");

          
            CreateIndex("Expense.Budgets", "UserId");
            AddForeignKey("Expense.Budgets", "UserId", "Identity.UsersInfo", "Id");

         
            CreateIndex("Expense.Categories", "UserId");
            AddForeignKey("Expense.Categories", "UserId", "Identity.UsersInfo", "Id");

         
            CreateIndex("Expense.Payees", "UserId");
            AddForeignKey("Expense.Payees", "UserId", "Identity.UsersInfo", "Id");

           
            CreateIndex("Expense.Transactions", "UserId");
            AddForeignKey("Expense.Transactions", "UserId", "Identity.UsersInfo", "Id");

            
        }
        
        public override void Down()
        {
            DropForeignKey("Expense.Accounts", "UserId", "Identity.UsersInfo");
            DropIndex("Expense.Accounts", new[] { "UserId" });

            DropForeignKey("Expense.Budgets", "UserId", "Identity.UsersInfo");
            DropIndex("Expense.Budgets", new[] { "UserId" });

            DropForeignKey("Expense.Categories", "UserId", "Identity.UsersInfo");
            DropIndex("Expense.Categories", new[] { "UserId" });

            DropForeignKey("Expense.Payees", "UserId", "Identity.UsersInfo");
            DropIndex("Expense.Payees", new[] { "UserId" });

            DropForeignKey("Expense.Transactions", "UserId", "Identity.UsersInfo");
            DropIndex("Expense.Transactions", new[] { "UserId" });

        }
    }
}
