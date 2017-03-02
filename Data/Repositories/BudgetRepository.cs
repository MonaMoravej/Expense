using Data.Entities.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Data.DbContexts;

namespace Data.Repositories
{
    public class BudgetRepository : CUDRepository<Budget>, IExpenseReadWithUserIdRepository<Budget>
    {
        public BudgetRepository(ExpenseDb db) : base(db)
        {


        }
        public IEnumerable<Budget> All(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Budget> FindBy(Guid userId, Expression<Func<Budget, bool>> predicte)
        {
            throw new NotImplementedException();
        }

        public Budget FindById(Guid userId, Guid Id)
        {
            throw new NotImplementedException();
        }

        public Budget FindSingle(Guid userId, Expression<Func<Budget, bool>> predicte)
        {
            throw new NotImplementedException();
        }
    }
}
