using LambdicSql.Inside;
using LambdicSql.Inside.Keywords;
using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// SQL Keywords.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// Use[using static LambdicSql.Keywords;], you can use to write natural SQL.
    /// </summary>
    [SqlSyntax]
    public static class Keywords
    {
        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(Top top, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, Top top, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select(Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select<TSrcSelected>(this IQuery<TSrcSelected> before, Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select(Top top, Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select<TSrcSelected>(this IQuery<TSrcSelected> before, Top top, Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(Top top, Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, Top top, Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(AggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, AggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(AggregatePredicate predicate, Top top, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

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
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, AggregatePredicate predicate, Top top, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select(AggregatePredicate predicate, Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select<TSrcSelected>(this IQuery<TSrcSelected> before, AggregatePredicate predicate, Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(AggregatePredicate predicate, Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, AggregatePredicate predicate, Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select(AggregatePredicate predicate, Top top, Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and Non is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select<TSrcSelected>(this IQuery<TSrcSelected> before, AggregatePredicate predicate, Top top, Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(AggregatePredicate predicate, Top top, Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

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
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, AggregatePredicate predicate, Top top, Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

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
        public static IElseAfter<T> Else<T>(this IThenAfter<T> before, T t) => InvalitContext.Throw<IElseAfter<T>>(nameof(Then));

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
        public static IQuery<Non> From(params object[] expressions) => InvalitContext.Throw<IQuery<Non>>(nameof(From));

        /// <summary>
        /// FROM clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="tables">Table or subquery.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> From<TSelected>(this IQuery<TSelected> before, params object[] tables) => InvalitContext.Throw<IQuery<TSelected>>(nameof(From));

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Join(object table, bool condition) => InvalitContext.Throw<IQuery<Non>>(nameof(Join));

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Join<TSelected>(this IQuery<TSelected> before, object table, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Join));

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of LEFT JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> LeftJoin(object table, bool condition) => InvalitContext.Throw<IQuery<Non>>(nameof(LeftJoin));

        /// <summary>
        /// LEFT JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of LEFT JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> LeftJoin<TSelected>(this IQuery<TSelected> before, object table, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(LeftJoin));

        /// <summary>
        /// RIGHT JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> RightJoin(object table, bool condition) => InvalitContext.Throw<IQuery<Non>>(nameof(RightJoin));

        /// <summary>
        /// RIGHT JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> RightJoin<TSelected>(this IQuery<TSelected> before, object table, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(RightJoin));

        /// <summary>
        /// CROSS JOIN clause.
        /// </summary>
        /// <param name="expressions">Table or subquery.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> CrossJoin(object expression) => InvalitContext.Throw<IQuery<Non>>(nameof(CrossJoin));

        /// <summary>
        /// CROSS JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> CrossJoin<TSelected>(this IQuery<TSelected> before, object table) => InvalitContext.Throw<IQuery<TSelected>>(nameof(CrossJoin));

        /// <summary>
        /// WHERE clause.
        /// </summary>
        /// <param name="condition">It is a conditional expression of WHERE.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Where(bool condition) => InvalitContext.Throw<IQuery<Non>>(nameof(Where));

        /// <summary>
        /// WHERE clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="condition">It is a conditional expression of WHERE.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Where<TSelected>(this IQuery<TSelected> before, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Where));

        /// <summary>
        /// GROUP BY clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> GroupBy(params object[] columns) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupBy));

        /// <summary>
        /// GROUP BY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> GroupBy<TSelected>(this IQuery<TSelected> before, params object[] columns) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupBy));

        /// <summary>
        /// GROUP BY ROLLUP clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> GroupByRollup(params object[] columns) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY ROLLUP clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> GroupByRollup<TSelected>(this IQuery<TSelected> before, params object[] columns) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY columns WITH ROLLUP clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> GroupByWithRollup(params object[] columns) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY columns WITH ROLLUP clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> GroupByWithRollup<TSelected>(this IQuery<TSelected> before, params object[] columns) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY CUBE clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> GroupByCube(params object[] columns) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupByCube));

        /// <summary>
        /// GROUP BY CUBE clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> GroupByCube<TSelected>(this IQuery<TSelected> before, params object[] columns) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupByCube));

        /// <summary>
        /// GROUP BY GROUPING SETS clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> GroupByGroupingSets(params object[] columns) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupByGroupingSets));

        /// <summary>
        /// GROUP BY GROUPING SETS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> GroupByGroupingSets<TSelected>(this IQuery<TSelected> before, params object[] columns) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupByGroupingSets));

        /// <summary>
        /// HAVING clause.
        /// </summary>
        /// <param name="condition">It is a conditional expression of HAVING.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Having(bool condition) => InvalitContext.Throw<IQuery<Non>>(nameof(Having));

        /// <summary>
        /// HAVING clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="condition">It is a conditional expression of HAVING.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Having<TSelected>(this IQuery<TSelected> before, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Having));

        /// <summary>
        /// ORDER BY clause.
        /// </summary>
        /// <param name="elements">Specify column and sort order. Asc(column) or Desc(column).</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> OrderBy(params ISortedBy[] elements) => InvalitContext.Throw<IQuery<Non>>(nameof(OrderBy));

        /// <summary>
        /// ORDER BY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="elements">Specify column and sort order. Asc(column) or Desc(column).</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> OrderBy<TSelected>(this IQuery<TSelected> before, params ISortedBy[] elements) => InvalitContext.Throw<IQuery<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Limit(object count) => InvalitContext.Throw<IQuery<Non>>(nameof(OrderBy));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Limit<TSelected>(this IQuery<TSelected> before, object count) => InvalitContext.Throw<IQuery<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <param name="offset">Start position.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Limit(object offset, object count) => InvalitContext.Throw<IQuery<Non>>(nameof(OrderBy));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="offset">Start position.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Limit<TSelected>(this IQuery<TSelected> before, object offset, object count) => InvalitContext.Throw<IQuery<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// OFFSET clause.
        /// </summary>
        /// <param name="offset">Start position.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Offset(object offset) => InvalitContext.Throw<IQuery<Non>>(nameof(OrderBy));

        /// <summary>
        /// OFFSET clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="offset">Start position.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Offset<TSelected>(this IQuery<TSelected> before, object offset) => InvalitContext.Throw<IQuery<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// OFFSET count ROWS clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> OffsetRows(object count) => InvalitContext.Throw<IQuery<Non>>(nameof(OrderBy));

        /// <summary>
        /// OFFSET count ROWS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> OffsetRows<TSelected>(this IQuery<TSelected> before, object count) => InvalitContext.Throw<IQuery<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// FETCH NEXT count ROWS ONLY clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> FetchNextRowsOnly(object count) => InvalitContext.Throw<IQuery<Non>>(nameof(OrderBy));

        /// <summary>
        /// FETCH NEXT count ROWS ONLY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> FetchNextRowsOnly<TSelected>(this IQuery<TSelected> before, object count) => InvalitContext.Throw<IQuery<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Union() => InvalitContext.Throw<IQuery<Non>>(nameof(Union));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Union<TSelected>(this IQuery<TSelected> before) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Union));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Union(bool isAll) => InvalitContext.Throw<IQuery<Non>>(nameof(Union));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Union<TSelected>(this IQuery<TSelected> before, bool isAll) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Union));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Intersect() => InvalitContext.Throw<IQuery<Non>>(nameof(Intersect));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Intersect<TSelected>(this IQuery<TSelected> before) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Intersect));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Intersect(bool isAll) => InvalitContext.Throw<IQuery<Non>>(nameof(Intersect));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Intersect<TSelected>(this IQuery<TSelected> before, bool isAll) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Intersect));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Except() => InvalitContext.Throw<IQuery<Non>>(nameof(Except));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Except<TSelected>(this IQuery<TSelected> before) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Except));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Except(bool isAll) => InvalitContext.Throw<IQuery<Non>>(nameof(Except));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Except<TSelected>(this IQuery<TSelected> before, bool isAll) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Except));

        /// <summary>
        /// MINUS clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Minus() => InvalitContext.Throw<IQuery<Non>>(nameof(Minus));

        /// <summary>
        /// MINUS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Minus<TSelected>(this IQuery<TSelected> before) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Minus));

        /// <summary>
        /// UPDATE clause.
        /// </summary>
        /// <param name="table">Table for UPDATE.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodGroup(nameof(Update))]
        public static IQuery<Non> Update(object table) => InvalitContext.Throw<IQuery<Non>>(nameof(Update));

        /// <summary>
        /// SET clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="assigns">Assignment in the SET clause.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodGroup(nameof(Update))]
        public static IQuery<TSelected> Set<TSelected>(this IQuery<TSelected> before, params Assign[] assigns) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Set));

        /// <summary>
        /// DELETE clause.
        /// </summary>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Delete() => InvalitContext.Throw<IQuery<Non>>(nameof(Delete));

        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <param name="table">Table for INSERT.</param>
        /// <param name="columns">It is a column that performs INSERT.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodGroup(nameof(InsertInto))]
        public static IQuery<Non> InsertInto(object table, params object[] columns) => InvalitContext.Throw<IQuery<Non>>(nameof(InsertInto));

        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="values">It is the value to be Inserted.</param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodGroup(nameof(InsertInto))]
        public static IQuery<TSelected> Values<TSelected>(this IQuery<TSelected> before, params object[] values)
             => InvalitContext.Throw<IQuery<TSelected>>(nameof(Values));

        /// <summary>
        /// It's *.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="selected">The type you want to obtain with the SELECT clause. Usually you specify a table element.</param>
        /// <returns>*</returns>
        public static Asterisk<TSelected> Asterisk<TSelected>(TSelected selected) => InvalitContext.Throw<Asterisk<TSelected>>(nameof(Asterisk));

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
        public static bool In(object target, IQuery canditates) => InvalitContext.Throw<bool>(nameof(In));

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
        public static bool Exists(IQuery expression) => InvalitContext.Throw<bool>(nameof(Exists));

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

        static string ToString(ISqlStringConverter converter, MemberExpression member)
            => member.Member.Name.ToUpper();

        static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            switch (method.Method.Name)
            {
                case nameof(Select):
                    return SelectClause.ToString(converter, methods);
                case nameof(Case):
                    return CaseClause.ToString(converter, methods);
                case nameof(Delete):
                    return DeleteClause.ToString(converter, methods);
                case nameof(From):
                case nameof(Join):
                case nameof(LeftJoin):
                case nameof(RightJoin):
                case nameof(CrossJoin):
                    return FromClause.ToString(converter, methods);
                case nameof(GroupBy):
                case nameof(GroupByRollup):
                case nameof(GroupByWithRollup):
                case nameof(GroupByCube):
                case nameof(GroupByGroupingSets):
                    return GroupByClause.ToString(converter, methods);
                case nameof(Having):
                    return HavingClause.ToString(converter, methods);
                case nameof(InsertInto):
                    return InsertIntoClause.ToString(converter, methods);
                case nameof(OrderBy):
                    return OrderByWordsClause.ToString(converter, methods);
                case nameof(OffsetRows):
                    return OffsetRowsClause.ToString(converter, methods);
                case nameof(FetchNextRowsOnly):
                    return FetchNextRowsOnlyClause.ToString(converter, methods);
                case nameof(Limit):
                    return LimitClause.ToString(converter, methods);
                case nameof(Offset):
                    return OffsetClause.ToString(converter, methods);
                case nameof(Update):
                    return UpdateClause.ToString(converter, methods);
                case nameof(Where):
                    return WhereClause.ToString(converter, methods);
                case nameof(In):
                case nameof(Like):
                case nameof(Between):
                case nameof(Exists):
                    return ConditionKeyWords.ToString(converter, methods);
                case nameof(Union):
                case nameof(Intersect):
                case nameof(Except):
                case nameof(Minus):
                    return SetOperation.ToString(converter, methods);
                case nameof(IsNull):
                case nameof(IsNotNull):
                    return NullCheck.ToString(converter, methods);
            }
            throw new NotSupportedException();
        }
    }
}
