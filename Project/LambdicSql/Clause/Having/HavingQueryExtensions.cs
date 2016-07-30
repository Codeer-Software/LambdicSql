using LambdicSql.Clause.Having;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class HavingQueryExtensions
    {
        public static IQuery<TDB, TSelect, HavingClause> Having<TDB, TSelect>(this IQuery<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
            => new ClauseMakingQuery<TDB, TSelect, HavingClause>(query, new HavingClause(condition.Body));
    }
}
