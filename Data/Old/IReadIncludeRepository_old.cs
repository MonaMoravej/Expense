using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IReadIncludeRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> AllInclude(Guid userId, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> FindByInclude(Guid userId, Expression<Func<TEntity, bool>> predicte, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity FindByIdInclude(Guid userId, Guid Id, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity FindSingleInclude(Guid userId, Expression<Func<TEntity, bool>> predicte, params Expression<Func<TEntity, object>>[] includeProperties);

    }
}
