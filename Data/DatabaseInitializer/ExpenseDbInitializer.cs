using Data.DbContexts;
using Data.Entities.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.DatabaseInitializer
{
    public static class ExpenseDbInitializer
    {

        public static void Seed(this ExpenseDb db)
        {
            db.AddCategories();
            db.AddIcons();
            
        }


        private static void AddCategories(this ExpenseDb db)
        {
            if (!db.Categories.Any())
            {
                var Auto = new Category() { Name = "Auto", Type = CategoryType.Expense };
                db.Categories.AddRange(new Category() { Name = "Salary", Type = CategoryType.Income },
                   Auto,
                    new Category() { Name = "Gas", Type = CategoryType.Expense, ParentId = Auto.Id });
                db.SaveChanges();
            }
        }


        private static void AddIcons(this ExpenseDb db)
        {
            if (!db.Icons.Any())
            {
                db.Icons.Add(new Icon() { Name = "Home" });
                db.SaveChanges();
            }
        }
    }



}
