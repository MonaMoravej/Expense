using Data.Entities.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories
{
   public interface IAccountRepository: IExpenseReadWithUserIdRepository<Account>,ICUDRepository<Account>
    {
        Account FindByIdInclude(Guid userId, Guid Id);

        Account FindSingleInclude(Guid userId, Expression<Func<Account, bool>> predicte);

        bool HasTransaction(Guid Id);

        bool IsDuplicate(Guid userId,string name, AccountType type, Guid? Id);
    }
}
