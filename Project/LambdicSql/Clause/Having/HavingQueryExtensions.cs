using LambdicSql.Clause.Having;
using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class HavingQueryExtensions
    {
        public static IQuery<TDB, TSelect, HavingClause> Having<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
            => new ClauseMakingQuery<TDB, TSelect, HavingClause>(query, new HavingClause());

        public static IQuery<TDB, TSelect, HavingClause> Having<TDB, TSelect>(this IQuery<TDB, TSelect> query, Expression<Func<TDB, IHavingFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
            => new ClauseMakingQuery<TDB, TSelect, HavingClause>(query, new HavingClause(condition.Body));

        public static IQuery<TDB, TSelect, HavingClause> Not<TDB, TSelect>(this IQuery<TDB, TSelect, HavingClause> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Not());

        public static IQuery<TDB, TSelect, HavingClause> And<TDB, TSelect>(this IQuery<TDB, TSelect, HavingClause> query, Expression<Func<TDB, IHavingFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.And(condition.Body));

        public static IQuery<TDB, TSelect, HavingClause> And<TDB, TSelect>(this IQuery<TDB, TSelect, HavingClause> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.And());

        public static IQuery<TDB, TSelect, HavingClause> Or<TDB, TSelect>(this IQuery<TDB, TSelect, HavingClause> query, Expression<Func<TDB, IHavingFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Or(condition.Body));

        public static IQuery<TDB, TSelect, HavingClause> Or<TDB, TSelect>(this IQuery<TDB, TSelect, HavingClause> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Or());

        public static IQuery<TDB, TSelect, HavingClause> In<TDB, TSelect, TTarget>(this IQuery<TDB, TSelect, HavingClause> query, Expression<Func<TDB, IHavingFuncs, TTarget>> target, params TTarget[] inArguments)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.In(target.Body, inArguments));

        public static IQuery<TDB, TSelect, HavingClause> Like<TDB, TSelect>(this IQuery<TDB, TSelect, HavingClause> query, Expression<Func<TDB, IHavingFuncs, string>> target, string serachText)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Like(target.Body, serachText));

        public static IQuery<TDB, TSelect, HavingClause> Between<TDB, TSelect, TTarget>(this IQuery<TDB, TSelect, HavingClause> query, Expression<Func<TDB, IHavingFuncs, TTarget>> target, TTarget min, TTarget max)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Between(target.Body, min, max));
    }
}
