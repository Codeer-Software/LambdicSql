using LambdicSql.Inside;
using LambdicSql.QueryInfo;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class OrderByQueryExtensions
    {
        public static IOrderByQuery<TDB, TSelect> OrderBy<TDB, TSelect>(this IQueryStart<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.OrderBy = new OrderByInfo());

        public static IOrderByQuery<TDB, TSelect> ASC<TDB, TSelect>(this IOrderByQuery<TDB, TSelect> query, Expression<Func<TDB, object>> exp)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.OrderBy.Add(Order.ASC, exp.Body));

        public static IOrderByQuery<TDB, TSelect> DESC<TDB, TSelect>(this IOrderByQuery<TDB, TSelect> query, Expression<Func<TDB, object>> exp)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.OrderBy.Add(Order.DESC, exp.Body));
    }
}
