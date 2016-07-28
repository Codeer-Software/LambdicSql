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

        public static IQuery<TDB, TSelect, WhereClause> And<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query, bool isEnabled, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => isEnabled ? query.CustomClone(e => e.And(condition.Body)) : query;

        public static IQuery<TDB, TSelect, WhereClause> And<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.And());

        public static IQuery<TDB, TSelect, WhereClause> And<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query, bool isEnabled)
            where TDB : class
            where TSelect : class
             => isEnabled ? query.CustomClone(e => e.And()) : query.CustomClone(e => e.Skip());

        public static IQuery<TDB, TSelect, WhereClause> Or<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Or(condition.Body));

        public static IQuery<TDB, TSelect, WhereClause> Or<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query, bool isEnabled, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => isEnabled ? query.CustomClone(e => e.Or(condition.Body)) : query;
        
        public static IQuery<TDB, TSelect, WhereClause> Or<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Or());

        public static IQuery<TDB, TSelect, WhereClause> Or<TDB, TSelect>(this IQuery<TDB, TSelect, WhereClause> query, bool isEnabled)
            where TDB : class
            where TSelect : class
             => isEnabled ? query.CustomClone(e => e.Or()) : query.CustomClone(e => e.Skip());

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
