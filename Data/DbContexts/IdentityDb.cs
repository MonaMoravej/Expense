using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


using Data.Entities.Identity;

using System;
using Data.Entities.Expense;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.DbContexts
{


    public class IdentityDb : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {


        private IConfigurationRoot _config;

        public IdentityDb(DbContextOptions<IdentityDb> options, IConfigurationRoot config)
            : base(options)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Identity");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity("Data.Entities.Identity.User", u =>
            {
                u.ToTable("User");
                u.HasKey("Id");
               

            }
            );
           


            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins").HasKey(l => new { l.ProviderKey, l.LoginProvider });
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims").HasKey(c => c.Id);
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken").HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles").HasKey(r => r.Id);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims").HasKey(rc => rc.Id);


            modelBuilder.Ignore(typeof(Icon));
            modelBuilder.Ignore(typeof(Account));
            modelBuilder.Ignore(typeof(Budget));
            modelBuilder.Ignore(typeof(Category));
            modelBuilder.Ignore(typeof(Payee));
            modelBuilder.Ignore(typeof(Transaction));
           
        }


        public DbSet<Language> Languages { get; set; }

        public DbSet<Currency> Currencies { get; set; }

      


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["Data:ConnectionString"]);
        }
    }
}