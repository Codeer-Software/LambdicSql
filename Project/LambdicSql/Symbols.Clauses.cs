﻿using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.SymbolConverters.Inside;

namespace LambdicSql
{
    /// <summary>
    /// SQL Symbols.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// Use[using static LambdicSql.Keywords;], you can use to write natural SQL.
    /// </summary>
    public static partial class Symbols
    {
        /// <summary>
        /// Class representing argument of recursive part.
        /// </summary>
        /// <typeparam name="T">Type representing argument.</typeparam>
        public class RecursiveArguments<T>
        {
            RecursiveArguments() { }
        }
        
        /// <summary>
        /// WITH clause.
        /// </summary>
        /// <param name="subQuerys">sub querys.</param>
        /// <returns></returns>
        [WithConverter]
        public static ClauseChain<Non> With(params Sql[] subQuerys) => InvalitContext.Throw<ClauseChain<Non>>(nameof(With));

        /// <summary>
        /// WITH clause.
        /// </summary>
        /// <typeparam name="T">Type representing argument of recursive part.</typeparam>
        /// <param name="args">Argument of recursive part.</param>
        /// <param name="subQuery">sub query.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [WithConverter]
        public static ClauseChain<T> With<T>(SqlRecursiveArguments<T> args, Sql subQuery) => InvalitContext.Throw<ClauseChain<T>>(nameof(With));

