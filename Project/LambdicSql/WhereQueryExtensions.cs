using LambdicSql.Inside;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class WhereQueryExtensions
    {
        public static IWhereQuery<TDB, TSelect> Not<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Not());

        public static IWhereQuery<TDB, TSelect> And<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And(condition.Body));

        public static IWhereQuery<TDB, TSelect> And<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query, Expression<Func<TDB, IWhereFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And(condition.Body));

        public static IWhereQuery<TDB, TSelect> And<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And());

        public static IWhereQuery<TDB, TSelect> Or<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or(condition.Body));

        public static IWhereQuery<TDB, TSelect> Or<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query, Expression<Func<TDB, IWhereFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or(condition.Body));

        public static IWhereQuery<TDB, TSelect> Or<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or());

        public static IWhereQuery<TDB, TSelect> In<TDB, TSelect, TTarget>(this IWhereQuery<TDB, TSelect> query, Expression<Func<TDB, TTarget>> target, params TTarget[] inArguments)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.In(target.Body, inArguments));

        public static IWhereQuery<TDB, TSelect> Like<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query, Expression<Func<TDB, string>> target, string serachText)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Like(target.Body, serachText));

        public static IWhereQuery<TDB, TSelect> Between<TDB, TSelect, TTarget>(this IWhereQuery<TDB, TSelect> query, Expression<Func<TDB, TTarget>> target, TTarget min, TTarget max)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Between(target.Body, min, max));
    }
}
