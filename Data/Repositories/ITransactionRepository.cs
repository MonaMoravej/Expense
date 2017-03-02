using Data.Entities.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface ITransactionRepository: IExpenseReadWithUserIdRepository<Transaction>, ICUDRepository<Transaction>
    {
    }
}
