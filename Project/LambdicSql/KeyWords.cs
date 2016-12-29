using LambdicSql.Inside;
using LambdicSql.Inside.Keywords;
using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;
using LambdicSql.SqlBase.TextParts;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;
using System.Linq;

namespace LambdicSql
{
    //TODO 改行をいれながらドキュメントというかサンプルを書く！

    //<para>paraを</para>
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RecursiveArguments<T> { }

    /// <summary>
    /// SQL Keywords.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// Use[using static LambdicSql.Keywords;], you can use to write natural SQL.
    /// </summary>
    public static class Keywords
    {
        #region Keywords
        //あー、WITH、ASかな
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subQuerys"></param>
        /// <returns></returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> With(params ISqlExpression[] subQuerys) => InvalitContext.Throw<ClauseChain<object>>(nameof(With));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSelected"></typeparam>
        /// <param name="args"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> With<TSelected>(SqlRecursiveArgumentsExpression<TSelected> args, ISqlExpression select) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(With));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSelected"></typeparam>
        /// <param name="selected"></param>
        /// <returns></returns>
        [SqlSyntaxTmp]
        public static RecursiveArguments<TSelected> Recursive<TSelected>(TSelected selected) => InvalitContext.Throw<RecursiveArguments<TSelected>>(nameof(Select));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSelected"></typeparam>
        /// <param name="before"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSelected>(this ClauseChain<TSelected> before, object[] items) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSelected>(TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSelected>(ITop top, TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, ITop top, TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Select(IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<object>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Select<TSrcSelected>(this ClauseChain<TSrcSelected> before, IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<object>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSelected>(IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Select(ITop top, IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<object>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Select<TSrcSelected>(this ClauseChain<TSrcSelected> before, ITop top, IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<object>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSelected>(ITop top, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, ITop top, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSelected>(IAggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSelected>(IAggregatePredicate predicate, ITop top, TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, ITop top, TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Select(IAggregatePredicate predicate, IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<object>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Select<TSrcSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<object>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSelected>(IAggregatePredicate predicate, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Select(IAggregatePredicate predicate, ITop top, IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<object>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Select<TSrcSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, ITop top, IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<object>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSelected>(IAggregatePredicate predicate, ITop top, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, ITop top, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// Return value of CASE clause.
        /// </summary>
        public interface ICaseAfter { }

        /// <summary>
        /// Return value of WHEN clause.
        /// </summary>
        public interface IWhenAfter { }

        /// <summary>
        /// Return value of WHEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        public interface IWhenAfter<T> { }

        /// <summary>
        /// Return value of THEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        public interface IThenAfter<T> { }

        /// <summary>
        /// Return value of ELSE clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        public interface IElseAfter<T> { }

        /// <summary>
        /// CASE clause.
        /// </summary>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [SqlSyntaxTmp]
        public static ICaseAfter Case() => InvalitContext.Throw<ICaseAfter>(nameof(Case));

        /// <summary>
        /// CASE clause.
        /// </summary>
        /// <param name="target">It's target of CASE branch.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [SqlSyntaxTmp]
        public static ICaseAfter Case(object target) => InvalitContext.Throw<ICaseAfter>(nameof(Case));

        /// <summary>
        /// WHEN clause.
        /// </summary>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="expression">It is a conditional expression of the WHEN clause.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [SqlSyntaxTmp]
        public static IWhenAfter When(this ICaseAfter before, object expression) => InvalitContext.Throw<IWhenAfter>(nameof(When));

        /// <summary>
        /// WHEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="expression">It is a conditional expression of the WHEN clause.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [SqlSyntaxTmp]
        public static IWhenAfter<T> When<T>(this IThenAfter<T> before, object expression) => InvalitContext.Throw<IWhenAfter<T>>(nameof(When));

