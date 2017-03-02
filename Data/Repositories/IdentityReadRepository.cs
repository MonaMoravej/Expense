using Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories
{
    //for languages and Currencies
    public class IdentityReadRepository<TEntity> : IIdentityReadRepository<TEntity> where TEntity : class
    {
        private IdentityDb _db;
        private DbSet<TEntity> _dbSet;

        public IdentityReadRepository(IdentityDb db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }
        public IEnumerable<TEntity> All()
        {
           return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicte)
        {
            return _dbSet.AsNoTracking().Where(predicte).ToList();
        }

        public TEntity FindById(Guid Id)
        {
            Expression<Func<TEntity,bool>> lamda = Utilities.BuildLambdaForFindByKey<TEntity>(Id);
            return _dbSet.AsNoTracking().SingleOrDefault(lamda);
        }

        public TEntity FindByName(string Name)
        {
            Expression<Func<TEntity, bool>> lamda = Utilities.BuildLambdaForFindByName<TEntity>(Name);
            return _dbSet.AsNoTracking().SingleOrDefault(lamda);

        }
    }
}
