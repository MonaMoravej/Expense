using Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CUDRepository<TEntity> : ICUDRepository<TEntity> where TEntity : class
    {
        protected ExpenseDb _db;
        protected DbSet<TEntity> _dbSet;
        public CUDRepository(ExpenseDb db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();

        }

        public TEntity Insert(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
                if (_db.SaveChanges() > 0)
                {
                    return entity;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex) { return null; }
            return null;
        }

        public TEntity Upadte(Guid Id, TEntity entity)
        {
            try
            {
                _dbSet.Attach(entity);//,EntityState.Modified);

                _dbSet.Update(entity);
                if (_db.SaveChanges() > 0)
                {
                    return entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) { return null; }
            return null;

        }
        public bool Delete(Guid Id)
        {
            try
            {
                var lamda = Utilities.BuildLambdaForFindByKey<TEntity>(Id);
                TEntity entity = _dbSet.Single(lamda);
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
                return _db.SaveChanges() > 0;
            }
            catch (Exception ex) { return false; }
            return false;

        }
    }
}