        /// <summary>
        /// THEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the THEN clause is valid.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [SqlSyntaxTmp]
        public static IThenAfter<T> Then<T>(this IWhenAfter before, T result) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));

        /// <summary>
        /// THEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the THEN clause is valid.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [SqlSyntaxTmp]
        public static IThenAfter<T> Then<T>(this IWhenAfter<T> before, T result) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));

        /// <summary>
        /// ELSE clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the ELSE clause is valid.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [SqlSyntaxTmp]
        public static IElseAfter<T> Else<T>(this IThenAfter<T> before, T result) => InvalitContext.Throw<IElseAfter<T>>(nameof(Then));

        /// <summary>
        /// END clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <returns>It is the result of CASE expression.</returns>
        [SqlSyntaxTmp]
        public static T End<T>(this IThenAfter<T> before) => InvalitContext.Throw<T>(nameof(End));

        /// <summary>
        /// END clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <returns>It is the result of CASE expression.</returns>
        [SqlSyntaxTmp]
        public static T End<T>(this IElseAfter<T> before) => InvalitContext.Throw<T>(nameof(End));

        /// <summary>
        /// FROM clause.
        /// </summary>
        /// <param name="expressions">Table or subquery.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> From(params object[] expressions) => InvalitContext.Throw<ClauseChain<object>>(nameof(From));

        /// <summary>
        /// FROM clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="tables">Table or subquery.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> From<TSelected>(this ClauseChain<TSelected> before, params object[] tables) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(From));

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Join(object table, bool condition) => InvalitContext.Throw<ClauseChain<object>>(nameof(Join));

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Join<TSelected>(this ClauseChain<TSelected> before, object table, bool condition) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Join));

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of LEFT JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> LeftJoin(object table, bool condition) => InvalitContext.Throw<ClauseChain<object>>(nameof(LeftJoin));

        /// <summary>
        /// LEFT JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of LEFT JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> LeftJoin<TSelected>(this ClauseChain<TSelected> before, object table, bool condition) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(LeftJoin));

        /// <summary>
        /// RIGHT JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> RightJoin(object table, bool condition) => InvalitContext.Throw<ClauseChain<object>>(nameof(RightJoin));

        /// <summary>
        /// RIGHT JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> RightJoin<TSelected>(this ClauseChain<TSelected> before, object table, bool condition) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(RightJoin));

        /// <summary>
        /// FULL JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> FullJoin(object table, bool condition) => InvalitContext.Throw<ClauseChain<object>>(nameof(FullJoin));

        /// <summary>
        /// FULL JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> FullJoin<TSelected>(this ClauseChain<TSelected> before, object table, bool condition) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(FullJoin));

        /// <summary>
        /// CROSS JOIN clause.
        /// </summary>
        /// <param name="expression">Table or subquery.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> CrossJoin(object expression) => InvalitContext.Throw<ClauseChain<object>>(nameof(CrossJoin));

        /// <summary>
        /// CROSS JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> CrossJoin<TSelected>(this ClauseChain<TSelected> before, object table) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(CrossJoin));

        /// <summary>
        /// WHERE clause.
        /// </summary>
        /// <param name="condition">It is a conditional expression of WHERE.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Where(bool condition) => InvalitContext.Throw<ClauseChain<object>>(nameof(Where));

        /// <summary>
        /// WHERE clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="condition">It is a conditional expression of WHERE.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Where<TSelected>(this ClauseChain<TSelected> before, bool condition) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Where));

        /// <summary>
        /// GROUP BY clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> GroupBy(params object[] columns) => InvalitContext.Throw<ClauseChain<object>>(nameof(GroupBy));

        /// <summary>
        /// GROUP BY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> GroupBy<TSelected>(this ClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(GroupBy));

        /// <summary>
        /// GROUP BY ROLLUP clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> GroupByRollup(params object[] columns) => InvalitContext.Throw<ClauseChain<object>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY ROLLUP clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> GroupByRollup<TSelected>(this ClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY columns WITH ROLLUP clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> GroupByWithRollup(params object[] columns) => InvalitContext.Throw<ClauseChain<object>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY columns WITH ROLLUP clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> GroupByWithRollup<TSelected>(this ClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY CUBE clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> GroupByCube(params object[] columns) => InvalitContext.Throw<ClauseChain<object>>(nameof(GroupByCube));

        /// <summary>
        /// GROUP BY CUBE clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> GroupByCube<TSelected>(this ClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(GroupByCube));

        /// <summary>
        /// GROUP BY GROUPING SETS clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> GroupByGroupingSets(params object[] columns) => InvalitContext.Throw<ClauseChain<object>>(nameof(GroupByGroupingSets));

        /// <summary>
        /// GROUP BY GROUPING SETS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> GroupByGroupingSets<TSelected>(this ClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(GroupByGroupingSets));

        /// <summary>
        /// HAVING clause.
        /// </summary>
        /// <param name="condition">It is a conditional expression of HAVING.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Having(bool condition) => InvalitContext.Throw<ClauseChain<object>>(nameof(Having));

        /// <summary>
        /// HAVING clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="condition">It is a conditional expression of HAVING.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Having<TSelected>(this ClauseChain<TSelected> before, bool condition) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Having));

        /// <summary>
        /// ORDER BY clause.
        /// </summary>
        /// <param name="elements">Specify column and sort order. Asc(column) or Desc(column).</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static OrderBy OrderBy(params ISortedBy[] elements) => InvalitContext.Throw<OrderBy>(nameof(OrderBy));

        /// <summary>
        /// ORDER BY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="elements">Specify column and sort order. Asc(column) or Desc(column).</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> OrderBy<TSelected>(this ClauseChain<TSelected> before, params ISortedBy[] elements) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Limit(object count) => InvalitContext.Throw<ClauseChain<object>>(nameof(OrderBy));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Limit<TSelected>(this ClauseChain<TSelected> before, object count) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <param name="offset">Start position.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Limit(object offset, object count) => InvalitContext.Throw<ClauseChain<object>>(nameof(OrderBy));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="offset">Start position.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Limit<TSelected>(this ClauseChain<TSelected> before, object offset, object count) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// OFFSET clause.
        /// </summary>
        /// <param name="offset">Start position.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Offset(object offset) => InvalitContext.Throw<ClauseChain<object>>(nameof(OrderBy));

        /// <summary>
        /// OFFSET clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="offset">Start position.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Offset<TSelected>(this ClauseChain<TSelected> before, object offset) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// OFFSET count ROWS clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> OffsetRows(object count) => InvalitContext.Throw<ClauseChain<object>>(nameof(OrderBy));

        /// <summary>
        /// OFFSET count ROWS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> OffsetRows<TSelected>(this ClauseChain<TSelected> before, object count) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// FETCH NEXT count ROWS ONLY clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> FetchNextRowsOnly(object count) => InvalitContext.Throw<ClauseChain<object>>(nameof(OrderBy));

        /// <summary>
        /// FETCH NEXT count ROWS ONLY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> FetchNextRowsOnly<TSelected>(this ClauseChain<TSelected> before, object count) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Union() => InvalitContext.Throw<ClauseChain<object>>(nameof(Union));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Union<TSelected>(this ClauseChain<TSelected> before) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Union));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Union(bool isAll) => InvalitContext.Throw<ClauseChain<object>>(nameof(Union));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Union<TSelected>(this ClauseChain<TSelected> before, bool isAll) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Union));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Intersect() => InvalitContext.Throw<ClauseChain<object>>(nameof(Intersect));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Intersect<TSelected>(this ClauseChain<TSelected> before) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Intersect));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Intersect(bool isAll) => InvalitContext.Throw<ClauseChain<object>>(nameof(Intersect));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Intersect<TSelected>(this ClauseChain<TSelected> before, bool isAll) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Intersect));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Except() => InvalitContext.Throw<ClauseChain<object>>(nameof(Except));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Except<TSelected>(this ClauseChain<TSelected> before) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Except));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Except(bool isAll) => InvalitContext.Throw<ClauseChain<object>>(nameof(Except));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Except<TSelected>(this ClauseChain<TSelected> before, bool isAll) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Except));

        /// <summary>
        /// MINUS clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Minus() => InvalitContext.Throw<ClauseChain<object>>(nameof(Minus));

        /// <summary>
        /// MINUS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Minus<TSelected>(this ClauseChain<TSelected> before) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Minus));

        /// <summary>
        /// UPDATE clause.
        /// </summary>
        /// <param name="table">Table for UPDATE.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Update(object table) => InvalitContext.Throw<ClauseChain<object>>(nameof(Update));

        /// <summary>
        /// SET clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="assigns">Assignment in the SET clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Set<TSelected>(this ClauseChain<TSelected> before, params Assign[] assigns) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Set));

        /// <summary>
        /// DELETE clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> Delete() => InvalitContext.Throw<ClauseChain<object>>(nameof(Delete));

        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <param name="table">Table for INSERT.</param>
        /// <param name="columns">It is a column that performs INSERT.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<object> InsertInto(object table, params object[] columns) => InvalitContext.Throw<ClauseChain<object>>(nameof(InsertInto));

        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="values">It is the value to be Inserted.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [SqlSyntaxTmp]
        public static ClauseChain<TSelected> Values<TSelected>(this ClauseChain<TSelected> before, params object[] values) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Values));

        /// <summary>
        /// LIKE keyword.
        /// </summary>
        /// <param name="target">Target text.</param>
        /// <param name="pattern">Text that represents pattern matching.</param>
        /// <returns>If target matches the specified pattern, LIKE returns TRUE.</returns>
        [SqlSyntaxTmp]
        public static bool Like(object target, object pattern) => InvalitContext.Throw<bool>(nameof(Like));

        /// <summary>
        /// BETWEEN keyword.
        /// </summary>
        /// <param name="target">Target of range check.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maxmum value.</param>
        /// <returns>Returns TRUE if target is included in the range of min and max.</returns>
        [SqlSyntaxTmp]
        public static bool Between(object target, object min, object max) => InvalitContext.Throw<bool>(nameof(Between));

        /// <summary>
        /// IN keyword.
        /// </summary>
        /// <param name="target">Target of IN check.</param>
        /// <param name="canditates">Canditates.</param>
        /// <returns>Returns TRUE if target is included in the canditates represented by expression.</returns>
        [SqlSyntaxTmp]
        public static bool In(object target, params object[] canditates) => InvalitContext.Throw<bool>(nameof(In));

        /// <summary>
        /// IN keyword.
        /// </summary>
        /// <param name="target">Target of IN check.</param>
        /// <param name="canditates">Candidates expected for target.</param>
        /// <returns>Returns TRUE if target is included in the candidate represented by expression.</returns>
        [SqlSyntaxTmp]
        public static bool In(object target, ISqlExpression canditates) => InvalitContext.Throw<bool>(nameof(In));

        /// <summary>
        /// IN keyword.
        /// </summary>
        /// <param name="target">Target of IN check.</param>
        /// <param name="canditates">Table or subquery.</param>
        /// <returns>Returns TRUE if target is included in the candidate represented by expression.</returns>
        [SqlSyntaxTmp]
        public static bool In(object target, IClauseChain canditates) => InvalitContext.Throw<bool>(nameof(In));

        /// <summary>
        /// ALL Keyword
        /// </summary>
        /// <returns></returns>
        [SqlSyntaxTmp]
        public static IAggregatePredicate All() => InvalitContext.Throw<IAggregatePredicate>(nameof(All));

        /// <summary>
        /// Distinct Keyword
        /// </summary>
        /// <returns></returns>
        [SqlSyntaxTmp]
        public static IAggregatePredicate Distinct() => InvalitContext.Throw<IAggregatePredicate>(nameof(All));

        /// <summary>
        /// ALL Keyword
        /// </summary>
        /// <typeparam name="T">Retunn type.</typeparam>
        /// <param name="sub">Sub query.</param>
        /// <returns>Sub query's selected value.</returns>
        [SqlSyntaxTmp]
        public static T All<T>(object sub) => InvalitContext.Throw<T>(nameof(All));

        /// <summary>
        /// EXISTS keyword.
        /// </summary>
        /// <param name="expression">Sub query.</param>
        /// <returns>Returns TRUE if there is at least one record returned by expression, FALSE otherwise.</returns>
        [SqlSyntaxTmp]
        public static bool Exists(ISqlExpression expression) => InvalitContext.Throw<bool>(nameof(Exists));

        /// <summary>
        /// EXISTS keyword.
        /// </summary>
        /// <param name="expression">Sub query.</param>
        /// <returns>Returns TRUE if there is at least one record returned by expression, FALSE otherwise.</returns>
        [SqlSyntaxTmp]
        public static bool Exists(IClauseChain expression) => InvalitContext.Throw<bool>(nameof(Exists));

        /// <summary>
        /// IS NULL keyword.
        /// </summary>
        /// <param name="target">Target of null check.</param>
        /// <returns>Returns TRUE if target is null.</returns>
        [SqlSyntaxTmp]
        public static bool IsNull(object target) => InvalitContext.Throw<bool>(nameof(IsNull));

        /// <summary>
        /// IS NOT NULL keyword.
        /// </summary>
        /// <param name="target">Target of not null check.</param>
        /// <returns>Returns TRUE if target is not null</returns>
        [SqlSyntaxTmp]
        public static bool IsNotNull(object target) => InvalitContext.Throw<bool>(nameof(IsNotNull));

        /// <summary>
        /// It's *.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="selected">The type you want to obtain with the SELECT clause. Usually you specify a table element.</param>
        /// <returns>*</returns>
        [SqlSyntaxTmp]
        public static IAsterisk<TSelected> Asterisk<TSelected>(TSelected selected) => InvalitContext.Throw<IAsterisk<TSelected>>(nameof(Asterisk));

        /// <summary>
        /// It's *.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <returns>*</returns>
        [SqlSyntaxTmp]
        public static IAsterisk<TSelected> Asterisk<TSelected>() => InvalitContext.Throw<IAsterisk<TSelected>>(nameof(Asterisk));

        /// <summary>
        /// It's *.
        /// </summary>
        /// <returns>*</returns>
        [SqlSyntaxTmp]
        public static IAsterisk Asterisk() => InvalitContext.Throw<IAsterisk>(nameof(Asterisk));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="target">target column.</param>
        [SqlSyntaxTmp]
        public static ISortedBy Asc(object target) => InvalitContext.Throw<ISortedBy>(nameof(Asc));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="target">target column.</param>
        [SqlSyntaxTmp]
        public static ISortedBy Desc(object target) => InvalitContext.Throw<ISortedBy>(nameof(Desc));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="count">cout.</param>
        [SqlSyntaxTmp]
        public static ITop Top(long count) => InvalitContext.Throw<ITop>(nameof(Top));
        
        /// <summary>
        /// ROWNUM BETWEEN keyword.
        /// </summary>
        [SqlSyntaxTmp]
        public static object RowNum() => InvalitContext.Throw<object>(nameof(RowNum));

        /// <summary>
        /// DUAL keyword.
        /// </summary>
        [SqlSyntaxTmp]
        public static object Dual() => InvalitContext.Throw<object>(nameof(Dual));

        /// <summary>
        /// CURREN_TDATE function.
        /// </summary>
        /// <returns>Date of executing SQL.</returns>
        [SqlSyntaxTmp]
        public static DateTime CurrentDate() => InvalitContext.Throw<DateTime>(nameof(CurrentDate));

        /// <summary>
        /// CURRENT_TIME function.
        /// </summary>
        /// <returns>Date of executing SQL.</returns>
        [SqlSyntaxTmp]
        public static TimeSpan CurrentTime() => InvalitContext.Throw<TimeSpan>(nameof(DateTimeOffset));

        /// <summary>
        /// CURRENT_TIMESTAMP function.
        /// </summary>
        /// <returns>Date and time of executing SQL.</returns>
        [SqlSyntaxTmp]
        public static DateTime CurrentTimeStamp() => InvalitContext.Throw<DateTime>(nameof(CurrentTimeStamp));

        #endregion

        #region SQL Functions
        /// <summary>
        /// SUM function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Total.</returns>
        [SqlSyntaxTmp]
        public static T Sum<T>(T column) => InvalitContext.Throw<T>(nameof(Sum));

        /// <summary>
        /// SUM function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Total.</returns>
        [SqlSyntaxTmp]
        public static T Sum<T>(IAggregatePredicate aggregatePredicate, T column) => InvalitContext.Throw<T>(nameof(Sum));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Count.</returns>
        [SqlSyntaxTmp]
        public static int Count(object column) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="asterisk">*</param>
        /// <returns>Count.</returns>
        [SqlSyntaxTmp]
        public static int Count(IAsterisk asterisk) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Count.</returns>
        [SqlSyntaxTmp]
        public static int Count(IAggregatePredicate aggregatePredicate, object column) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Count.</returns>
        [SqlSyntaxTmp]
        public static int Count(IAggregatePredicate aggregatePredicate, IAsterisk asterisk) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// AVG function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Average.</returns>
        [SqlSyntaxFunc]
        public static double Avg(object column) => InvalitContext.Throw<double>(nameof(Avg));

        /// <summary>
        /// MIN function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Minimum.</returns>
        [SqlSyntaxFunc]
        public static T Min<T>(T column) => InvalitContext.Throw<T>(nameof(Min));

        /// <summary>
        /// MAX function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Maximum.</returns>
        [SqlSyntaxFunc]
        public static T Max<T>(T column) => InvalitContext.Throw<T>(nameof(Max));

        /// <summary>
        /// ABS function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Absolute value.</returns>
        [SqlSyntaxFunc]
        public static T Abs<T>(T column) => InvalitContext.Throw<T>(nameof(Abs));

        /// <summary>
        /// MOD function.
        /// </summary>
        /// <typeparam name="T">Type represented by target</typeparam>
        /// <param name="target">Numeric expression to divide.</param>
        /// <param name="div">A numeric expression that divides the dividend.</param>
        /// <returns>Surplus.</returns>
        [SqlSyntaxFunc]
        public static T Mod<T>(T target, object div) => InvalitContext.Throw<T>(nameof(Mod));

        /// <summary>
        /// ROUND function.
        /// </summary>
        /// <typeparam name="T">Type represented by target.</typeparam>
        /// <param name="target">Numeric expression to round.</param>
        /// <param name="digit">Is the precision to which it is to be rounded.</param>
        /// <returns>Rounded result.</returns>
        [SqlSyntaxFunc]
        public static T Round<T>(T target, object digit) => InvalitContext.Throw<T>(nameof(Round));

        /// <summary>
        /// CONCAT function.
        /// </summary>
        /// <param name="targets">A string value to concatenate to the other values.</param>
        /// <returns>concatenated result.</returns>
        [SqlSyntaxFunc]
        public static string Concat(params object[] targets) => InvalitContext.Throw<string>(nameof(Concat));

        /// <summary>
        /// LENGTH function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>String length.</returns>
        [SqlSyntaxFunc]
        public static int Length(object target) => InvalitContext.Throw<int>(nameof(Length));

        /// <summary>
        /// LEN function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>String length.</returns>
        [SqlSyntaxFunc]
        public static int Len(object target) => InvalitContext.Throw<int>(nameof(Len));

        /// <summary>
        /// LOWER function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>Changed string.</returns>
        [SqlSyntaxFunc]
        public static string Lower(object target) => InvalitContext.Throw<string>(nameof(Lower));

        /// <summary>
        /// UPPER function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>Changed string.</returns>
        [SqlSyntaxFunc]
        public static string Upper(object target) => InvalitContext.Throw<string>(nameof(Upper));

        /// <summary>
        /// REPLACE function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <param name="src">source.</param>
        /// <param name="dst">destination.</param>
        /// <returns>Changed string.</returns>
        [SqlSyntaxFunc]
        public static string Replace(object target, object src, object dst) => InvalitContext.Throw<string>(nameof(Replace));

        /// <summary>
        /// SUBSTRING function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <param name="startIndex">Specify the starting position of the character string to be acquired.</param>
        /// <param name="length">Specify the length of the string to be retrieved.</param>
        /// <returns>Part of a text.</returns>
        [SqlSyntaxFunc]
        public static string Substring(object target, object startIndex, object length) => InvalitContext.Throw<string>(nameof(Substring));

        /// <summary>
        /// EXTRACT function.
        /// </summary>
        /// <param name="element">Part type.</param>
        /// <param name="src">The date data.</param>
        /// <returns>A part from the date data.</returns>
        [SqlSyntaxTmp]
        public static double Extract(DateTimeElement element, DateTime src) => InvalitContext.Throw<double>(nameof(Extract));

        /// <summary>
        /// DATEPART function.
        /// </summary>
        /// <param name="element">Part type.</param>
        /// <param name="src">The date data.</param>
        /// <returns>A part from the date data.</returns>
        [SqlSyntaxFunc]
        public static int DatePart(DateTimeElement element, DateTime src) => InvalitContext.Throw<int>(nameof(Extract));

        /// <summary>
        /// CAST function.
        /// </summary>
        /// <typeparam name="TDst">Type of destination.</typeparam>
        /// <param name="target"></param>
        /// <param name="destinationType">Type of destination.</param>
        /// <returns>Converted data.</returns>
        [SqlSyntaxTmp]
        public static TDst Cast<TDst>(object target, string destinationType) => InvalitContext.Throw<TDst>(nameof(Cast));

        /// <summary>
        /// COALESCE function.
        /// </summary>
        /// <typeparam name="T">Type of parameter</typeparam>
        /// <param name="parameter">Parameter.</param>
        /// <returns>The first non-null value in the parameter.</returns>
        [SqlSyntaxFunc]
        public static T Coalesce<T>(params T[] parameter) => InvalitContext.Throw<T>(nameof(Coalesce));

        /// <summary>
        /// NVL function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="expression1">expression.</param>
        /// <param name="expression2">expression.</param>
        /// <returns>expression1 or expression2.</returns>
        [SqlSyntaxFunc]
        public static T NVL<T>(T expression1, T expression2) => InvalitContext.Throw<T>(nameof(NVL));

        /// <summary>
        /// FIRST_VALUE function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [SqlSyntaxFunc]
        public static T First_Value<T>(T column) => InvalitContext.Throw<T>(nameof(First_Value));

        /// <summary>
        /// LAST_VALUE function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [SqlSyntaxFunc]
        public static T Last_Value<T>(T column) => InvalitContext.Throw<T>(nameof(Last_Value));

        /// <summary>
        /// RANK function.
        /// </summary>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [SqlSyntaxFunc]
        public static int Rank() => InvalitContext.Throw<int>(nameof(Rank));

        /// <summary>
        /// DENSE_RANK function.
        /// </summary>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [SqlSyntaxFunc]
        public static int Dense_Rank() => InvalitContext.Throw<int>(nameof(Dense_Rank));

        /// <summary>
        /// PERCENT_RANK function.
        /// </summary>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [SqlSyntaxFunc]
        public static double Percent_Rank() => InvalitContext.Throw<double>(nameof(Percent_Rank));

        /// <summary>
        /// CUME_DIST function.
        /// </summary>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [SqlSyntaxFunc]
        public static double Cume_Dist() => InvalitContext.Throw<double>(nameof(Cume_Dist));

        /// <summary>
        /// NTILE function.
        /// </summary>
        /// <param name="groupCount">The number of ranking groups.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [SqlSyntaxFunc]
        public static int Ntile(object groupCount) => InvalitContext.Throw<int>(nameof(Ntile));

        /// <summary>
        /// NTH_VALUE function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">Specify the number of lines associated with the first line of the window that returns the expression.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [SqlSyntaxFunc]
        public static int Nth_Value(object column, object offset) => InvalitContext.Throw<int>(nameof(Nth_Value));

        //TODO Window関数で Row_Numerってのがあるらしい

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [SqlSyntaxFunc]
        public static T Lag<T>(T column) => InvalitContext.Throw<T>(nameof(Lag));

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">An optional parameter that specifies the number of lines before the current line that returns a value.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [SqlSyntaxFunc]
        public static T Lag<T>(T column, object offset) => InvalitContext.Throw<T>(nameof(Lag));

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">An optional parameter that specifies the number of lines before the current line that returns a value.</param>
        /// <param name="default">The value returned if the value specified by offset is NULL.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [SqlSyntaxFunc]
        public static T Lag<T>(T column, object offset, T @default) => InvalitContext.Throw<T>(nameof(Lag));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="preceding">Preceding row count.</param>
        [SqlSyntaxTmp]
        public static IRows Rows(int preceding) => InvalitContext.Throw<IRows>(nameof(Rows));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="preceding">Preceding row count.</param>
        /// <param name="following">Following row count.</param>
        [SqlSyntaxTmp]
        public static IRows Rows(int preceding, int following) => InvalitContext.Throw<IRows>(nameof(Rows));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="columns">Specify column or expression.</param>
        [SqlSyntaxTmp]
        public static IPartitionBy PartitionBy(params object[] columns) => InvalitContext.Throw<IPartitionBy>(nameof(PartitionBy));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [ForcedMethodGroup, SqlSyntaxTmp]
        public static T Over<T>(this T before, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [ForcedMethodGroup, SqlSyntaxTmp]
        public static T Over<T>(this T before, IPartitionBy partitionBy, IOrderBy orderBy) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [ForcedMethodGroup, SqlSyntaxTmp]
        public static T Over<T>(this T before, IPartitionBy partitionBy, IRows rows) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">Getting row order.</param>
        /// <returns>It is the result of Window function.</returns>
        [ForcedMethodGroup, SqlSyntaxTmp]
        public static T Over<T>(this T before, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [ForcedMethodGroup, SqlSyntaxTmp]
        public static T Over<T>(this T before, IPartitionBy partitionBy) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [ForcedMethodGroup, SqlSyntaxTmp]
        public static T Over<T>(this T before, IOrderBy orderBy) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="rows">Getting row order.</param>
        /// <returns>It is the result of Window function.</returns>
        [ForcedMethodGroup, SqlSyntaxTmp]
        public static T Over<T>(this T before, IRows rows) => InvalitContext.Throw<T>(nameof(Over));
        #endregion
    }
    class SqlSyntaxTmpAttribute : SqlSyntaxMethodAttribute
    {


        static ExpressionElement ConvertTop(IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return LineSpace("TOP", args[0].Customize(new CustomizeParameterToObject()));
        }

        class CurrentDateTimeExpressionElement : ExpressionElement
        {
            string _front = string.Empty;
            string _back = string.Empty;
            string _core;

            internal CurrentDateTimeExpressionElement(string core)
            {
                _core = core;
            }

            CurrentDateTimeExpressionElement(string core, string front, string back)
            {
                _core = core;
                _front = front;
                _back = back;
            }

            public override bool IsSingleLine(ExpressionConvertingContext context) => true;

            public override bool IsEmpty => false;

            public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
                => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + "CURRENT" + context.Option.CurrentDateTimeSeparator + _core + _back;

            public override ExpressionElement ConcatAround(string front, string back)
                => new CurrentDateTimeExpressionElement(_core, front + _front, _back + back);

            public override ExpressionElement ConcatToFront(string front)
                => new CurrentDateTimeExpressionElement(_core, front + _front, _back);

            public override ExpressionElement ConcatToBack(string back)
                => new CurrentDateTimeExpressionElement(_core, _front, _back + back);

            public override ExpressionElement Customize(ISqlTextCustomizer customizer)
                => customizer.Custom(this);
        }
        
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var methods = new[] { method };
            switch (method.Method.Name)
            {
                case nameof(Keywords.Dual):
                case nameof(Keywords.RowNum):
                    return method.Method.Name.ToUpper();
                case nameof(Keywords.CurrentDate): return new CurrentDateTimeExpressionElement("DATE");
                case nameof(Keywords.CurrentTime): return new CurrentDateTimeExpressionElement("TIME");
                case nameof(Keywords.CurrentTimeStamp): return new CurrentDateTimeExpressionElement("TIMESTAMP");
                case nameof(Keywords.With):
                    return FromClause.ConvertWith(converter, methods);
                case nameof(Keywords.Recursive):
                    return SelectClause.ConvertRecursive(converter, methods);
                case nameof(Keywords.Asc):
                    return LineSpace(converter.Convert(methods[0].Arguments[0]), "ASC");
                case nameof(Keywords.Desc):
                    return LineSpace(converter.Convert(methods[0].Arguments[0]), "DESC");
                case nameof(Keywords.Top):
                    return ConvertTop(converter, methods);
                case nameof(Keywords.Select):
                    return SelectClause.Convert(converter, methods);
                case nameof(Keywords.Case):
                case nameof(Keywords.When):
                case nameof(Keywords.Then):
                case nameof(Keywords.Else):
                case nameof(Keywords.End):
                    return CaseClause.Convert(converter, methods);
                case nameof(Keywords.Delete):
                    return DeleteClause.Convert(converter, methods);
                case nameof(Keywords.From):
                    return FromClause.ConvertFrom(converter, methods);
                case nameof(Keywords.Join):
                    return FromClause.ConvertJoin(converter, methods);
                case nameof(Keywords.LeftJoin):
                    return FromClause.ConvertLeftJoin(converter, methods);
                case nameof(Keywords.RightJoin):
                    return FromClause.ConvertRightJoin(converter, methods);
                case nameof(Keywords.FullJoin):
                    return FromClause.ConvertFullJoin(converter, methods);
                case nameof(Keywords.CrossJoin):
                    return FromClause.ConvertCrossJoin(converter, methods);
                case nameof(Keywords.GroupBy):
                    return GroupByClause.ConvertGroupBy(converter, methods);
                case nameof(Keywords.GroupByRollup):
                    return GroupByClause.ConvertGroupByRollup(converter, methods);
                case nameof(Keywords.GroupByWithRollup):
                    return GroupByClause.ConvertGroupByWithRollup(converter, methods);
                case nameof(Keywords.GroupByCube):
                    return GroupByClause.ConvertGroupByCube(converter, methods);
                case nameof(Keywords.GroupByGroupingSets):
                    return GroupByClause.ConvertGroupByGroupingSets(converter, methods);
                case nameof(Keywords.Having):
                    return HavingClause.Convert(converter, methods);
                case nameof(Keywords.InsertInto):
                case nameof(Keywords.Values):
                    return InsertIntoClause.Convert(converter, methods);
                case nameof(Keywords.OrderBy):
                    return OrderByWordsClause.Convert(converter, methods);
                case nameof(Keywords.OffsetRows):
                    return OffsetRowsClause.Convert(converter, methods);
                case nameof(Keywords.FetchNextRowsOnly):
                    return FetchNextRowsOnlyClause.Convert(converter, methods);
                case nameof(Keywords.Limit):
                    return LimitClause.Convert(converter, methods);
                case nameof(Keywords.Offset):
                    return OffsetClause.Convert(converter, methods);
                case nameof(Keywords.Update):
                case nameof(Keywords.Set):
                    return UpdateClause.Convert(converter, methods);
                case nameof(Keywords.Where):
                    return WhereClause.Convert(converter, methods);
                case nameof(Keywords.In):
                    return ConditionKeyWords.ConvertIn(converter, methods);
                case nameof(Keywords.All):
                    if (method.Arguments.Count == 0) return method.Method.Name.ToUpper();
                    return ConditionKeyWords.ConvertAll(converter, methods);
                case nameof(Keywords.Distinct):
                    return method.Method.Name.ToUpper();
                case nameof(Keywords.Like):
                    return ConditionKeyWords.ConvertLike(converter, methods);
                case nameof(Keywords.Between):
                    return ConditionKeyWords.ConvertBetween(converter, methods);
                case nameof(Keywords.Exists):
                    return ConditionKeyWords.ConvertExists(converter, methods);
                case nameof(Keywords.Union):
                case nameof(Keywords.Intersect):
                case nameof(Keywords.Except):
                case nameof(Keywords.Minus):
                    return SetOperation.Convert(converter, methods);
                case nameof(Keywords.IsNull):
                    return NullCheck.ConvertIsNull(converter, methods);
                case nameof(Keywords.IsNotNull):
                    return NullCheck.ConvertIsNotNull(converter, methods);
                case nameof(Keywords.Asterisk):
                    return "*";

                //Functions
                case nameof(Keywords.Sum):
                case nameof(Keywords.Count):
                    {
                        var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
                        return (method.Arguments.Count == 2) ? FuncSpace(method.Method.Name.ToUpper(), args):
                                                                    Func(method.Method.Name.ToUpper(), args);
                    }
                case nameof(Keywords.Extract):
                    {
                        var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
                        return FuncSpace(method.Method.Name.ToUpper(), args[0], "FROM", args[1]);
                    }
                case nameof(Keywords.Cast):
                    {
                        var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
                        return FuncSpace("CAST", args[0], "AS", args[1].Customize(new CustomizeParameterToObject()));
                    }
                case nameof(Keywords.Over):
                    {
                        var v = new VText();
                        var overMethod = method;
                        v.Add(overMethod.Method.Name.ToUpper() + "(");
                        v.AddRange(1, overMethod.Arguments.Skip(1).
                            Where(e => !(e is ConstantExpression)). //Skip null.
                            Select(e => converter.Convert(e)).ToArray());
                        return v.ConcatToBack(")");
                    }
                case nameof(Keywords.Rows):
                    return Window.ConvertRows(converter, new[] { method });
                case nameof(Keywords.PartitionBy):
                    return Window.ConvertPartitionBy(converter, new[] { method });
            }
            throw new NotSupportedException();
        }
    }
}
