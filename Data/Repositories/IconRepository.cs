using Data.DbContexts;
using Data.Entities.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class IconRepository : IExpenseReadWithoutUserIdRepository<Icon>
    {
        private ExpenseDb _db;
        public IconRepository(ExpenseDb db)
        {
            _db = db;
        }
        public IEnumerable<Icon> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Icon> FindBy(System.Linq.Expressions.Expression<Func<Icon, bool>> predicte)
        {
            throw new NotImplementedException();
        }

        public Icon FindById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Icon FindSingle(System.Linq.Expressions.Expression<Func<Icon, bool>> predicte)
        {
            throw new NotImplementedException();
        }
    }
}
