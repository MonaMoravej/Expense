
using Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Data.Repositories
{
    public class GenericRepository_old<TEntity> : IGenericRepository_old<TEntity> where TEntity : class
    {
        internal ExpenseDb _db;
        internal DbSet<TEntity> _dbSet;

        //Dependency injection, DbContext is injected
        public GenericRepository_old(ExpenseDb db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
            //_db = new ExpenseDb(); without DependencyInjection
        }

        public IEnumerable<TEntity> All()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList<TEntity>();
        }

        public TEntity FindByKey(Guid Id)
        {
            var lambda = Utilities.BuildLambdaForFindByKey<TEntity>(Id);
            return _dbSet.AsNoTracking().SingleOrDefault(lambda);
        }

        private IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = _dbSet.AsNoTracking();
            return includeProperties.Aggregate(
                queryable, (current, includeProperty) => current.Include(includeProperty)
                );

        }

        public IEnumerable<TEntity> AllInclue(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).ToList();
        }

        public IEnumerable<TEntity> FindByInclude(Expression<Func<TEntity, bool>> predicate,params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            IEnumerable<TEntity> result = query.Where(predicate).ToList();
            return result; 
        }

        public TEntity FindByKeyInclude(Guid Id,params Expression<Func<TEntity,object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            var lambda = Utilities.BuildLambdaForFindByKey<TEntity>(Id);
            return query.SingleOrDefault(lambda);
        }



        public TEntity Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            if (SaveChanges())
            {
                return entity;
            }
            else
            {
                return null;
            }
        }

        public TEntity Update(TEntity entity)
        {
            _dbSet.Attach(entity);//,EntityState.Modified);
           
            _dbSet.Update(entity);
            if (SaveChanges())
            {
                return entity;
            }
            else
            {
                return null;
            }

        }

        public bool Delete(Guid Id)
        {
            TEntity entity = FindByKey(Id);
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            return SaveChanges();
        }

        private bool SaveChanges()
        {
            return (_db.SaveChanges() > 0);
        }
    }
}
