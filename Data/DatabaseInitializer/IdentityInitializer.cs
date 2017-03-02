using Data.DbContexts;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Data.DatabaseInitializer
{
    public static class IdentityInitializer
    {
       
        public static void Seed(this IdentityDb db)
        {
            db.AddLanguages();
            db.AddCurrencies();
           
        }


        private static void AddLanguages(this IdentityDb db)
        {
            if (!db.Languages.Any())
            {
                db.Languages.AddRange(new Language() { Name = "English" },
                    new Language() { Name = "فارسی" },
                    new Language() { Name = "Malay" });
                db.SaveChanges();
            }
        }
        private static void AddCurrencies(this IdentityDb db)
        {
            if (!db.Currencies.Any())
            {
                db.Currencies.AddRange(new Currency() { Name = "RM" },
                    new Currency() { Name = "IRR" }
                    );
                db.SaveChanges();
            }
        }

       
    }

   
}
