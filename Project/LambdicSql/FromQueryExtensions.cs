using LambdicSql.Inside;
using LambdicSql.QueryInfo;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class FromQueryExtensions
    {
        public static IQueryFrom<TDB, TSelect> Join<TDB, TSelect>(this IQueryFrom<TDB, TSelect> query, Expression<Func<TDB, object>> table, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.From.Join(new JoinInfo(table.Body, condition.Body)));
    }
}
