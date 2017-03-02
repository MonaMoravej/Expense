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
    public class AccountRepository_old : IRepository_old<Account>
    {
        private ExpenseDb _db;
       
        public AccountRepository_old(ExpenseDb db)
        {
            _db = db;
        }
        private IEnumerable<Account> All()
        {
          return  _db.Accounts.ToList();
        }

        private IQueryable<Account> GetAllIncluding(params Expression<Func<Account,object>>[] includeProperties)
        {
            DbSet<Account> set = _db.Set<Account>();
            IQueryable<Account> queryable = set.AsNoTracking<Account>();

            return includeProperties.Aggregate(
               queryable, (current, includeProperty) => current.Include(includeProperty)
               );
        }

        private IEnumerable<Account> AllInclue(params Expression<Func<Account, object>>[] includeProperties)
        {

            return GetAllIncluding(includeProperties).ToList();
          
        }


        private bool Delete(Guid Id)
        {
            var tr = _db.Transactions.FirstOrDefault(t => t.AccountId == Id && t.Type == TransactionType.StartBalance);
            if (tr != null) _db.Transactions.Remove(tr);

            var ac = _db.Accounts.FirstOrDefault(a => a.Id == Id);
            if (ac != null) _db.Accounts.Remove(ac);

            return _db.SaveChanges() > 0;

        }

        private IEnumerable<Account> FindBy(Expression<Func<Account, bool>> predicate)
        {
            _db.Accounts.AsNoTracking().FirstOrDefault();
            return (_db.Accounts.Where(predicate).ToList());
        }

        private IEnumerable<Account> FindByInclude(Expression<Func<Account, bool>> predicate, params Expression<Func<Account, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            return query.Where(predicate).ToList();
        }

        private Account FindByKey(Guid Id)
        {
            return _db.Accounts.FirstOrDefault(a => a.Id == Id);
        }

        public Account FindByKeyInclude(Guid Id, params Expression<Func<Account, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).FirstOrDefault(a => a.Id == Id);
        }

        private Account Insert(Account entity)
        {
            //entity.AccountType = 1; //default
            //entity.Color = Color.Red; //defualt
            //entity.Name = entity.Name; 
            //entity.OpenDate = DateTime.Now;
            //entity.StartBalance = 10; // defualt =0
            //entity.UserId = "jhbjhbjhbjjbj"; //login user

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

        private Account Update(Account entity)
        {
            var tr = _db.Transactions.FirstOrDefault(t => t.AccountId == entity.Id && t.Type == TransactionType.StartBalance);
            if (tr != null)
            {
                tr.Date = entity.OpenDate;
                tr.Amount = entity.StartBalance;
            }

            _db.Accounts.Attach(entity);
            _db.Accounts.Update(entity);
            _db.SaveChanges();
            return entity;
        }




        public IEnumerable<Account> All(Guid userId)
        {
            return _db.Accounts.AsNoTracking().Where(a => a.UserId == userId).ToList();
        }

        public IEnumerable<Account> FindBy(Guid userId, Expression<Func<Account, bool>> predicte)
        {
           var results =  _db.Accounts.AsNoTracking().Where(a => a.UserId == userId).Where(predicte).ToList();
            return (results);
        }

        public Account FindByKey(Guid userId, Guid Id)
        {
            return _db.Accounts.AsNoTracking().SingleOrDefault(a => a.Id == Id && a.UserId==userId);
        }

        public Account FindSingle(Guid userId, Expression<Func<Account, bool>> predicte)
        {
           
            var results= _db.Accounts.AsNoTracking().Where(a=>a.UserId==userId).SingleOrDefault(predicte);
            return results;
        }

       

        public Account Upadte(Account entity)
        {
            throw new NotImplementedException();
        }

        

        Account IRepository_old<Account>.Insert(Account entity)
        {
            throw new NotImplementedException();
        }

        bool IRepository_old<Account>.Delete(Guid Id)
        {
            throw new NotImplementedException();
        }


        //not used
        //public IEnumerable<Account> AllInclude(Guid userId, params Expression<Func<Account, object>>[] includeProperties)
        //{
        //    try
        //    {
        //        IQueryable<Account> query = _db.Accounts.AsNoTracking().Where(a => a.UserId == userId);
        //        //  if (includeProperties.Length > 0) { }
        //        //  a=>a.User

        //        var resultQ = includeProperties.Aggregate(
        //            query, (current, includeProperty) => current.Include(includeProperty)
        //            );
        //        var results = resultQ.ToList();
        //        return (results);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}


        //not used
        public Account FindByIdInclude(Guid userId, Guid Id, params Expression<Func<Account, object>>[] includeProperties)
        {
            IQueryable<Account> query = _db.Accounts.AsNoTracking().Where(a => a.Id == Id && a.UserId == userId);
            //  if (includeProperties.Length > 0) { }
            //  a=>a.User

            var resultQ = includeProperties.Aggregate(
                query, (current, includeProperty) => current.Include(includeProperty)
                );
            var account = resultQ.SingleOrDefault();
            return (account);

        }


    }
}
