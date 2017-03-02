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
    public class PayeeRepository : CUDRepository<Payee>, IExpenseReadWithUserIdRepository<Payee>
    {
      
        public PayeeRepository(ExpenseDb db):base(db)
        {
           

        }
        public IEnumerable<Payee> All(Guid userId)
        {
            return (_db.Payees.AsNoTracking().Where(a => a.UserId == userId ).ToList());
        }

        public IEnumerable<Payee> FindBy(Guid userId, Expression<Func<Payee, bool>> predicte)
        {
            return (_db.Payees.AsNoTracking().Where(a => a.UserId == userId).Where(predicte).ToList());

        }

        public Payee FindById(Guid userId, Guid Id)
        {
            return (_db.Payees.AsNoTracking().SingleOrDefault(a => a.Id == Id && a.UserId == userId ));

        }

        public Payee FindSingle(Guid userId, Expression<Func<Payee, bool>> predicte)
        {
            return (_db.Payees.AsNoTracking().Where(a => a.UserId == userId).Where(predicte).SingleOrDefault());

        }

       
    }
}
