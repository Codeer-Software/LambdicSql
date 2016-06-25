using LambdicSql.Inside;
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
             => query.CustomClone(dst => dst.Where.And((BinaryExpression)condition.Body));

        public static IWhereQuery<TDB, TSelect> And<TDB, TSelect>(this IWhereQueryNot<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And((BinaryExpression)condition.Body));

        public static IWhereQuery<TDB, TSelect> And<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query, Expression<Func<TDB, IWhereFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And((BinaryExpression)condition.Body));

        public static IWhereQuery<TDB, TSelect> And<TDB, TSelect>(this IWhereQueryNot<TDB, TSelect> query, Expression<Func<TDB, IWhereFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.And((BinaryExpression)condition.Body));

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
             => query.CustomClone(dst => dst.Where.Or((BinaryExpression)condition.Body));

        public static IWhereQueryNot<TDB, TSelect> Or<TDB, TSelect>(this IWhereQueryNot<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or((BinaryExpression)condition.Body));

        public static IWhereQuery<TDB, TSelect> Or<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query, Expression<Func<TDB, IWhereFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or((BinaryExpression)condition.Body));

        public static IWhereQueryNot<TDB, TSelect> Or<TDB, TSelect>(this IWhereQueryNot<TDB, TSelect> query, Expression<Func<TDB, IWhereFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or((BinaryExpression)condition.Body));

        public static IWhereQueryConnectable<TDB, TSelect> Or<TDB, TSelect>(this IWhereQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or());

        public static IWhereQueryConnectableNot<TDB, TSelect> Or<TDB, TSelect>(this IWhereQueryNot<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Or());
        #endregion

        public static IWhereQuery<TDB, TSelect> In<TDB, TSelect, TLeft>(this IWhereQueryConnectable<TDB, TSelect> query, Expression<Func<TDB, TLeft>> target, params TLeft[] inArguments)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.In(target.GetElementName(), inArguments));

        public static IWhereQuery<TDB, TSelect> In<TDB, TSelect, TLeft>(this IWhereQueryConnectableNot<TDB, TSelect> query, Expression<Func<TDB, TLeft>> target, params TLeft[] inArguments)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.In(target.GetElementName(), inArguments));

        public static IWhereQuery<TDB, TSelect> Like<TDB, TSelect>(this IWhereQueryConnectable<TDB, TSelect> query, Expression<Func<TDB, string>> target, string serachText)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Like(target.GetElementName(), serachText));

        public static IWhereQuery<TDB, TSelect> Like<TDB, TSelect>(this IWhereQueryConnectableNot<TDB, TSelect> query, Expression<Func<TDB, string>> target, string serachText)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.Like(target.GetElementName(), serachText));

        //TODO BETWEEN
        /*
        public static IWhereQuery<TDB, TSelect> IsNull<TDB, TSelect, TRight>(this IWhereQueryConnectable<TDB, TSelect> query, Expression<Func<TDB, TRight>> target)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where.IsNull(target.GetElementName()));
             */
    }
}
