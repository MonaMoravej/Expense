using Expense.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Expense.Data.Entities.Identity;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense.Data.DbContexts
{
   

    public class IdentityDb : IdentityDbContext<ApplicationUser, CustomRole,Guid, CustomUserLogin, CustomUserRole, CustomUserClaim>
    
    {
        public IdentityDb()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Identity");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("UsersInfo");
            modelBuilder.Entity<CustomUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<CustomRole>().ToTable("Roles");



        }
        public DbSet<Language> Languages { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public static IdentityDb Create()
        {
            return new IdentityDb();
        }
    }
}