using LambdicSql.Clause.OrderBy;
using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class OrderByQueryExtensions
    {
        public static IQuery<TDB, TSelect, OrderByClause> OrderBy<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
            => new ClauseMakingQuery<TDB, TSelect, OrderByClause>(query, new OrderByClause());

        public static T ToSubQuery<T>(this IQuery query) => default(T);

        public static IQuery<TDB, TSelect, OrderByClause> ASC<TDB, TSelect, T>(this IQuery<TDB, TSelect, OrderByClause> query, Expression<Func<TDB, T>> exp)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Add(Order.ASC, exp.Body));

        public static IQuery<TDB, TSelect, OrderByClause> DESC<TDB, TSelect, T>(this IQuery<TDB, TSelect, OrderByClause> query, Expression<Func<TDB, T>> exp)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Add(Order.DESC, exp.Body));
    }
}
