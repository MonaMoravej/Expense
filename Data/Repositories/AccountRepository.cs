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
    public class AccountRepository : IAccountRepository
    {
        private ExpenseDb _db;
        private ITransactionRepository _transactionRepository;

        public AccountRepository(ExpenseDb db,ITransactionRepository transactionRepository)
        {
            _db = db;
            _transactionRepository = transactionRepository;
        }

        //for all accounts
        public IEnumerable<Account> All(Guid userId)
        {
            return (_db.Accounts.AsNoTracking().Where(a => a.UserId == userId).ToList());
        }

        //for search
        public IEnumerable<Account> FindBy(Guid userId, Expression<Func<Account, bool>> predicte)
        {
            return (_db.Accounts.AsNoTracking().Where(predicte).Where(a => a.UserId == userId).ToList());
        }

        //for one specific account
        public Account FindById(Guid userId, Guid Id)
        {
            return (_db.Accounts.AsNoTracking().SingleOrDefault(a => a.UserId == userId && a.Id == Id));
        }

        //for search just one :maybe it's not useful, could be deleted here and in interface
        public Account FindSingle(Guid userId, Expression<Func<Account, bool>> predicte)
        {
            return (_db.Accounts.AsNoTracking().Where(a => a.UserId == userId).Where(predicte).SingleOrDefault());
        }

        public Account Insert(Account entity)
        {
            //entity.AccountType = 1; //default
            //entity.Color = Color.Red; //defualt
            //entity.Name = entity.Name; 
            //entity.OpenDate = DateTime.Now;
            //entity.StartBalance = 10; // defualt =0
            //entity.UserId = userId;

            Transaction t = new Transaction()
            {
                UserId = entity.UserId,
                Date = entity.OpenDate,
                Amount = entity.StartBalance,
                AccountId = entity.Id,
                Type = TransactionType.StartBalance
            };

            _db.Transactions.Add(t);
            _db.Accounts.Add(entity);

            _db.SaveChanges();
            return entity;
        }

        public Account Upadte(Guid Id, Account entity)
        {
            Account ac = _db.Accounts.AsNoTracking().SingleOrDefault(a => a.Id == Id);
            if (ac != null)
            {
                //account updated
                entity.Id = ac.Id;
                _db.Accounts.Attach(entity);
                _db.Accounts.Update(entity);
                //StartBalance updated
                Transaction startBalance = _db.Transactions.AsNoTracking().SingleOrDefault(t => t.AccountId == entity.Id && t.Type == TransactionType.StartBalance && t.UserId == entity.UserId);
                if (startBalance != null)
                {
                    startBalance.Date = entity.OpenDate;
                    startBalance.Amount = entity.StartBalance;
                    _db.Transactions.Attach(startBalance);
                    _db.Transactions.Update(startBalance);
                }


                if (_db.SaveChanges() > 0)
                    return entity;
            }
            return null;
        }

        public bool Delete(Guid Id)
        {
            Account entity = _db.Accounts.SingleOrDefault(a => a.Id == Id);
            if (entity != null)
            {
               Transaction startBalance=  _db.Transactions.SingleOrDefault(t => t.UserId == entity.UserId && t.Type == TransactionType.StartBalance && t.AccountId == entity.Id);
                if (startBalance != null)
                {
                    _db.Transactions.Remove(startBalance);

                }
                _db.Accounts.Remove(entity);
                return _db.SaveChanges() > 0;

            }

            return false;
        }

        public Account FindByIdInclude(Guid userId, Guid Id)
        {
            var account = _db.Accounts.AsNoTracking().SingleOrDefault(a => a.UserId == userId && a.Id == Id);
            if (account != null)
            {
                account.Transactions = _transactionRepository.FindBy(userId, a => a.AccountId == account.Id || a.FromAccountId == account.Id).ToList();
                  
                return account;
            }
            return null;
        }

        //for search just one :maybe it's not useful, could be deleted here and in interface
        public Account FindSingleInclude(Guid userId, Expression<Func<Account, bool>> predicte)
        {
            var account = _db.Accounts.AsNoTracking().Where(a => a.UserId == userId).Where(predicte).SingleOrDefault();
            if (account != null)
            {
                account.Transactions = _transactionRepository.FindBy(userId, a => a.AccountId == account.Id || a.FromAccountId == account.Id).ToList();
               return account;
            }
            return null;
        }

        public bool HasTransaction(Guid Id)
        {
            var account = _db.Accounts.AsNoTracking().SingleOrDefault(a => a.Id == Id);
            if (account != null)
            {
                return _db.Transactions.Count(t => t.Type != TransactionType.StartBalance && (t.AccountId == account.Id || t.FromAccountId == account.Id || t.ToAccountId == account.Id)) > 0;
            }
            return false;
        }

        public bool IsDuplicate(Guid userId,string name, AccountType type, Guid? Id)
        {
            //check duplicate for Insert
            if (Id == null)
            {
                if(_db.Accounts.Count(a=>a.Name==name && a.Type==type && a.UserId == userId) > 0) { return true; }
            }
            //check duplicate for update
            else
            {
                if(_db.Accounts.Count(a=>a.Name == name && a.Type == type && a.UserId == userId && a.Id != Id.Value) > 0) { return true; }
            }
            return false;
        }
    }
}
