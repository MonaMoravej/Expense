using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IRepository_old<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> All(Guid userId );

        IEnumerable<TEntity> FindBy(Guid userId, Expression<Func<TEntity, bool>> predicte);

        TEntity FindByKey(Guid userId, Guid Id);

        TEntity FindSingle(Guid userId, Expression<Func<TEntity, bool>> predicte);

        TEntity Insert(TEntity entity);

        TEntity Upadte(TEntity entity);

        bool Delete(Guid Id);


        


    }
}
