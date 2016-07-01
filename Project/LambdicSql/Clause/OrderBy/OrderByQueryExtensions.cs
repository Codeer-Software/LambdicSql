using LambdicSql.Clause.OrderBy;
using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class OrderByQueryExtensions
    {
        public static IQueryOrderBy<TDB, TSelect> OrderBy<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.OrderBy = new OrderByClause());

        public static T ToSubQuery<T>(this IQuery query) => default(T);

        public static IQueryOrderBy<TDB, TSelect> ASC<TDB, TSelect, T>(this IQueryOrderBy<TDB, TSelect> query, Expression<Func<TDB, T>> exp)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.OrderBy.Add(Order.ASC, exp.Body));

        public static IQueryOrderBy<TDB, TSelect> DESC<TDB, TSelect, T>(this IQueryOrderBy<TDB, TSelect> query, Expression<Func<TDB, T>> exp)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.OrderBy.Add(Order.DESC, exp.Body));
    }
}
