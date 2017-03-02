using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
   public  interface ICUDRepository<TEntity> where TEntity :class
    {

        TEntity Insert(TEntity entity);

        TEntity Upadte(Guid Id,TEntity entity);

        bool Delete(Guid Id);

       
    }
}
