namespace Expense.Data.DbContexts.IdentityMigrations
{
    using Entities.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Expense.Data.DbContexts.IdentityDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DbContexts\IdentityMigrations";
        }

        protected override void Seed(Expense.Data.DbContexts.IdentityDb context)
        {
            context.Languages.AddOrUpdate(
              l => l.Name,
              new Language { Name = "English",Id=Guid.NewGuid() },
              new Language { Name = "فارسی", Id = Guid.NewGuid() }
            );

            context.Currencies.AddOrUpdate(c => c.Name,
                new Currency { Name = "RM", Id = Guid.NewGuid() },
                new Currency { Name = "IRR", Id = Guid.NewGuid() },
                new Currency { Name = "EUR" },
                new Currency { Name = "USD", Id = Guid.NewGuid() });
               

        }
    }
}
