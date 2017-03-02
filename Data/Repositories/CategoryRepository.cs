using Data.Entities.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{

    //update and delete and Insert of category and payee are the same so we can have a abstract class and implement there with generic entity and then inherite from them
    public class CategoryRepository : CUDRepository<Category>, IExpenseReadWithUserIdRepository<Category>
    {
        public CategoryRepository(ExpenseDb db) : base(db)
        {


        }
        public IEnumerable<Category> All(Guid userId)
        {

            return (_db.Categories.AsNoTracking().Where(a => a.UserId == userId || a.UserId == null).ToList());
        }


        public IEnumerable<Category> FindBy(Guid userId, Expression<Func<Category, bool>> predicte)
        {
            return (_db.Categories.AsNoTracking().Where(a => a.UserId == userId || a.UserId == null).Where(predicte).ToList());
        }

        public Category FindById(Guid userId, Guid Id)
        {
            return (_db.Categories.AsNoTracking().SingleOrDefault(a => a.Id == Id && (a.UserId == userId || a.UserId == null)));
        }

        public Category FindSingle(Guid userId, Expression<Func<Category, bool>> predicte)
        {
            return (_db.Categories.AsNoTracking().Where(a => a.UserId == userId || a.UserId == null).Where(predicte).SingleOrDefault());
        }


    }
}
