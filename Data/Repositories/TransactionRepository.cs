using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entities.Expense;
using Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private ExpenseDb _db;

        public TransactionRepository(ExpenseDb db)
        {
            _db = db;
        }

        private IQueryable<Transaction> QueryAll(Guid userId)
        {
            var query= _db.Transactions.Where(t=>t.UserId==userId)
                .Include(t => t.Account)
                .Include(t => t.ToAccount)
                .Include(t => t.FromAccount)
                .Include(t => t.Category)
                .Include(t => t.Payee);
            return query;
        }
        public IEnumerable<Transaction> All(Guid userId)
        {
            
          return QueryAll(userId).AsNoTracking().ToList();
        }

        public IEnumerable<Transaction> FindBy(Guid userId, Expression<Func<Transaction, bool>> predicte)
        {
            return QueryAll(userId).Where(predicte).AsNoTracking().ToList();
           
        }

        public Transaction FindById(Guid userId, Guid Id)
        {
            return (QueryAll(userId).AsNoTracking().SingleOrDefault(a => a.UserId == userId && a.Id==Id));
        }

        public Transaction FindSingle(Guid userId, Expression<Func<Transaction, bool>> predicte)
        {
            return (QueryAll(userId).AsNoTracking().Where(a => a.UserId == userId).Where(predicte).SingleOrDefault());
        }

        public Transaction Insert(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public Transaction Upadte(Guid Id, Transaction entity)
        {
            throw new NotImplementedException();
        }
        public bool Delete(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
