using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IGenericRepository_old<TEntity>where TEntity:class
    {
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        TEntity FindByKey(Guid Id);
        IEnumerable<TEntity> AllInclue(params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> FindByInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity FindByKeyInclude(
            Guid Id, 
            params Expression<Func<TEntity, object>>[] includeProperties
            );
   
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(Guid Id);

    }
}
