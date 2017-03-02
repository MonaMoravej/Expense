
using Data.Entities.Identity;
using Data.Entities.Expense;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Data.DbContexts
{
    public class ExpenseDb: DbContext 
    {
        private IConfigurationRoot _config;
        public ExpenseDb(DbContextOptions<ExpenseDb> options, IConfigurationRoot config) : base(options)
        {
            _config = config;
         

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            
            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<Budget>(a =>
            //{
            //    a.HasOne("Data.Entities.Identity.User")
            //    .WithMany("Budgets")
            //    .HasForeignKey("UserId")
            //    .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
            //});

            //modelBuilder.Entity<Category>(a =>
            //{
            //    a.HasOne("Data.Entities.Identity.User")
            //    .WithMany("Categories")
            //    .HasForeignKey("UserId")
            //    .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
            //});

            //modelBuilder.Entity<Payee>(a =>
            //{
            //    a.HasOne("Data.Entities.Identity.User")
            //    .WithMany("Payees")
            //    .HasForeignKey("UserId")
            //    .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
            //});

            //modelBuilder.Entity<Transaction>(a =>
            //{
            //    a.HasOne("Data.Entities.Identity.User")
            //    .WithMany("Transactions")
            //    .HasForeignKey("UserId")
            //    .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
            //});

            // modelBuilder.Ignore(typeof(User));
            modelBuilder.Ignore(typeof(IdentityRole<Guid>));
            modelBuilder.Ignore(typeof(IdentityRoleClaim<Guid>));
            modelBuilder.Ignore(typeof(IdentityUserClaim<Guid>));
            modelBuilder.Ignore(typeof(IdentityUserLogin<Guid>));
            modelBuilder.Ignore(typeof(IdentityUserRole<Guid>));
            modelBuilder.Ignore(typeof(IdentityUserToken<Guid>));

            modelBuilder.Ignore(typeof(Language));
            modelBuilder.Ignore(typeof(Currency));


            //configuration for decimal type or maybe dateTime as well

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Payee> Payees { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["Data:ConnectionString"]);
        }

       
    }
}