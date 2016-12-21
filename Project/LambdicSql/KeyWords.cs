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


    //TODO ファイル分割
    /// <summary>
    /// SQL Keywords.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// Use[using static LambdicSql.Keywords;], you can use to write natural SQL.
    /// </summary>
    [SqlSyntax]
    public static class Keywords
    {
        #region Keywords
        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSelected>(TSelected selected) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSrcSelected, TSelected>(this IClauseChain<TSrcSelected> before, TSelected selected) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSelected>(ITop top, TSelected selected) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSrcSelected, TSelected>(this IClauseChain<TSrcSelected> before, ITop top, TSelected selected) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Select(IAsterisk asterisk) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Select<TSrcSelected>(this IClauseChain<TSrcSelected> before, IAsterisk asterisk) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSelected>(IAsterisk<TSelected> asterisk) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSrcSelected, TSelected>(this IClauseChain<TSrcSelected> before, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Select(ITop top, IAsterisk asterisk) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Select<TSrcSelected>(this IClauseChain<TSrcSelected> before, ITop top, IAsterisk asterisk) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSelected>(ITop top, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSrcSelected, TSelected>(this IClauseChain<TSrcSelected> before, ITop top, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSelected>(AggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSrcSelected, TSelected>(this IClauseChain<TSrcSelected> before, AggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSelected>(AggregatePredicate predicate, ITop top, TSelected selected) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSrcSelected, TSelected>(this IClauseChain<TSrcSelected> before, AggregatePredicate predicate, ITop top, TSelected selected) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Select(AggregatePredicate predicate, IAsterisk asterisk) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Select<TSrcSelected>(this IClauseChain<TSrcSelected> before, AggregatePredicate predicate, IAsterisk asterisk) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSelected>(AggregatePredicate predicate, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSrcSelected, TSelected>(this IClauseChain<TSrcSelected> before, AggregatePredicate predicate, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Select(AggregatePredicate predicate, ITop top, IAsterisk asterisk) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Select<TSrcSelected>(this IClauseChain<TSrcSelected> before, AggregatePredicate predicate, ITop top, IAsterisk asterisk) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSelected>(AggregatePredicate predicate, ITop top, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Select<TSrcSelected, TSelected>(this IClauseChain<TSrcSelected> before, AggregatePredicate predicate, ITop top, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// Return value of CASE clause.
        /// </summary>
        public interface ICaseAfter : IMethodChain { }

        /// <summary>
        /// Return value of WHEN clause.
        /// </summary>
        public interface IWhenAfter : IMethodChain { }

        /// <summary>
        /// Return value of WHEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        public interface IWhenAfter<T> : IMethodChain { }

        /// <summary>
        /// Return value of THEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        public interface IThenAfter<T> : IMethodChain { }

        /// <summary>
        /// Return value of ELSE clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        public interface IElseAfter<T> : IMethodChain { }

        /// <summary>
        /// CASE clause.
        /// </summary>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [MethodGroup(nameof(Case))]
        public static ICaseAfter Case() => InvalitContext.Throw<ICaseAfter>(nameof(Case));

        /// <summary>
        /// CASE clause.
        /// </summary>
        /// <param name="target">It's target of CASE branch.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [MethodGroup(nameof(Case))]
        public static ICaseAfter Case(object target) => InvalitContext.Throw<ICaseAfter>(nameof(Case));

        /// <summary>
        /// WHEN clause.
        /// </summary>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="expression">It is a conditional expression of the WHEN clause.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [MethodGroup(nameof(Case))]
        public static IWhenAfter When(this ICaseAfter before, object expression) => InvalitContext.Throw<IWhenAfter>(nameof(When));

        /// <summary>
        /// WHEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="expression">It is a conditional expression of the WHEN clause.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [MethodGroup(nameof(Case))]
        public static IWhenAfter<T> When<T>(this IThenAfter<T> before, object expression) => InvalitContext.Throw<IWhenAfter<T>>(nameof(When));

        /// <summary>
        /// THEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the THEN clause is valid.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [MethodGroup(nameof(Case))]
        public static IThenAfter<T> Then<T>(this IWhenAfter before, T result) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));

        /// <summary>
        /// THEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the THEN clause is valid.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [MethodGroup(nameof(Case))]
        public static IThenAfter<T> Then<T>(this IWhenAfter<T> before, T result) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));

        /// <summary>
        /// ELSE clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the ELSE clause is valid.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [MethodGroup(nameof(Case))]
        public static IElseAfter<T> Else<T>(this IThenAfter<T> before, T result) => InvalitContext.Throw<IElseAfter<T>>(nameof(Then));

        /// <summary>
        /// END clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <returns>It is the result of CASE expression.</returns>
        [MethodGroup(nameof(Case))]
        public static T End<T>(this IThenAfter<T> before) => InvalitContext.Throw<T>(nameof(End));

        /// <summary>
        /// END clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <returns>It is the result of CASE expression.</returns>
        [MethodGroup(nameof(Case))]
        public static T End<T>(this IElseAfter<T> before) => InvalitContext.Throw<T>(nameof(End));

        /// <summary>
        /// FROM clause.
        /// </summary>
        /// <param name="expressions">Table or subquery.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> From(params object[] expressions) => InvalitContext.Throw<IClauseChain<Non>>(nameof(From));

        /// <summary>
        /// FROM clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="tables">Table or subquery.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> From<TSelected>(this IClauseChain<TSelected> before, params object[] tables) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(From));

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Join(object table, bool condition) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Join));

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Join<TSelected>(this IClauseChain<TSelected> before, object table, bool condition) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Join));

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of LEFT JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> LeftJoin(object table, bool condition) => InvalitContext.Throw<IClauseChain<Non>>(nameof(LeftJoin));

        /// <summary>
        /// LEFT JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of LEFT JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> LeftJoin<TSelected>(this IClauseChain<TSelected> before, object table, bool condition) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(LeftJoin));

        /// <summary>
        /// RIGHT JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> RightJoin(object table, bool condition) => InvalitContext.Throw<IClauseChain<Non>>(nameof(RightJoin));

        /// <summary>
        /// RIGHT JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> RightJoin<TSelected>(this IClauseChain<TSelected> before, object table, bool condition) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(RightJoin));

        /// <summary>
        /// CROSS JOIN clause.
        /// </summary>
        /// <param name="expression">Table or subquery.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> CrossJoin(object expression) => InvalitContext.Throw<IClauseChain<Non>>(nameof(CrossJoin));

        /// <summary>
        /// CROSS JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> CrossJoin<TSelected>(this IClauseChain<TSelected> before, object table) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(CrossJoin));

        /// <summary>
        /// WHERE clause.
        /// </summary>
        /// <param name="condition">It is a conditional expression of WHERE.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Where(bool condition) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Where));

        /// <summary>
        /// WHERE clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="condition">It is a conditional expression of WHERE.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Where<TSelected>(this IClauseChain<TSelected> before, bool condition) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Where));

        /// <summary>
        /// GROUP BY clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> GroupBy(params object[] columns) => InvalitContext.Throw<IClauseChain<Non>>(nameof(GroupBy));

        /// <summary>
        /// GROUP BY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> GroupBy<TSelected>(this IClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(GroupBy));

        /// <summary>
        /// GROUP BY ROLLUP clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> GroupByRollup(params object[] columns) => InvalitContext.Throw<IClauseChain<Non>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY ROLLUP clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> GroupByRollup<TSelected>(this IClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY columns WITH ROLLUP clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> GroupByWithRollup(params object[] columns) => InvalitContext.Throw<IClauseChain<Non>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY columns WITH ROLLUP clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> GroupByWithRollup<TSelected>(this IClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY CUBE clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> GroupByCube(params object[] columns) => InvalitContext.Throw<IClauseChain<Non>>(nameof(GroupByCube));

        /// <summary>
        /// GROUP BY CUBE clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> GroupByCube<TSelected>(this IClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(GroupByCube));

        /// <summary>
        /// GROUP BY GROUPING SETS clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> GroupByGroupingSets(params object[] columns) => InvalitContext.Throw<IClauseChain<Non>>(nameof(GroupByGroupingSets));

        /// <summary>
        /// GROUP BY GROUPING SETS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> GroupByGroupingSets<TSelected>(this IClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(GroupByGroupingSets));

        /// <summary>
        /// HAVING clause.
        /// </summary>
        /// <param name="condition">It is a conditional expression of HAVING.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Having(bool condition) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Having));

        /// <summary>
        /// HAVING clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="condition">It is a conditional expression of HAVING.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Having<TSelected>(this IClauseChain<TSelected> before, bool condition) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Having));

        /// <summary>
        /// ORDER BY clause.
        /// </summary>
        /// <param name="elements">Specify column and sort order. Asc(column) or Desc(column).</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IOrderBy OrderBy(params ISortedBy[] elements) => InvalitContext.Throw<IOrderBy>(nameof(OrderBy));

        /// <summary>
        /// ORDER BY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="elements">Specify column and sort order. Asc(column) or Desc(column).</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> OrderBy<TSelected>(this IClauseChain<TSelected> before, params ISortedBy[] elements) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Limit(object count) => InvalitContext.Throw<IClauseChain<Non>>(nameof(OrderBy));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Limit<TSelected>(this IClauseChain<TSelected> before, object count) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <param name="offset">Start position.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Limit(object offset, object count) => InvalitContext.Throw<IClauseChain<Non>>(nameof(OrderBy));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="offset">Start position.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Limit<TSelected>(this IClauseChain<TSelected> before, object offset, object count) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// OFFSET clause.
        /// </summary>
        /// <param name="offset">Start position.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Offset(object offset) => InvalitContext.Throw<IClauseChain<Non>>(nameof(OrderBy));

        /// <summary>
        /// OFFSET clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="offset">Start position.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Offset<TSelected>(this IClauseChain<TSelected> before, object offset) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// OFFSET count ROWS clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> OffsetRows(object count) => InvalitContext.Throw<IClauseChain<Non>>(nameof(OrderBy));

        /// <summary>
        /// OFFSET count ROWS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> OffsetRows<TSelected>(this IClauseChain<TSelected> before, object count) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// FETCH NEXT count ROWS ONLY clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> FetchNextRowsOnly(object count) => InvalitContext.Throw<IClauseChain<Non>>(nameof(OrderBy));

        /// <summary>
        /// FETCH NEXT count ROWS ONLY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> FetchNextRowsOnly<TSelected>(this IClauseChain<TSelected> before, object count) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Union() => InvalitContext.Throw<IClauseChain<Non>>(nameof(Union));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Union<TSelected>(this IClauseChain<TSelected> before) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Union));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Union(bool isAll) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Union));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Union<TSelected>(this IClauseChain<TSelected> before, bool isAll) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Union));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Intersect() => InvalitContext.Throw<IClauseChain<Non>>(nameof(Intersect));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Intersect<TSelected>(this IClauseChain<TSelected> before) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Intersect));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Intersect(bool isAll) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Intersect));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Intersect<TSelected>(this IClauseChain<TSelected> before, bool isAll) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Intersect));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Except() => InvalitContext.Throw<IClauseChain<Non>>(nameof(Except));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Except<TSelected>(this IClauseChain<TSelected> before) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Except));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Except(bool isAll) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Except));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Except<TSelected>(this IClauseChain<TSelected> before, bool isAll) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Except));

        /// <summary>
        /// MINUS clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Minus() => InvalitContext.Throw<IClauseChain<Non>>(nameof(Minus));

        /// <summary>
        /// MINUS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<TSelected> Minus<TSelected>(this IClauseChain<TSelected> before) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Minus));

        /// <summary>
        /// UPDATE clause.
        /// </summary>
        /// <param name="table">Table for UPDATE.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodGroup(nameof(Update))]
        public static IClauseChain<Non> Update(object table) => InvalitContext.Throw<IClauseChain<Non>>(nameof(Update));

        /// <summary>
        /// SET clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="assigns">Assignment in the SET clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodGroup(nameof(Update))]
        public static IClauseChain<TSelected> Set<TSelected>(this IClauseChain<TSelected> before, params Assign[] assigns) => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Set));

        /// <summary>
        /// DELETE clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IClauseChain<Non> Delete() => InvalitContext.Throw<IClauseChain<Non>>(nameof(Delete));

        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <param name="table">Table for INSERT.</param>
        /// <param name="columns">It is a column that performs INSERT.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodGroup(nameof(InsertInto))]
        public static IClauseChain<Non> InsertInto(object table, params object[] columns) => InvalitContext.Throw<IClauseChain<Non>>(nameof(InsertInto));

        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="values">It is the value to be Inserted.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodGroup(nameof(InsertInto))]
        public static IClauseChain<TSelected> Values<TSelected>(this IClauseChain<TSelected> before, params object[] values)
             => InvalitContext.Throw<IClauseChain<TSelected>>(nameof(Values));

        /// <summary>
        /// LIKE keyword.
        /// </summary>
        /// <param name="target">Target text.</param>
        /// <param name="pattern">Text that represents pattern matching.</param>
        /// <returns>If target matches the specified pattern, LIKE returns TRUE.</returns>
        public static bool Like(object target, object pattern) => InvalitContext.Throw<bool>(nameof(Like));

        /// <summary>
        /// BETWEEN keyword.
        /// </summary>
        /// <param name="target">Target of range check.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maxmum value.</param>
        /// <returns>Returns TRUE if target is included in the range of min and max.</returns>
        public static bool Between(object target, object min, object max) => InvalitContext.Throw<bool>(nameof(Between));

        /// <summary>
        /// IN keyword.
        /// </summary>
        /// <param name="target">Target of IN check.</param>
        /// <param name="canditates">Canditates.</param>
        /// <returns>Returns TRUE if target is included in the canditates represented by expression.</returns>
        public static bool In(object target, params object[] canditates) => InvalitContext.Throw<bool>(nameof(In));

        /// <summary>
        /// IN keyword.
        /// </summary>
        /// <param name="target">Target of IN check.</param>
        /// <param name="canditates">Candidates expected for target.</param>
        /// <returns>Returns TRUE if target is included in the candidate represented by expression.</returns>
        public static bool In(object target, ISqlExpressionBase canditates) => InvalitContext.Throw<bool>(nameof(In));

        /// <summary>
        /// IN keyword.
        /// </summary>
        /// <param name="target">Target of IN check.</param>
        /// <param name="canditates">Table or subquery.</param>
        /// <returns>Returns TRUE if target is included in the candidate represented by expression.</returns>
        public static bool In(object target, IClauseChain canditates) => InvalitContext.Throw<bool>(nameof(In));

        /// <summary>
        /// EXISTS keyword.
        /// </summary>
        /// <param name="expression">Sub query.</param>
        /// <returns>Returns TRUE if there is at least one record returned by expression, FALSE otherwise.</returns>
        public static bool Exists(ISqlExpressionBase expression) => InvalitContext.Throw<bool>(nameof(Exists));

        /// <summary>
        /// EXISTS keyword.
        /// </summary>
        /// <param name="expression">Sub query.</param>
        /// <returns>Returns TRUE if there is at least one record returned by expression, FALSE otherwise.</returns>
        public static bool Exists(IClauseChain expression) => InvalitContext.Throw<bool>(nameof(Exists));

        /// <summary>
        /// IS NULL keyword.
        /// </summary>
        /// <param name="target">Target of null check.</param>
        /// <returns>Returns TRUE if target is null.</returns>
        public static bool IsNull(object target) => InvalitContext.Throw<bool>(nameof(IsNull));

        /// <summary>
        /// IS NOT NULL keyword.
        /// </summary>
        /// <param name="target">Target of not null check.</param>
        /// <returns>Returns TRUE if target is not null</returns>
        public static bool IsNotNull(object target) => InvalitContext.Throw<bool>(nameof(IsNotNull));

        /// <summary>
        /// ROWNUM BETWEEN keyword.
        /// </summary>
        public static object RowNum => InvalitContext.Throw<object>(nameof(RowNum));

        /// <summary>
        /// DUAL keyword.
        /// </summary>
        public static object Dual => InvalitContext.Throw<object>(nameof(Dual));

        /// <summary>
        /// SYSIBM keyword.
        /// </summary>
        public static SysibmType Sysibm => InvalitContext.Throw<SysibmType>(nameof(Sysibm));

        /// <summary>
        /// CURREN_TDATE function.
        /// </summary>
        /// <returns>Date of executing SQL.</returns>
        public static DateTime Current_Date => InvalitContext.Throw<DateTime>(nameof(Current_Date));

        /// <summary>
        /// CURRENT_TIME function.
        /// </summary>
        /// <returns>Date of executing SQL.</returns>
        public static TimeSpan Current_Time => InvalitContext.Throw<TimeSpan>(nameof(DateTimeOffset));

        /// <summary>
        /// CURRENT_TIMESTAMP function.
        /// </summary>
        /// <returns>Date and time of executing SQL.</returns>
        public static DateTime Current_TimeStamp => InvalitContext.Throw<DateTime>(nameof(Current_TimeStamp));

        //TODO いやーやっぱり、オプションで何とかしようかなー
        //他の関数見てからにするかー
        //-----------------------------------------------------------------------------------
        /// <summary>
        /// CURRENT DATE function.
        /// Get date and time of executing SQL.
        /// </summary>
        /// <returns>Date and time of executing SQL.</returns>
        public static DateTime CurrentSpaceDate => InvalitContext.Throw<DateTime>(nameof(CurrentSpaceDate));

        /// <summary>
        /// CURRENT TIME function.
        /// Get date and time of executing SQL.
        /// </summary>
        /// <returns>Date and time of executing SQL.</returns>
        public static TimeSpan CurrentSpaceTime => InvalitContext.Throw<TimeSpan>(nameof(CurrentSpaceTime));

        /// <summary>
        /// CURRENT TIMESTAMP function.
        /// Get date and time of executing SQL.
        /// </summary>
        /// <returns>Date and time of executing SQL.</returns>
        public static DateTime CurrentSpaceTimeStamp => InvalitContext.Throw<DateTime>(nameof(CurrentSpaceTimeStamp));
        //----------------------------------------------------------------------------------

        /// <summary>
        /// It's *.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="selected">The type you want to obtain with the SELECT clause. Usually you specify a table element.</param>
        /// <returns>*</returns>
        public static IAsterisk<TSelected> Asterisk<TSelected>(TSelected selected) => InvalitContext.Throw<IAsterisk<TSelected>>(nameof(Asterisk));

        /// <summary>
        /// It's *.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <returns>*</returns>
        public static IAsterisk<TSelected> Asterisk<TSelected>() => InvalitContext.Throw<IAsterisk<TSelected>>(nameof(Asterisk));

        /// <summary>
        /// It's *.
        /// </summary>
        /// <returns>*</returns>
        public static IAsterisk Asterisk() => InvalitContext.Throw<IAsterisk>(nameof(Asterisk));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="target">target column.</param>
        public static ISortedBy Asc(object target) => InvalitContext.Throw<ISortedBy>(nameof(Asc));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="target">target column.</param>
        public static ISortedBy Desc(object target) => InvalitContext.Throw<ISortedBy>(nameof(Desc));


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="count">cout.</param>
        public static ITop Top(long count) => InvalitContext.Throw<ITop>(nameof(Top));
        #endregion

        #region SQL Functions
        /// <summary>
        /// SUM function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Total.</returns>
        public static T Sum<T>(T column) => InvalitContext.Throw<T>(nameof(Sum));

        /// <summary>
        /// SUM function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Total.</returns>
        public static T Sum<T>(AggregatePredicate aggregatePredicate, T column) => InvalitContext.Throw<T>(nameof(Sum));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Count.</returns>
        public static int Count(object column) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="asterisk">*</param>
        /// <returns>Count.</returns>
        public static int Count(IAsterisk asterisk) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Count.</returns>
        public static int Count(AggregatePredicate aggregatePredicate, object column) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Count.</returns>
        public static int Count(AggregatePredicate aggregatePredicate, IAsterisk asterisk) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// AVG function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Average.</returns>
        public static double Avg(object column) => InvalitContext.Throw<double>(nameof(Avg));

        /// <summary>
        /// MIN function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Minimum.</returns>
        public static T Min<T>(T column) => InvalitContext.Throw<T>(nameof(Min));

        /// <summary>
        /// MAX function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Maximum.</returns>
        public static T Max<T>(T column) => InvalitContext.Throw<T>(nameof(Max));

        /// <summary>
        /// ABS function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Absolute value.</returns>
        public static T Abs<T>(T column) => InvalitContext.Throw<T>(nameof(Abs));

        /// <summary>
        /// MOD function.
        /// </summary>
        /// <typeparam name="T">Type represented by target</typeparam>
        /// <param name="target">Numeric expression to divide.</param>
        /// <param name="div">A numeric expression that divides the dividend.</param>
        /// <returns>Surplus.</returns>
        public static T Mod<T>(T target, object div) => InvalitContext.Throw<T>(nameof(Mod));

        /// <summary>
        /// ROUND function.
        /// </summary>
        /// <typeparam name="T">Type represented by target.</typeparam>
        /// <param name="target">Numeric expression to round.</param>
        /// <param name="digit">Is the precision to which it is to be rounded.</param>
        /// <returns>Rounded result.</returns>
        public static T Round<T>(T target, object digit) => InvalitContext.Throw<T>(nameof(Round));

        /// <summary>
        /// CONCAT function.
        /// </summary>
        /// <param name="targets">A string value to concatenate to the other values.</param>
        /// <returns>concatenated result.</returns>
        public static string Concat(params object[] targets) => InvalitContext.Throw<string>(nameof(Concat));

        /// <summary>
        /// LENGTH function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>String length.</returns>
        public static int Length(object target) => InvalitContext.Throw<int>(nameof(Length));

        /// <summary>
        /// LEN function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>String length.</returns>
        public static int Len(object target) => InvalitContext.Throw<int>(nameof(Len));

        /// <summary>
        /// LOWER function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>Changed string.</returns>
        public static string Lower(object target) => InvalitContext.Throw<string>(nameof(Lower));

        /// <summary>
        /// UPPER function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>Changed string.</returns>
        public static string Upper(object target) => InvalitContext.Throw<string>(nameof(Upper));

        /// <summary>
        /// REPLACE function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <param name="src">source.</param>
        /// <param name="dst">destination.</param>
        /// <returns>Changed string.</returns>
        public static string Replace(object target, object src, object dst) => InvalitContext.Throw<string>(nameof(Replace));

        /// <summary>
        /// SUBSTRING function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <param name="startIndex">Specify the starting position of the character string to be acquired.</param>
        /// <param name="length">Specify the length of the string to be retrieved.</param>
        /// <returns>Part of a text.</returns>
        public static string Substring(object target, object startIndex, object length) => InvalitContext.Throw<string>(nameof(Substring));

        /// <summary>
        /// EXTRACT function.
        /// </summary>
        /// <param name="element">Part type.</param>
        /// <param name="src">The date data.</param>
        /// <returns>A part from the date data.</returns>
        public static double Extract(DateTimeElement element, DateTime src) => InvalitContext.Throw<double>(nameof(Extract));

        /// <summary>
        /// DATEPART function.
        /// </summary>
        /// <param name="element">Part type.</param>
        /// <param name="src">The date data.</param>
        /// <returns>A part from the date data.</returns>
        public static int DatePart(DateTimeElement element, DateTime src) => InvalitContext.Throw<int>(nameof(Extract));

        /// <summary>
        /// CAST function.
        /// </summary>
        /// <typeparam name="TDst">Type of destination.</typeparam>
        /// <param name="target"></param>
        /// <param name="destinationType">Type of destination.</param>
        /// <returns>Converted data.</returns>
        public static TDst Cast<TDst>(object target, string destinationType) => InvalitContext.Throw<TDst>(nameof(Cast));

        /// <summary>
        /// COALESCE function.
        /// </summary>
        /// <typeparam name="T">Type of parameter</typeparam>
        /// <param name="parameter">Parameter.</param>
        /// <returns>The first non-null value in the parameter.</returns>
        public static T Coalesce<T>(params T[] parameter) => InvalitContext.Throw<T>(nameof(Coalesce));

        /// <summary>
        /// NVL function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="expression1">expression.</param>
        /// <param name="expression2">expression.</param>
        /// <returns>expression1 or expression2.</returns>
        public static T NVL<T>(T expression1, T expression2) => InvalitContext.Throw<T>(nameof(NVL));
        #endregion

        #region Window Functions
        /// <summary>
        /// FIRST_VALUE function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        public static T First_Value<T>(T column) => InvalitContext.Throw<T>(nameof(First_Value));

        /// <summary>
        /// LAST_VALUE function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        public static T Last_Value<T>(T column) => InvalitContext.Throw<T>(nameof(Last_Value));

        /// <summary>
        /// RANK function.
        /// </summary>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        public static int Rank() => InvalitContext.Throw<int>(nameof(Rank));

        /// <summary>
        /// DENSE_RANK function.
        /// </summary>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        public static int Dense_Rank() => InvalitContext.Throw<int>(nameof(Dense_Rank));

        /// <summary>
        /// PERCENT_RANK function.
        /// </summary>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        public static double Percent_Rank() => InvalitContext.Throw<double>(nameof(Percent_Rank));

        /// <summary>
        /// CUME_DIST function.
        /// </summary>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        public static double Cume_Dist() => InvalitContext.Throw<double>(nameof(Cume_Dist));

        /// <summary>
        /// NTILE function.
        /// </summary>
        /// <param name="groupCount">The number of ranking groups.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        public static int Ntile(object groupCount) => InvalitContext.Throw<int>(nameof(Ntile));

        /// <summary>
        /// NTH_VALUE function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">Specify the number of lines associated with the first line of the window that returns the expression.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        public static int Nth_Value(object column, object offset) => InvalitContext.Throw<int>(nameof(Nth_Value));

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        public static T Lag<T>(T column) => InvalitContext.Throw<T>(nameof(Lag));

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">An optional parameter that specifies the number of lines before the current line that returns a value.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        public static T Lag<T>(T column, object offset) => InvalitContext.Throw<T>(nameof(Lag));

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">An optional parameter that specifies the number of lines before the current line that returns a value.</param>
        /// <param name="default">The value returned if the value specified by offset is NULL.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        public static T Lag<T>(T column, object offset, T @default) => InvalitContext.Throw<T>(nameof(Lag));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="preceding">Preceding row count.</param>
        public static IRows Rows(int preceding) => InvalitContext.Throw<IRows>(nameof(Rows));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="preceding">Preceding row count.</param>
        /// <param name="following">Following row count.</param>
        public static IRows Rows(int preceding, int following) => InvalitContext.Throw<IRows>(nameof(Rows));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="columns">Specify column or expression.</param>
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
        [ForcedMethodGroup]
        public static T Over<T>(this T before, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [ForcedMethodGroup]
        public static T Over<T>(this T before, IPartitionBy partitionBy, IOrderBy orderBy) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [ForcedMethodGroup]
        public static T Over<T>(this T before, IPartitionBy partitionBy, IRows rows) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">Getting row order.</param>
        /// <returns>It is the result of Window function.</returns>
        [ForcedMethodGroup]
        public static T Over<T>(this T before, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [ForcedMethodGroup]
        public static T Over<T>(this T before, IPartitionBy partitionBy) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [ForcedMethodGroup]
        public static T Over<T>(this T before, IOrderBy orderBy) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="rows">Getting row order.</param>
        /// <returns>It is the result of Window function.</returns>
        [ForcedMethodGroup]
        public static T Over<T>(this T before, IRows rows) => InvalitContext.Throw<T>(nameof(Over));
        #endregion

        //TODO あー。。。
        //これは多分かぶるやろ
        
        //CAST句は出ない方に掛けるけど

        //AssignとConditionは難しいな


        #region Utility
        /// <summary>
        /// Cast.
        /// Used to place an ISqlExpressionBase object in a C # expression.
        /// </summary>
        /// <typeparam name="T">Destination type.</typeparam>
        /// <param name="expression">Expression.</param>
        /// <returns>Casted object.</returns>
        public static T Cast<T>(this ISqlExpressionBase expression) => InvalitContext.Throw<T>(nameof(Cast));

        /// <summary>
        /// Cast.
        /// Used to place an IMethodChain object in a C # expression.
        /// </summary>
        /// <typeparam name="T">Destination type.</typeparam>
        /// <param name="expression">Expression.</param>
        /// <returns>Casted object.</returns>
        [ResolveSqlSyntaxMethodChain]
        public static T Cast<T>(this IMethodChain expression) => InvalitContext.Throw<T>(nameof(Cast));

        /// <summary>
        /// Condition building helper.
        /// condition is used if enable is valid.
        /// </summary>
        /// <param name="enable">Whether condition is valid.</param>
        /// <param name="condition">Condition expression.</param>
        /// <returns>Condition.</returns>
        public static bool Condition(bool enable, bool condition) => InvalitContext.Throw<bool>(nameof(Condition));

        /// <summary>
        /// Put the text in the expression of LamblicSql.
        /// </summary>
        /// <param name="text">Text.You can use the same format as System.String's Format method.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>LamblicSql's expression.</returns>
        public static object TextSql(string text, params object[] args) => InvalitContext.Throw<object>(nameof(TextSql));

        /// <summary>
        /// Put the text in the expression of LamblicSql.
        /// </summary>
        /// <typeparam name="T">Destination type.</typeparam>
        /// <param name="text">Text.You can use the same format as System.String's Format method.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>LamblicSql's expression.</returns>
        public static T TextSql<T>(string text, params object[] args) => InvalitContext.Throw<T>(nameof(TextSql));

        /// <summary>
        /// Put the text in the expression of LamblicSql.
        /// You can use TwoWaySql text format.
        /// It's ...
        /// TwoWaySql(@"SELECT money + /*0*/1000/**/ FROM tbl_remuneration WHERE /*1*/tbl_remuneration.money = 100/**/", 1000, tbl_remuneration.staff_id == 10)
        /// Replace /*no*/.../**/ by arguments.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>LamblicSql's expression.</returns>
        public static IClauseChain<Non> TwoWaySql(string text, params object[] args) => InvalitContext.Throw<IClauseChain<Non>>(nameof(TextSql));

        /// <summary>
        /// Get column name only.
        /// It's Removing table name and schema name.
        /// </summary>
        /// <typeparam name="T">column type.</typeparam>
        /// <param name="column">column.</param>
        /// <returns>Column name only.</returns>
        public static T ColumnOnly<T>(T column) => InvalitContext.Throw<T>(nameof(ColumnOnly));
        #endregion

        static SqlText ConvertTop(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return LineSpace("TOP", args[0].Customize(new CustomizeParameterToObject()));
        }


        static SqlText Convert(ISqlStringConverter converter, MemberExpression member)
        {
            switch (member.Member.Name)
            {
                case nameof(CurrentSpaceDate):
                    return "CURRENT DATE";
                case nameof(CurrentSpaceTime):
                    return "CURRENT TIME";
                case nameof(CurrentSpaceTimeStamp):
                    return "CURRENT TIMESTAMP";
            }
            return member.Member.Name.ToUpper();
        }

        static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            switch (method.Method.Name)
            {
                case nameof(Asc):
                    return LineSpace(converter.Convert(methods[0].Arguments[0]), "ASC");
                case nameof(Desc):
                    return LineSpace(converter.Convert(methods[0].Arguments[0]), "DESC");
                case nameof(Top):
                    return ConvertTop(converter, methods);
                case nameof(Select):
                    return SelectClause.Convert(converter, methods);
                case nameof(Case):
                    return CaseClause.Convert(converter, methods);
                case nameof(Delete):
                    return DeleteClause.Convert(converter, methods);
                case nameof(From):
                    return FromClause.ConvertFrom(converter, methods);
                case nameof(Join):
                    return FromClause.ConvertJoin(converter, methods);
                case nameof(LeftJoin):
                    return FromClause.ConvertLeftJoin(converter, methods);
                case nameof(RightJoin):
                    return FromClause.ConvertRightJoin(converter, methods);
                case nameof(CrossJoin):
                    return FromClause.ConvertCrossJoin(converter, methods);
                case nameof(GroupBy):
                    return GroupByClause.ConvertGroupBy(converter, methods);
                case nameof(GroupByRollup):
                    return GroupByClause.ConvertGroupByRollup(converter, methods);
                case nameof(GroupByWithRollup):
                    return GroupByClause.ConvertGroupByWithRollup(converter, methods);
                case nameof(GroupByCube):
                    return GroupByClause.ConvertGroupByCube(converter, methods);
                case nameof(GroupByGroupingSets):
                    return GroupByClause.ConvertGroupByGroupingSets(converter, methods);
                case nameof(Having):
                    return HavingClause.Convert(converter, methods);
                case nameof(InsertInto):
                    return InsertIntoClause.Convert(converter, methods);
                case nameof(OrderBy):
                    return OrderByWordsClause.Convert(converter, methods);
                case nameof(OffsetRows):
                    return OffsetRowsClause.Convert(converter, methods);
                case nameof(FetchNextRowsOnly):
                    return FetchNextRowsOnlyClause.Convert(converter, methods);
                case nameof(Limit):
                    return LimitClause.Convert(converter, methods);
                case nameof(Offset):
                    return OffsetClause.Convert(converter, methods);
                case nameof(Update):
                    return UpdateClause.Convert(converter, methods);
                case nameof(Where):
                    return WhereClause.Convert(converter, methods);
                case nameof(In):
                    return ConditionKeyWords.ConvertIn(converter, methods);
                case nameof(Like):
                    return ConditionKeyWords.ConvertLike(converter, methods);
                case nameof(Between):
                    return ConditionKeyWords.ConvertBetween(converter, methods);
                case nameof(Exists):
                    return ConditionKeyWords.ConvertExists(converter, methods);
                case nameof(Union):
                case nameof(Intersect):
                case nameof(Except):
                case nameof(Minus):
                    return SetOperation.Convert(converter, methods);
                case nameof(IsNull):
                    return NullCheck.ConvertIsNull(converter, methods);
                case nameof(IsNotNull):
                    return NullCheck.ConvertIsNotNull(converter, methods);
                case nameof(Asterisk):
                    return "*";

                //Functions
                case nameof(Sum):
                case nameof(Count):
                    {
                        var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
                        var func = (method.Arguments.Count == 2) ? FuncSpace(method.Method.Name.ToUpper(), args):
                                                                    Func(method.Method.Name.ToUpper(), args);
                        if (methods.Length == 1)
                        {
                            return func;
                        }
                        var v = new VText();
                        v.Add(func);
                        var overMethod = methods[1];
                        v.Add(overMethod.Method.Name.ToUpper() + "(");
                        v.AddRange(1, overMethod.Arguments.Skip(1).
                            Where(e => !(e is ConstantExpression)). //Skip null.
                            Select(e => converter.Convert(e)).ToArray());
                        return v.ConcatToBack(")");
                    }
                case nameof(Extract):
                    {
                        var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
                        return FuncSpace(method.Method.Name.ToUpper(), args[0], "FROM", args[1]);
                    }
                case nameof(Cast):
                    if (method.Arguments.Count == 1)
                    {
                        if (typeof(IMethodChain).IsAssignableFrom(method.Arguments[0].Type))
                        {
                            return converter.Convert(method.Arguments[0]);
                        }
                        return converter.Convert(method.Arguments[0]);
                    }
                    else
                    {
                        var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
                        return FuncSpace("CAST", args[0], "AS", args[1].Customize(new CustomizeParameterToObject()));
                    }
                case nameof(Avg):
                case nameof(Min):
                case nameof(Max):
                case nameof(Abs):
                case nameof(Mod):
                case nameof(Round):
                case nameof(Concat):
                case nameof(Length):
                case nameof(Len):
                case nameof(Lower):
                case nameof(Upper):
                case nameof(Replace):
                case nameof(Substring):
                case nameof(DatePart):
                case nameof(Coalesce):
                case nameof(NVL):
                case nameof(First_Value):
                case nameof(Last_Value):
                case nameof(Rank):
                case nameof(Dense_Rank):
                case nameof(Percent_Rank):
                case nameof(Cume_Dist):
                case nameof(Ntile):
                case nameof(Nth_Value):
                case nameof(Lag):
                    {
                        var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
                        var func = Func(method.Method.Name.ToUpper(), args);
                        if (methods.Length == 1)
                        {
                            return func;
                        }
                        var v = new VText();
                        v.Add(func);
                        var overMethod = methods[1];
                        v.Add(overMethod.Method.Name.ToUpper() + "(");
                        v.AddRange(1, overMethod.Arguments.Skip(1).
                            Where(e => !(e is ConstantExpression)). //Skip null.
                            Select(e => converter.Convert(e)).ToArray());
                        return v.ConcatToBack(")");
                    }

                case nameof(Rows):
                    return Window.ConvertRows(converter, methods);
                case nameof(PartitionBy):
                    return Window.ConvertPartitionBy(converter, methods);
                //Utility
                case nameof(Condition): return Utils.Condition(converter, method);
                case nameof(TextSql): return Utils.TextSql(converter, method);
                case nameof(TwoWaySql): return Utils.TwoWaySql(converter, method);
                case nameof(ColumnOnly): return Utils.ColumnOnly(converter, method);
            }
            throw new NotSupportedException();
        }
    }
}
