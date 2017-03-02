using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories
{
   public interface IIdentityReadRepository<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicte);
        TEntity FindById(Guid Id);

        TEntity FindByName(string Name);
       
    }
}
