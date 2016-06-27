﻿using LambdicSql.Inside;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class HavingQueryExtensions
    {
        public static IQueryHaving<TDB, TSelect> Not<TDB, TSelect>(this IQueryHaving<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Having.Not());

        public static IQueryHaving<TDB, TSelect> And<TDB, TSelect>(this IQueryHaving<TDB, TSelect> query, Expression<Func<TDB, IHavingFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Having.And(condition.Body));

        public static IQueryHaving<TDB, TSelect> And<TDB, TSelect>(this IQueryHaving<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Having.And());
        
        public static IQueryHaving<TDB, TSelect> Or<TDB, TSelect>(this IQueryHaving<TDB, TSelect> query, Expression<Func<TDB, IHavingFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Having.Or(condition.Body));

        public static IQueryHaving<TDB, TSelect> Or<TDB, TSelect>(this IQueryHaving<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Having.Or());

        public static IQueryHaving<TDB, TSelect> In<TDB, TSelect, TTarget>(this IQueryHaving<TDB, TSelect> query, Expression<Func<TDB, IHavingFuncs, TTarget>> target, params TTarget[] inArguments)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Having.In(target.Body, inArguments));

        public static IQueryHaving<TDB, TSelect> Like<TDB, TSelect>(this IQueryHaving<TDB, TSelect> query, Expression<Func<TDB, IHavingFuncs, string>> target, string serachText)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Having.Like(target.Body, serachText));

        public static IQueryHaving<TDB, TSelect> Between<TDB, TSelect, TTarget>(this IQueryHaving<TDB, TSelect> query, Expression<Func<TDB, IHavingFuncs, TTarget>> target, TTarget min, TTarget max)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Having.Between(target.Body, min, max));
    }
}