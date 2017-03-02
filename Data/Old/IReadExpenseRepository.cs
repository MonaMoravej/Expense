using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories
{
   public interface IReadExpenseRepository<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> All(Guid userId);
        IEnumerable<TEntity> FindBy(Guid userId, Expression<Func<TEntity, bool>> predicte);
        TEntity FindById(Guid userId, Guid Id);
        TEntity FindSingle(Guid userId, Expression<Func<TEntity, bool>> predicte);
    }
}
