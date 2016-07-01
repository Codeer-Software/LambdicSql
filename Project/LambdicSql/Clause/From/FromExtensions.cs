using LambdicSql.Inside;
using LambdicSql.QueryBase;
using LambdicSql.Clause.From;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class FromExtensions
    {
        public static IQueryFrom<TDB, TSelect> From<TDB, TSelect, T>(this IQuery<TDB, TSelect> query, Expression<Func<TDB, T>> table)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.From = new FromClause(table.Body));

        public static IQueryFrom<TDB, TSelect> Join<TDB, TSelect>(this IQueryFrom<TDB, TSelect> query, Expression<Func<TDB, object>> table, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.From.Join(new JoinElement(table.Body, condition.Body)));
    }
}
