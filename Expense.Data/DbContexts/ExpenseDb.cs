
using Expense.Data.Entities.Identity;
using Expense.Entities.Expense;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Expense.Data.DbContexts
{
    public class ExpenseDb:DbContext 
    {
        public ExpenseDb() : base("DefaultConnection")
        {
            Database.SetInitializer<ExpenseDb>(null); // Remove default initializer
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Expense");
            base.OnModelCreating(modelBuilder);

           //configuration for decimal type or maybe dateTime as well

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Payee> Payees { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
      


    }
}