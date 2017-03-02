using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data
{
    public static class Utilities
    {
        /// <summary>
        ///             Lambda
        ///              /  \
        ///          Equal Parameter
        ///           /   \    entity
        ///      Property  \
        ///       "Id"   Constant
        ///         |       Id(Guid parameter)
        ///     Parameter
        ///       entity
        /// entity => entity.Id == Id
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        ///           
        public static Expression<Func<TEntity, bool>> BuildLambdaForFindByKey<TEntity>(Guid Id)
        {
            var entity = Expression.Parameter(typeof(TEntity), "entity");

            var prop = Expression.Property(entity, "Id");

            var IdValue = Expression.Constant(Id); //Id :Guid

            var equal = Expression.Equal(prop, IdValue);

            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, entity);

            return lambda;
        }


        public static Expression<Func<TEntity, bool>> BuildLambdaForFindByName<TEntity>(string Name)
        {
            var entity = Expression.Parameter(typeof(TEntity), "entity");

            var prop = Expression.Property(entity, "Name");

            var NameValue = Expression.Constant(Name); //Name :string

            var equal = Expression.Equal(prop, NameValue);

            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, entity);

            return lambda;
        }
        //public static Expression<Func<TEntity, bool>> AndAlso<TEntity>(
        //     this Expression<Func<TEntity, bool>> left,
        //     Expression<Func<TEntity, bool>> right)
        //{
        //    var param = Expression.Parameter(typeof(TEntity), "x");
        //    var body = Expression.Add(
        //            Expression.Invoke(left, param),
        //            Expression.Invoke(right, param)
        //        );
        //    var lambda = Expression.Lambda<Func<TEntity, bool>>(body, param);
        //    return lambda;
        //}

        //By passing a "Func" you will pass the compiled lambda, so you cannot access the expression tree any more.
        //public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> collection, Expression<Func<TEntity, bool>> otherPredicate)
        //{
        //    var binExpr = otherPredicate.Body as BinaryExpression;
        //    var value = binExpr.Right;

        //    var func = otherPredicate.Compile();
        //    return collection.Where<TEntity>(otherPredicate);
        //}


    }
}
