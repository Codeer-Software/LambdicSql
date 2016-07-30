using LambdicSql.Clause.Where;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class WhereQueryExtensions
    {
        public static IQuery<TDB, TSelect, WhereClause> Where<TDB, TSelect>(this IQuery<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => new ClauseMakingQuery<TDB, TSelect, WhereClause>(query, new WhereClause(condition.Body));
    }
}
