using LambdicSql.Inside;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql
{
    public static class WhereQueryExtensions
    {
        public static IQueryWhere<TDB, TSelect> Not<TDB, TSelect>(this IQueryWhere<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Not());

        public static IQueryWhere<TDB, TSelect> And<TDB, TSelect>(this IQueryWhere<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And(condition.Body));

        public static IQueryWhere<TDB, TSelect> And<TDB, TSelect>(this IQueryWhere<TDB, TSelect> query, Expression<Func<TDB, IWhereFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And(condition.Body));

        public static IQueryWhere<TDB, TSelect> And<TDB, TSelect>(this IQueryWhere<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And());

        public static IQueryWhere<TDB, TSelect> Or<TDB, TSelect>(this IQueryWhere<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or(condition.Body));

        public static IQueryWhere<TDB, TSelect> Or<TDB, TSelect>(this IQueryWhere<TDB, TSelect> query, Expression<Func<TDB, IWhereFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or(condition.Body));

        public static IQueryWhere<TDB, TSelect> Or<TDB, TSelect>(this IQueryWhere<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or());

        public static IQueryWhere<TDB, TSelect> In<TDB, TSelect, TTarget>(this IQueryWhere<TDB, TSelect> query, Expression<Func<TDB, TTarget>> target, params TTarget[] inArguments)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.In(target.Body, inArguments.Cast<object>().ToArray()));

        public static IQueryWhere<TDB, TSelect> In<TDB, TSelect, TTarget>(this IQueryWhere<TDB, TSelect> query, Expression<Func<TDB, TTarget>> target, IQuery inArgument)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.In(target.Body, inArgument));

        public static IQueryWhere<TDB, TSelect> Like<TDB, TSelect>(this IQueryWhere<TDB, TSelect> query, Expression<Func<TDB, string>> target, string serachText)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Like(target.Body, serachText));

        public static IQueryWhere<TDB, TSelect> Like<TDB, TSelect>(this IQueryWhere<TDB, TSelect> query, Expression<Func<TDB, string>> target, IQuery serachText)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Like(target.Body, serachText));

        public static IQueryWhere<TDB, TSelect> Between<TDB, TSelect, TTarget>(this IQueryWhere<TDB, TSelect> query, Expression<Func<TDB, TTarget>> target, TTarget min, TTarget max)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Between(target.Body, min, max));

        public static IQueryWhere<TDB, TSelect> Between<TDB, TSelect, TTarget>(this IQueryWhere<TDB, TSelect> query, Expression<Func<TDB, TTarget>> target, IQuery min, IQuery max)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Between(target.Body, min, max));
    }
}