        /// <summary>
        /// RECURSIVE clause.
        /// </summary>
        /// <typeparam name="T">Type representing argument of recursive part.</typeparam>
        /// <param name="args">Argument of recursive part.</param>
        /// <returns>Class representing argument of recursive part.</returns>
        [RecursiveConverter]
        public static RecursiveArguments<T> Recursive<T>(T args) => InvalitContext.Throw<RecursiveArguments<T>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(ITop top, TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, ITop top, TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select(IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select<TSrcSelected>(this ClauseChain<TSrcSelected> before, IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select(ITop top, IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select<TSrcSelected>(this ClauseChain<TSrcSelected> before, ITop top, IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(ITop top, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, ITop top, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(IAggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(IAggregatePredicate predicate, ITop top, TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, ITop top, TSelected selected) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select(IAggregatePredicate predicate, IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select<TSrcSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(IAggregatePredicate predicate, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select(IAggregatePredicate predicate, ITop top, IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select<TSrcSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, ITop top, IAsterisk asterisk) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(IAggregatePredicate predicate, ITop top, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, ITop top, IAsterisk<TSelected> asterisk) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Select));

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
        [ClauseConverter]
        public static ICaseAfter Case() => InvalitContext.Throw<ICaseAfter>(nameof(Case));

        /// <summary>
        /// CASE clause.
        /// </summary>
        /// <param name="target">It's target of CASE branch.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [ClauseConverter]
        public static ICaseAfter Case(object target) => InvalitContext.Throw<ICaseAfter>(nameof(Case));

        /// <summary>
        /// WHEN clause.
        /// </summary>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="expression">It is a conditional expression of the WHEN clause.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [ClauseConverter(Indent =1)]
        public static IWhenAfter When(this ICaseAfter before, object expression) => InvalitContext.Throw<IWhenAfter>(nameof(When));

        /// <summary>
        /// WHEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="expression">It is a conditional expression of the WHEN clause.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [ClauseConverter(Indent = 1)]
        public static IWhenAfter<T> When<T>(this IThenAfter<T> before, object expression) => InvalitContext.Throw<IWhenAfter<T>>(nameof(When));

        /// <summary>
        /// THEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the THEN clause is valid.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [ClauseConverter(Indent = 1)]
        public static IThenAfter<T> Then<T>(this IWhenAfter before, T result) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));

        /// <summary>
        /// THEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the THEN clause is valid.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [ClauseConverter(Indent = 1)]
        public static IThenAfter<T> Then<T>(this IWhenAfter<T> before, T result) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));

        /// <summary>
        /// ELSE clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the ELSE clause is valid.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [ClauseConverter(Indent = 1)]
        public static IElseAfter<T> Else<T>(this IThenAfter<T> before, T result) => InvalitContext.Throw<IElseAfter<T>>(nameof(Then));

        /// <summary>
        /// END clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <returns>It is the result of CASE expression.</returns>
        [ClauseConverter]
        public static T End<T>(this IThenAfter<T> before) => InvalitContext.Throw<T>(nameof(End));

        /// <summary>
        /// END clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <returns>It is the result of CASE expression.</returns>
        [ClauseConverter]
        public static T End<T>(this IElseAfter<T> before) => InvalitContext.Throw<T>(nameof(End));

        /// <summary>
        /// FROM clause.
        /// </summary>
        /// <param name="expressions">Table or subquery.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [FromConverter]
        public static ClauseChain<Non> From(params object[] expressions) => InvalitContext.Throw<ClauseChain<Non>>(nameof(From));

        /// <summary>
        /// FROM clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="tables">Table or subquery.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [FromConverter]
        public static ClauseChain<TSelected> From<TSelected>(this ClauseChain<TSelected> before, params object[] tables) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(From));

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "JOIN")]
        public static ClauseChain<Non> Join(object table, bool condition) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Join));

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "JOIN")]
        public static ClauseChain<TSelected> Join<TSelected>(this ClauseChain<TSelected> before, object table, bool condition) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Join));

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of LEFT JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "LEFT JOIN")]
        public static ClauseChain<Non> LeftJoin(object table, bool condition) => InvalitContext.Throw<ClauseChain<Non>>(nameof(LeftJoin));

        /// <summary>
        /// LEFT JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of LEFT JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "LEFT JOIN")]
        public static ClauseChain<TSelected> LeftJoin<TSelected>(this ClauseChain<TSelected> before, object table, bool condition) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(LeftJoin));

        /// <summary>
        /// RIGHT JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "RIGHT JOIN")]
        public static ClauseChain<Non> RightJoin(object table, bool condition) => InvalitContext.Throw<ClauseChain<Non>>(nameof(RightJoin));

        /// <summary>
        /// RIGHT JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "RIGHT JOIN")]
        public static ClauseChain<TSelected> RightJoin<TSelected>(this ClauseChain<TSelected> before, object table, bool condition) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(RightJoin));

        /// <summary>
        /// FULL JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "FULL JOIN")]
        public static ClauseChain<Non> FullJoin(object table, bool condition) => InvalitContext.Throw<ClauseChain<Non>>(nameof(FullJoin));

        /// <summary>
        /// FULL JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "FULL JOIN")]
        public static ClauseChain<TSelected> FullJoin<TSelected>(this ClauseChain<TSelected> before, object table, bool condition) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(FullJoin));

        /// <summary>
        /// CROSS JOIN clause.
        /// </summary>
        /// <param name="expression">Table or subquery.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "CROSS JOIN")]
        public static ClauseChain<Non> CrossJoin(object expression) => InvalitContext.Throw<ClauseChain<Non>>(nameof(CrossJoin));

        /// <summary>
        /// CROSS JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "CROSS JOIN")]
        public static ClauseChain<TSelected> CrossJoin<TSelected>(this ClauseChain<TSelected> before, object table) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(CrossJoin));

        /// <summary>
        /// WHERE clause.
        /// </summary>
        /// <param name="condition">It is a conditional expression of WHERE.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ConditionClauseConverter]
        public static ClauseChain<Non> Where(bool condition) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Where));

        /// <summary>
        /// WHERE clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="condition">It is a conditional expression of WHERE.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ConditionClauseConverter]
        public static ClauseChain<TSelected> Where<TSelected>(this ClauseChain<TSelected> before, bool condition) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Where));

        /// <summary>
        /// GROUP BY clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter(Name ="GROUP BY")]
        public static ClauseChain<Non> GroupBy(params object[] columns) => InvalitContext.Throw<ClauseChain<Non>>(nameof(GroupBy));

        /// <summary>
        /// GROUP BY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter(Name = "GROUP BY")]
        public static ClauseChain<TSelected> GroupBy<TSelected>(this ClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(GroupBy));

        /// <summary>
        /// GROUP BY ROLLUP clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [FuncConverter(Name = "GROUP BY ROLLUP")]
        public static ClauseChain<Non> GroupByRollup(params object[] columns) => InvalitContext.Throw<ClauseChain<Non>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY ROLLUP clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [FuncConverter(Name = "GROUP BY ROLLUP")]
        public static ClauseChain<TSelected> GroupByRollup<TSelected>(this ClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY columns WITH ROLLUP clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter(Name = "GROUP BY", AfterPredicate = "WITH ROLLUP")]
        public static ClauseChain<Non> GroupByWithRollup(params object[] columns) => InvalitContext.Throw<ClauseChain<Non>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY columns WITH ROLLUP clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter(Name = "GROUP BY", AfterPredicate = "WITH ROLLUP")]
        public static ClauseChain<TSelected> GroupByWithRollup<TSelected>(this ClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(GroupByRollup));

        /// <summary>
        /// GROUP BY CUBE clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [FuncConverter(Name = "GROUP BY CUBE")]
        public static ClauseChain<Non> GroupByCube(params object[] columns) => InvalitContext.Throw<ClauseChain<Non>>(nameof(GroupByCube));

        /// <summary>
        /// GROUP BY CUBE clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [FuncConverter(Name = "GROUP BY CUBE")]
        public static ClauseChain<TSelected> GroupByCube<TSelected>(this ClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(GroupByCube));

        /// <summary>
        /// GROUP BY GROUPING SETS clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [FuncConverter(Name = "GROUP BY GROUPING SETS")]
        public static ClauseChain<Non> GroupByGroupingSets(params object[] columns) => InvalitContext.Throw<ClauseChain<Non>>(nameof(GroupByGroupingSets));

        /// <summary>
        /// GROUP BY GROUPING SETS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [FuncConverter(Name = "GROUP BY GROUPING SETS")]
        public static ClauseChain<TSelected> GroupByGroupingSets<TSelected>(this ClauseChain<TSelected> before, params object[] columns) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(GroupByGroupingSets));

        /// <summary>
        /// HAVING clause.
        /// </summary>
        /// <param name="condition">It is a conditional expression of HAVING.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ConditionClauseConverter]
        public static ClauseChain<Non> Having(bool condition) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Having));

        /// <summary>
        /// HAVING clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="condition">It is a conditional expression of HAVING.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ConditionClauseConverter]
        public static ClauseChain<TSelected> Having<TSelected>(this ClauseChain<TSelected> before, bool condition) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Having));

        /// <summary>
        /// ORDER BY clause.
        /// </summary>
        /// <param name="elements">Specify column and sort order. Asc(column) or Desc(column).</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [OrderByConverter]
        public static OrderBy OrderBy(params ISortedBy[] elements) => InvalitContext.Throw<OrderBy>(nameof(OrderBy));

        /// <summary>
        /// ORDER BY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="elements">Specify column and sort order. Asc(column) or Desc(column).</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [OrderByConverter]
        public static ClauseChain<TSelected> OrderBy<TSelected>(this ClauseChain<TSelected> before, params ISortedBy[] elements) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(OrderBy));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter(Separator = ", ")]
        public static ClauseChain<Non> Limit(object count) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Limit));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter(Separator = ", ")]
        public static ClauseChain<TSelected> Limit<TSelected>(this ClauseChain<TSelected> before, object count) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Limit));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <param name="offset">Start position.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter(Separator = ", ")]
        public static ClauseChain<Non> Limit(object offset, object count) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Limit));

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="offset">Start position.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter(Separator = ", ")]
        public static ClauseChain<TSelected> Limit<TSelected>(this ClauseChain<TSelected> before, object offset, object count) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Limit));

        /// <summary>
        /// OFFSET clause.
        /// </summary>
        /// <param name="offset">Start position.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<Non> Offset(object offset) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Offset));

        /// <summary>
        /// OFFSET clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="offset">Start position.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<TSelected> Offset<TSelected>(this ClauseChain<TSelected> before, object offset) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Offset));

        /// <summary>
        /// OFFSET count ROWS clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter(Name = "OFFSET", AfterPredicate = "ROWS")]
        public static ClauseChain<Non> OffsetRows(object count) => InvalitContext.Throw<ClauseChain<Non>>(nameof(OffsetRows));

        /// <summary>
        /// OFFSET count ROWS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter(Name = "OFFSET", AfterPredicate = "ROWS")]
        public static ClauseChain<TSelected> OffsetRows<TSelected>(this ClauseChain<TSelected> before, object count) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(OffsetRows));

        /// <summary>
        /// FETCH NEXT count ROWS ONLY clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter(Name = "FETCH NEXT", AfterPredicate = "ROWS ONLY")]
        public static ClauseChain<Non> FetchNextRowsOnly(object count) => InvalitContext.Throw<ClauseChain<Non>>(nameof(FetchNextRowsOnly));

        /// <summary>
        /// FETCH NEXT count ROWS ONLY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter(Name = "FETCH NEXT", AfterPredicate = "ROWS ONLY")]
        public static ClauseChain<TSelected> FetchNextRowsOnly<TSelected>(this ClauseChain<TSelected> before, object count) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(FetchNextRowsOnly));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<Non> Union() => InvalitContext.Throw<ClauseChain<Non>>(nameof(Union));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<TSelected> Union<TSelected>(this ClauseChain<TSelected> before) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Union));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<Non> Union(IAggregatePredicateAll all) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Union));

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<TSelected> Union<TSelected>(this ClauseChain<TSelected> before, IAggregatePredicateAll all) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Union));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<Non> Intersect() => InvalitContext.Throw<ClauseChain<Non>>(nameof(Intersect));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<TSelected> Intersect<TSelected>(this ClauseChain<TSelected> before) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Intersect));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<Non> Intersect(IAggregatePredicateAll all) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Intersect));

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<TSelected> Intersect<TSelected>(this ClauseChain<TSelected> before, IAggregatePredicateAll all) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Intersect));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<Non> Except() => InvalitContext.Throw<ClauseChain<Non>>(nameof(Except));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<TSelected> Except<TSelected>(this ClauseChain<TSelected> before) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Except));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<Non> Except(IAggregatePredicateAll all) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Except));

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<TSelected> Except<TSelected>(this ClauseChain<TSelected> before, IAggregatePredicateAll all) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Except));

        /// <summary>
        /// MINUS clause.
        /// </summary>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<Non> Minus() => InvalitContext.Throw<ClauseChain<Non>>(nameof(Minus));

        /// <summary>
        /// MINUS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<TSelected> Minus<TSelected>(this ClauseChain<TSelected> before) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Minus));

        /// <summary>
        /// UPDATE clause.
        /// </summary>
        /// <param name="table">Table for UPDATE.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<Non> Update(object table) => InvalitContext.Throw<ClauseChain<Non>>(nameof(Update));

        //TODO test
        /// <summary>
        /// SET clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="assigns">Assignment in the SET clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SetConverter]
        public static ClauseChain<TSelected> Set<TSelected>(params Assign[] assigns) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Set));

        /// <summary>
        /// SET clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="assigns">Assignment in the SET clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SetConverter]
        public static ClauseChain<TSelected> Set<TSelected>(this ClauseChain<TSelected> before, params Assign[] assigns) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Set));

        /// <summary>
        /// DELETE clause.
        /// </summary>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseConverter]
        public static ClauseChain<Non> Delete() => InvalitContext.Throw<ClauseChain<Non>>(nameof(Delete));

        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <param name="table">Table for INSERT.</param>
        /// <param name="columns">It is a column that performs INSERT.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [InsertIntoConverter]
        public static ClauseChain<Non> InsertInto(object table, params object[] columns) => InvalitContext.Throw<ClauseChain<Non>>(nameof(InsertInto));

        //TODO test
        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="values">It is the value to be Inserted.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ValuesConverter]
        public static ClauseChain<TSelected> Values<TSelected>(params object[] values) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Values));

        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="values">It is the value to be Inserted.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ValuesConverter]
        public static ClauseChain<TSelected> Values<TSelected>(this ClauseChain<TSelected> before, params object[] values) => InvalitContext.Throw<ClauseChain<TSelected>>(nameof(Values));

        /// <summary>
        /// LIKE keyword.
        /// </summary>
        /// <param name="target">Target text.</param>
        /// <param name="pattern">Text that represents pattern matching.</param>
        /// <returns>If target matches the specified pattern, LIKE returns TRUE.</returns>
        [LikeConverter]
        public static bool Like(object target, object pattern) => InvalitContext.Throw<bool>(nameof(Like));

        /// <summary>
        /// BETWEEN keyword.
        /// </summary>
        /// <param name="target">Target of range check.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maxmum value.</param>
        /// <returns>Returns TRUE if target is included in the range of min and max.</returns>
        [BetweenConverter]
        public static bool Between(object target, object min, object max) => InvalitContext.Throw<bool>(nameof(Between));

        /// <summary>
        /// IN keyword.
        /// </summary>
        /// <param name="target">Target of IN check.</param>
        /// <param name="canditates">Canditates.</param>
        /// <returns>Returns TRUE if target is included in the canditates represented by expression.</returns>
        [InConverter]
        public static bool In(object target, params object[] canditates) => InvalitContext.Throw<bool>(nameof(In));

        /// <summary>
        /// ALL Keyword
        /// </summary>
        /// <returns></returns>
        [KeywordMethodConverter]
        public static IAggregatePredicateAll All() => null;

        /// <summary>
        /// Distinct Keyword
        /// </summary>
        /// <returns></returns>
        [KeywordMethodConverter]
        public static IAggregatePredicate Distinct() => InvalitContext.Throw<IAggregatePredicate>(nameof(All));

        /// <summary>
        /// ALL Keyword
        /// </summary>
        /// <typeparam name="T">Retunn type.</typeparam>
        /// <param name="sub">Sub query.</param>
        /// <returns>Sub query's selected value.</returns>
        [AllConverter]
        public static T All<T>(object sub) => InvalitContext.Throw<T>(nameof(All));

        /// <summary>
        /// EXISTS keyword.
        /// </summary>
        /// <param name="expression">Sub query.</param>
        /// <returns>Returns TRUE if there is at least one record returned by expression, FALSE otherwise.</returns>
        [ClauseConverter]
        public static bool Exists(object expression) => InvalitContext.Throw<bool>(nameof(Exists));
    }
}