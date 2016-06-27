using LambdicSql.Inside;
using LambdicSql.QueryInfo;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class OrderByQueryExtensions
    {
        public static IQueryOrderBy<TDB, TSelect> OrderBy<TDB, TSelect>(this IQueryStart<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.OrderBy = new OrderByInfo());

        public static IQueryOrderBy<TDB, TSelect> ASC<TDB, TSelect>(this IQueryOrderBy<TDB, TSelect> query, Expression<Func<TDB, object>> exp)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.OrderBy.Add(Order.ASC, exp.Body));

        public static IQueryOrderBy<TDB, TSelect> DESC<TDB, TSelect>(this IQueryOrderBy<TDB, TSelect> query, Expression<Func<TDB, object>> exp)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.OrderBy.Add(Order.DESC, exp.Body));
    }
}
