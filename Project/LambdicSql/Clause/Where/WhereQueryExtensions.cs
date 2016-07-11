using LambdicSql.Clause.Where;
using LambdicSql.QueryBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class WhereQueryExtensions
    {
        public static IQuery<TDB, TSelect, WhereClause> Where<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => new ClauseMakingQuery<TDB, TSelect, WhereClause>(query, new WhereClause());

        public static IQuery<TDB, TSelect, WhereClause> Where<TDB, TSelect>(this IQuery<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => new ClauseMakingQuery<TDB, TSelect, WhereClause>(query, new WhereClause(condition.Body));

        public static IQuery<TDB, TSelect, WhereClause> Where<TDB, TSelect, TParams>(this IQuery<TDB, TSelect> query, Expression<Func<TDB, TParams, bool>> condition, TParams parameters)
            where TDB : class
            where TSelect : class
             => new ClauseMakingQuery<TDB, TSelect, WhereClause>(query, new WhereClause(condition.Body, parameters));

        public static IQuery<TDB, TSelect, WhereClause> Not<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Not());

        public static IQuery<TDB, TSelect, WhereClause> And<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.And(condition.Body));

        public static IQuery<TDB, TSelect, WhereClause> And<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.And());

        public static IQuery<TDB, TSelect, WhereClause> Or<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Or(condition.Body));

        public static IQuery<TDB, TSelect, WhereClause> Or<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Or());

        public static IQuery<TDB, TSelect, WhereClause> In<TDB, TSelect, TTarget>(this IQuery<TDB, TSelect, WhereClause> query, Expression<Func<TDB, TTarget>> target, params TTarget[] inArguments)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.In(target.Body, inArguments.Cast<object>().ToArray()));

        public static IQuery<TDB, TSelect, WhereClause> In<TDB, TSelect, TTarget>(this IQuery<TDB, TSelect, WhereClause> query, Expression<Func<TDB, TTarget>> target, Expression<Func<TDB, TTarget>> inArgument)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.In(target.Body, inArgument.Body));

        public static IQuery<TDB, TSelect, WhereClause> Like<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query, Expression<Func<TDB, string>> target, string serachText)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Like(target.Body, serachText));

        public static IQuery<TDB, TSelect, WhereClause> Like<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query, Expression<Func<TDB, string>> target, Expression<Func<TDB, string>> serachText)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Like(target.Body, serachText.Body));

        public static IQuery<TDB, TSelect, WhereClause> Between<TDB, TSelect, TTarget>(this IQuery<TDB, TSelect, WhereClause> query, Expression<Func<TDB, TTarget>> target, TTarget min, TTarget max)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Between(target.Body, min, max));

        public static IQuery<TDB, TSelect, WhereClause> Between<TDB, TSelect, TTarget>(this IQuery<TDB, TSelect, WhereClause> query, Expression<Func<TDB, TTarget>> target, Expression<Func<TDB, TTarget>> min, Expression<Func<TDB, TTarget>> max)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Between(target.Body, min.Body, max.Body));

        public static IQuery<TDB, TSelect, WhereClause> BlockStart<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.BlockStart());

        public static IQuery<TDB, TSelect, WhereClause> BlockEnd<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.BlockEnd());
    }
}
