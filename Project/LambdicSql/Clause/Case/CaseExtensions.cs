using LambdicSql.Clause.Case;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class CaseExtensions
    {
        public static IQuery<TDB, TSelect, CaseClause> Case<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
            => new ClauseMakingQuery<TDB, TSelect, CaseClause>(query, new CaseClause());

        public static IQuery<TDB, TSelect, CaseClause<TTarget>> Case<TDB, TSelect, TTarget>(this IQuery<TDB, TSelect> query, Expression<Func<TDB, TTarget>> target)
            where TDB : class
            where TSelect : class
            => new ClauseMakingQuery<TDB, TSelect, CaseClause<TTarget>>(query, new CaseClause<TTarget>(target.Body));

        public static IQuery<TDB, TSelect, CaseClause> WhenThen<TDB, TSelect, TValue>(this IQuery<TDB, TSelect, CaseClause> query, Expression<Func<TDB, bool>> condition, TValue value)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.WhenThen(condition.Body, value));

        public static IQuery<TDB, TSelect, CaseClause<TTarget>> WhenThen<TDB, TSelect, TTarget, TValue>(this IQuery<TDB, TSelect, CaseClause<TTarget>> query, TTarget caseTarget, TValue value)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.WhenThen(caseTarget, value));

        public static IQuery<TDB, TSelect, CaseClause> Else<TDB, TSelect, TValue>(this IQuery<TDB, TSelect, CaseClause> query, TValue value)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Else(value));

        public static IQuery<TDB, TSelect, CaseClause<TTarget>> Else<TDB, TSelect, TTarget, TValue>(this IQuery<TDB, TSelect, CaseClause<TTarget>> query, TValue value)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Else(value));

    }
}
