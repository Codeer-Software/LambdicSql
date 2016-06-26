﻿using LambdicSql.Inside;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class WhereQueryExtensions
    {
        #region Not
        public static IWhereQueryNot<TDB, TSelect> Not<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Not());

        public static IWhereQueryConnectableNot<TDB, TSelect> Not<TDB, TSelect>(this IWhereQueryConnectable<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Not());
        #endregion

        #region And
        public static IWhereQuery<TDB, TSelect> And<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And(condition.Body));

        public static IWhereQuery<TDB, TSelect> And<TDB, TSelect>(this IWhereQueryNot<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And(condition.Body));
 
        public static IWhereQuery<TDB, TSelect> And<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query, Expression<Func<TDB, IWhereFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And(condition.Body));

        public static IWhereQuery<TDB, TSelect> And<TDB, TSelect>(this IWhereQueryNot<TDB, TSelect> query, Expression<Func<TDB, IWhereFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And(condition.Body));

        public static IWhereQueryConnectable<TDB, TSelect> And<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And());

        public static IWhereQueryConnectableNot<TDB, TSelect> And<TDB, TSelect>(this IWhereQueryNot<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And());
        #endregion

        #region Or
        public static IWhereQuery<TDB, TSelect> Or<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or(condition.Body));

        public static IWhereQueryNot<TDB, TSelect> Or<TDB, TSelect>(this IWhereQueryNot<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or(condition.Body));

        public static IWhereQuery<TDB, TSelect> Or<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query, Expression<Func<TDB, IWhereFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or(condition.Body));

        public static IWhereQueryNot<TDB, TSelect> Or<TDB, TSelect>(this IWhereQueryNot<TDB, TSelect> query, Expression<Func<TDB, IWhereFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or(condition.Body));

        public static IWhereQueryConnectable<TDB, TSelect> Or<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or());

        public static IWhereQueryConnectableNot<TDB, TSelect> Or<TDB, TSelect>(this IWhereQueryNot<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or());
        #endregion

        public static IWhereQuery<TDB, TSelect> In<TDB, TSelect, TTarget>(this IWhereQueryConnectable<TDB, TSelect> query, Expression<Func<TDB, TTarget>> target, params TTarget[] inArguments)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.In(target.Body, inArguments));

        public static IWhereQuery<TDB, TSelect> In<TDB, TSelect, TTarget>(this IWhereQueryConnectableNot<TDB, TSelect> query, Expression<Func<TDB, TTarget>> target, params TTarget[] inArguments)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.In(target.Body, inArguments));

        public static IWhereQuery<TDB, TSelect> Like<TDB, TSelect>(this IWhereQueryConnectable<TDB, TSelect> query, Expression<Func<TDB, string>> target, string serachText)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Like(target.Body, serachText));

        public static IWhereQuery<TDB, TSelect> Like<TDB, TSelect>(this IWhereQueryConnectableNot<TDB, TSelect> query, Expression<Func<TDB, string>> target, string serachText)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Like(target.Body, serachText));

        public static IWhereQuery<TDB, TSelect> Between<TDB, TSelect, TTarget>(this IWhereQueryConnectable<TDB, TSelect> query, Expression<Func<TDB, TTarget>> target, TTarget min, TTarget max)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Between(target.Body, min, max));

        public static IWhereQuery<TDB, TSelect> Between<TDB, TSelect, TTarget>(this IWhereQueryConnectableNot<TDB, TSelect> query, Expression<Func<TDB, TTarget>> target, TTarget min, TTarget max)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Between(target.Body, min, max));
    }
}
