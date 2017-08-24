using LambdicSql;
using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Specialized.SymbolConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static Test.PartsFactoryUtils;

namespace Test
{   
    /// <summary>
    /// TOP keyword.
    /// </summary>
    public abstract class TopElement { }

    /// <summary>
    /// Element of DateTime.
    /// </summary>
    [EnumToStringConverter]
    public enum DateTimeElement
    {
        Day,
    }
    
    /// <summary>
    /// SQL Symbol.
    /// It can only be used within lambda of the LambdicSql.
    /// Use[using static Test.Symbol;], you can use to write natural SQL.
    /// </summary>
    public static partial class Symbol
    {
        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<T>(T selected) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrc"></typeparam>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<TSrc, T>(this Clause<TSrc> before, T selected) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<T>(TopElement top, T selected) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrc">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<TSrc, T>(this Clause<TSrc> before, TopElement top, T selected) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<Non> Select(AsteriskElement asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrc">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<Non> Select<TSrc>(this Clause<TSrc> before, AsteriskElement asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<T>(AsteriskElement<T> asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrc">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<TSrc, T>(this Clause<TSrc> before, AsteriskElement<T> asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<Non> Select(TopElement top, AsteriskElement asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrc">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<Non> Select<TSrc>(this Clause<TSrc> before, TopElement top, AsteriskElement asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<T>(TopElement top, AsteriskElement<T> asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrc">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<TSrc, T>(this Clause<TSrc> before, TopElement top, AsteriskElement<T> asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<T>(AggregatePredicateElement predicate, T selected) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrc">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<TSrc, T>(this Clause<TSrc> before, AggregatePredicateElement predicate, T selected) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<T>(AggregatePredicateElement predicate, TopElement top, T selected) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrc">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<TSrc, T>(this Clause<TSrc> before, AggregatePredicateElement predicate, TopElement top, T selected) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<Non> Select(AggregatePredicateElement predicate, AsteriskElement asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrc">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<Non> Select<TSrc>(this Clause<TSrc> before, AggregatePredicateElement predicate, AsteriskElement asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<T>(AggregatePredicateElement predicate, AsteriskElement<T> asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrc">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<TSrc, T>(this Clause<TSrc> before, AggregatePredicateElement predicate, AsteriskElement<T> asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<Non> Select(AggregatePredicateElement predicate, TopElement top, AsteriskElement asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrc">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<Non> Select<TSrc>(this Clause<TSrc> before, AggregatePredicateElement predicate, TopElement top, AsteriskElement asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<T>(AggregatePredicateElement predicate, TopElement top, AsteriskElement<T> asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrc">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="T">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause.</returns>
        [SelectConverter]
        public static Clause<T> Select<TSrc, T>(this Clause<TSrc> before, AggregatePredicateElement predicate, TopElement top, AsteriskElement<T> asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// CASE clause.
        /// </summary>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> Case() { throw new InvalitContextException(nameof(Case)); }

        /// <summary>
        /// CASE clause.
        /// </summary>
        /// <param name="target">It's target of CASE branch.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> Case(object target) { throw new InvalitContextException(nameof(Case)); }

        /// <summary>
        /// CASE clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<T> Case<T>() { throw new InvalitContextException(nameof(Case)); }

        /// <summary>
        /// CASE clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="target">It's target of CASE branch.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<T> Case<T>(object target) { throw new InvalitContextException(nameof(Case)); }

        /// <summary>
        /// WHEN clause.
        /// </summary>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="expression">It is a conditional expression of the WHEN clause.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Indent = 1)]
        public static Clause<Non> When(this Clause<Non> before, object expression) { throw new InvalitContextException(nameof(When)); }

        /// <summary>
        /// WHEN clause.
        /// </summary>
        /// <param name="expression">It is a conditional expression of the WHEN clause.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Indent = 1)]
        public static Clause<Non> When(object expression) { throw new InvalitContextException(nameof(When)); }

        /// <summary>
        /// WHEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="expression">It is a conditional expression of the WHEN clause.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Indent = 1)]
        public static Clause<T> When<T>(this Clause<T> before, object expression) { throw new InvalitContextException(nameof(When)); }

        /// <summary>
        /// THEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the THEN clause is valid.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Indent = 1)]
        public static Clause<T> Then<T>(this Clause<Non> before, T result) { throw new InvalitContextException(nameof(Then)); }

        /// <summary>
        /// THEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="result">It is an item to return to when the THEN clause is valid.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Indent = 1)]
        public static Clause<T> Then<T>(T result) { throw new InvalitContextException(nameof(Then)); }

        /// <summary>
        /// THEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the THEN clause is valid.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Indent = 1)]
        public static Clause<T> Then<T>(this Clause<T> before, T result) { throw new InvalitContextException(nameof(Then)); }

        /// <summary>
        /// ELSE clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the ELSE clause is valid.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Indent = 1)]
        public static Clause<T> Else<T>(this Clause<T> before, T result) { throw new InvalitContextException(nameof(Then)); }

        /// <summary>
        /// ELSE clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="result">It is an item to return to when the ELSE clause is valid.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Indent = 1)]
        public static Clause<T> Else<T>(T result) { throw new InvalitContextException(nameof(Then)); }

        /// <summary>
        /// END clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <returns>It is the result of CASE expression.</returns>
        [ClauseStyleConverter]
        public static T End<T>(this Clause<T> before) { throw new InvalitContextException(nameof(End)); }

        /// <summary>
        /// END clause.
        /// </summary>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> End() { throw new InvalitContextException(nameof(End)); }

        /// <summary>
        /// FROM clause.
        /// </summary>
        /// <param name="expressions">Table or subquery.</param>
        /// <returns>Clause.</returns>
        [FromConverter]
        public static Clause<Non> From(params object[] expressions) { throw new InvalitContextException(nameof(From)); }

        /// <summary>
        /// FROM clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="tables">Table or subquery.</param>
        /// <returns>Clause.</returns>
        [FromConverter]
        public static Clause<T> From<T>(this Clause<T> before, params object[] tables) { throw new InvalitContextException(nameof(From)); }

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of JOIN.</param>
        /// <returns>Clause.</returns>
        [JoinConverter(Name = "JOIN")]
        public static Clause<Non> Join(object table, bool condition) { throw new InvalitContextException(nameof(Join)); }

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of JOIN.</param>
        /// <returns>Clause.</returns>
        [JoinConverter(Name = "JOIN")]
        public static Clause<T> Join<T>(this Clause<T> before, object table, bool condition) { throw new InvalitContextException(nameof(Join)); }

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of LEFT JOIN.</param>
        /// <returns>Clause.</returns>
        [JoinConverter(Name = "LEFT JOIN")]
        public static Clause<Non> LeftJoin(object table, bool condition) { throw new InvalitContextException(nameof(LeftJoin)); }

        /// <summary>
        /// LEFT JOIN clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of LEFT JOIN.</param>
        /// <returns>Clause.</returns>
        [JoinConverter(Name = "LEFT JOIN")]
        public static Clause<T> LeftJoin<T>(this Clause<T> before, object table, bool condition) { throw new InvalitContextException(nameof(LeftJoin)); }

        /// <summary>
        /// RIGHT JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>Clause.</returns>
        [JoinConverter(Name = "RIGHT JOIN")]
        public static Clause<Non> RightJoin(object table, bool condition) { throw new InvalitContextException(nameof(RightJoin)); }

        /// <summary>
        /// RIGHT JOIN clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>Clause.</returns>
        [JoinConverter(Name = "RIGHT JOIN")]
        public static Clause<T> RightJoin<T>(this Clause<T> before, object table, bool condition) { throw new InvalitContextException(nameof(RightJoin)); }

        /// <summary>
        /// FULL JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>Clause.</returns>
        [JoinConverter(Name = "FULL JOIN")]
        public static Clause<Non> FullJoin(object table, bool condition) { throw new InvalitContextException(nameof(FullJoin)); }

        /// <summary>
        /// FULL JOIN clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>Clause.</returns>
        [JoinConverter(Name = "FULL JOIN")]
        public static Clause<T> FullJoin<T>(this Clause<T> before, object table, bool condition) { throw new InvalitContextException(nameof(FullJoin)); }

        /// <summary>
        /// CROSS JOIN clause.
        /// </summary>
        /// <param name="expression">Table or subquery.</param>
        /// <returns>Clause.</returns>
        [JoinConverter(Name = "CROSS JOIN")]
        public static Clause<Non> CrossJoin(object expression) { throw new InvalitContextException(nameof(CrossJoin)); }

        /// <summary>
        /// CROSS JOIN clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <returns>Clause.</returns>
        [JoinConverter(Name = "CROSS JOIN")]
        public static Clause<T> CrossJoin<T>(this Clause<T> before, object table) { throw new InvalitContextException(nameof(CrossJoin)); }

        /// <summary>
        /// WHERE clause.
        /// </summary>
        /// <param name="condition">It is a conditional expression of WHERE.</param>
        /// <returns>Clause.</returns>
        [ConditionClauseConverter(Name = "WHERE")]
        public static Clause<Non> Where(bool condition) { throw new InvalitContextException(nameof(Where)); }

        /// <summary>
        /// WHERE clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="condition">It is a conditional expression of WHERE.</param>
        /// <returns>Clause.</returns>
        [ConditionClauseConverter(Name = "WHERE")]
        public static Clause<T> Where<T>(this Clause<T> before, bool condition) { throw new InvalitContextException(nameof(Where)); }

        /// <summary>
        /// GROUP BY clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Name = "GROUP BY")]
        public static Clause<Non> GroupBy(params object[] columns) { throw new InvalitContextException(nameof(GroupBy)); }

        /// <summary>
        /// GROUP BY clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Name = "GROUP BY")]
        public static Clause<T> GroupBy<T>(this Clause<T> before, params object[] columns) { throw new InvalitContextException(nameof(GroupBy)); }

        /// <summary>
        /// GROUP BY ROLLUP clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter(Name = "GROUP BY ROLLUP")]
        public static Clause<Non> GroupByRollup(params object[] columns) { throw new InvalitContextException(nameof(GroupByRollup)); }

        /// <summary>
        /// GROUP BY ROLLUP clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter(Name = "GROUP BY ROLLUP")]
        public static Clause<T> GroupByRollup<T>(this Clause<T> before, params object[] columns) { throw new InvalitContextException(nameof(GroupByRollup)); }

        /// <summary>
        /// GROUP BY columns WITH ROLLUP clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause.</returns>
        /// 
        [MethodFormatConverter(Format = "GROUP BY |[<, >0] WITH ROLLUP")]
        public static Clause<Non> GroupByWithRollup(params object[] columns) { throw new InvalitContextException(nameof(GroupByRollup)); }

        /// <summary>
        /// GROUP BY columns WITH ROLLUP clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause.</returns>
        [MethodFormatConverter(Format = "GROUP BY |[<, >1] WITH ROLLUP")]
        public static Clause<T> GroupByWithRollup<T>(this Clause<T> before, params object[] columns) { throw new InvalitContextException(nameof(GroupByRollup)); }

        /// <summary>
        /// GROUP BY CUBE clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter(Name = "GROUP BY CUBE")]
        public static Clause<Non> GroupByCube(params object[] columns) { throw new InvalitContextException(nameof(GroupByCube)); }

        /// <summary>
        /// GROUP BY CUBE clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter(Name = "GROUP BY CUBE")]
        public static Clause<T> GroupByCube<T>(this Clause<T> before, params object[] columns) { throw new InvalitContextException(nameof(GroupByCube)); }

        /// <summary>
        /// GROUP BY GROUPING SETS clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter(Name = "GROUP BY GROUPING SETS")]
        public static Clause<Non> GroupByGroupingSets(params object[] columns) { throw new InvalitContextException(nameof(GroupByGroupingSets)); }

        /// <summary>
        /// GROUP BY GROUPING SETS clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter(Name = "GROUP BY GROUPING SETS")]
        public static Clause<T> GroupByGroupingSets<T>(this Clause<T> before, params object[] columns) { throw new InvalitContextException(nameof(GroupByGroupingSets)); }

        /// <summary>
        /// HAVING clause.
        /// </summary>
        /// <param name="condition">It is a conditional expression of HAVING.</param>
        /// <returns>Clause.</returns>
        [ConditionClauseConverter(Name = "HAVING")]
        public static Clause<Non> Having(bool condition) { throw new InvalitContextException(nameof(Having)); }

        /// <summary>
        /// HAVING clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="condition">It is a conditional expression of HAVING.</param>
        /// <returns>Clause.</returns>
        [ConditionClauseConverter(Name = "HAVING")]
        public static Clause<T> Having<T>(this Clause<T> before, bool condition) { throw new InvalitContextException(nameof(Having)); }

        /// <summary>
        /// ORDER BY clause.
        /// </summary>
        /// <param name="elements">Specify column and sort order. Asc(column) or Desc(column).</param>
        /// <returns>Clause.</returns>
        [MethodFormatConverter(Format = "ORDER BY |[<, >0]", FormatDirection = FormatDirection.Vertical, VanishIfEmptyParams = true)]
        public static Clause<OverElement> OrderBy(params OrderByElement[] elements) { throw new InvalitContextException(nameof(OrderBy)); }

        /// <summary>
        /// ORDER BY clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="elements">Specify column and sort order. Asc(column) or Desc(column).</param>
        /// <returns>Clause.</returns>
        [MethodFormatConverter(Format = "ORDER BY |[<, >1]", FormatDirection = FormatDirection.Vertical, VanishIfEmptyParams = true)]
        public static Clause<T> OrderBy<T>(this Clause<T> before, params OrderByElement[] elements) { throw new InvalitContextException(nameof(OrderBy)); }

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> Limit(object count) { throw new InvalitContextException(nameof(Limit)); }

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<T> Limit<T>(this Clause<T> before, object count) { throw new InvalitContextException(nameof(Limit)); }

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <param name="offset">Start position.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> Limit(object offset, object count) { throw new InvalitContextException(nameof(Limit)); }

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="offset">Start position.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<T> Limit<T>(this Clause<T> before, object offset, object count) { throw new InvalitContextException(nameof(Limit)); }

        /// <summary>
        /// OFFSET clause.
        /// </summary>
        /// <param name="offset">Start position.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> Offset(object offset) { throw new InvalitContextException(nameof(Offset)); }

        /// <summary>
        /// OFFSET clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="offset">Start position.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<T> Offset<T>(this Clause<T> before, object offset) { throw new InvalitContextException(nameof(Offset)); }

        /// <summary>
        /// OFFSET count ROWS clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause.</returns>
        [MethodFormatConverter(Format = "OFFSET |[0] ROWS")]
        public static Clause<Non> OffsetRows(object count) { throw new InvalitContextException(nameof(OffsetRows)); }

        /// <summary>
        /// OFFSET count ROWS clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause.</returns>
        [MethodFormatConverter(Format = "OFFSET |[1] ROWS")]
        public static Clause<T> OffsetRows<T>(this Clause<T> before, object count) { throw new InvalitContextException(nameof(OffsetRows)); }

        /// <summary>
        /// FETCH NEXT count ROWS ONLY clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause.</returns>
        [MethodFormatConverter(Format = "FETCH NEXT |[0] ROWS ONLY")]
        public static Clause<Non> FetchNextRowsOnly(object count) { throw new InvalitContextException(nameof(FetchNextRowsOnly)); }

        /// <summary>
        /// FETCH NEXT count ROWS ONLY clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause.</returns>
        [MethodFormatConverter(Format = "FETCH NEXT |[1] ROWS ONLY")]
        public static Clause<T> FetchNextRowsOnly<T>(this Clause<T> before, object count) { throw new InvalitContextException(nameof(FetchNextRowsOnly)); }

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> Union() { throw new InvalitContextException(nameof(Union)); }

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<T> Union<T>(this Clause<T> before) { throw new InvalitContextException(nameof(Union)); }

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> Union(AggregatePredicateAllElement all) { throw new InvalitContextException(nameof(Union)); }

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<T> Union<T>(this Clause<T> before, AggregatePredicateAllElement all) { throw new InvalitContextException(nameof(Union)); }

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> Intersect() { throw new InvalitContextException(nameof(Intersect)); }

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<T> Intersect<T>(this Clause<T> before) { throw new InvalitContextException(nameof(Intersect)); }

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> Intersect(AggregatePredicateAllElement all) { throw new InvalitContextException(nameof(Intersect)); }

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<T> Intersect<T>(this Clause<T> before, AggregatePredicateAllElement all) { throw new InvalitContextException(nameof(Intersect)); }

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> Except() { throw new InvalitContextException(nameof(Except)); }

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<T> Except<T>(this Clause<T> before) { throw new InvalitContextException(nameof(Except)); }

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> Except(AggregatePredicateAllElement all) { throw new InvalitContextException(nameof(Except)); }

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<T> Except<T>(this Clause<T> before, AggregatePredicateAllElement all) { throw new InvalitContextException(nameof(Except)); }

        /// <summary>
        /// MINUS clause.
        /// </summary>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> Minus() { throw new InvalitContextException(nameof(Minus)); }

        /// <summary>
        /// MINUS clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<T> Minus<T>(this Clause<T> before) { throw new InvalitContextException(nameof(Minus)); }

        /// <summary>
        /// UPDATE clause.
        /// </summary>
        /// <param name="table">Table for UPDATE.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> Update(object table) { throw new InvalitContextException(nameof(Update)); }

        /// <summary>
        /// SET clause.
        /// </summary>
        /// <param name="assigns">Assignment in the SET clause.</param>
        /// <returns>Clause.</returns>
        [MethodFormatConverter(Format = "SET |[<,>0]", FormatDirection = FormatDirection.Vertical)]
        public static Clause<Non> Set(params Assign[] assigns) { throw new InvalitContextException(nameof(Set)); }

        /// <summary>
        /// SET clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="assigns">Assignment in the SET clause.</param>
        /// <returns>Clause.</returns>
        [MethodFormatConverter(Format = "SET |[<,>1]", FormatDirection = FormatDirection.Vertical)]
        public static Clause<T> Set<T>(this Clause<T> before, params Assign[] assigns) { throw new InvalitContextException(nameof(Set)); }

        /// <summary>
        /// DELETE clause.
        /// </summary>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<Non> Delete() { throw new InvalitContextException(nameof(Delete)); }

        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <param name="table">Table for INSERT.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Name = "INSERT INTO")]
        public static Clause<Non> InsertInto(object table) { throw new InvalitContextException(nameof(InsertInto)); }

        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <param name="table">Table for INSERT.</param>
        /// <param name="columns">It is a column that performs INSERT.</param>
        /// <returns>Clause.</returns>
        [MethodFormatConverter(Format = "INSERT INTO [0](|[#<, >1])")]
        public static Clause<Non> InsertInto(object table, params object[] columns) { throw new InvalitContextException(nameof(InsertInto)); }

        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <param name="values">It is the value to be Inserted.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter(Indent = 1)]
        public static Clause<Non> Values(params object[] values) { throw new InvalitContextException(nameof(Values)); }

        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <typeparam name="T">The type represented by before clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="values">It is the value to be Inserted.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter(Indent = 1)]
        public static Clause<T> Values<T>(this Clause<T> before, params object[] values) { throw new InvalitContextException(nameof(Values)); }

        /// <summary>
        /// LIKE keyword.
        /// </summary>
        /// <param name="target">Target text.</param>
        /// <param name="pattern">Text that represents pattern matching.</param>
        /// <returns>If target matches the specified pattern, LIKE returns TRUE.</returns>
        [MethodFormatConverter(Format = "[0] LIKE |[1]")]
        public static bool Like(object target, object pattern) { throw new InvalitContextException(nameof(Like)); }

        /// <summary>
        /// BETWEEN keyword.
        /// </summary>
        /// <param name="target">Target of range check.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maxmum value.</param>
        /// <returns>Returns TRUE if target is included in the range of min and max.</returns>
        [MethodFormatConverter(Format = "[0] BETWEEN |[1] AND [2]")]
        public static bool Between(object target, object min, object max) { throw new InvalitContextException(nameof(Between)); }
        
        /// <summary>
        /// IN keyword.
        /// </summary>
        /// <param name="target">Target of IN check.</param>
        /// <param name="canditates">Canditates.</param>
        /// <returns>Returns TRUE if target is included in the canditates represented by expression.</returns>
        [MethodFormatConverter(Format = "[0] IN(|[<, >1])")]
        public static bool In<T>(T target, params T[] canditates) { throw new InvalitContextException(nameof(In)); }

        /// <summary>
        /// ALL Keyword
        /// </summary>
        /// <typeparam name="T">Retunn type.</typeparam>
        /// <param name="sub">Sub query.</param>
        /// <returns>Sub query's selected value.</returns>
        [AllConverter]
        public static T All<T>(Clause<T> sub) { throw new InvalitContextException(nameof(All)); }

        /// <summary>
        /// ALL Keyword
        /// </summary>
        /// <typeparam name="T">Retunn type.</typeparam>
        /// <param name="sub">Sub query.</param>
        /// <returns>Sub query's selected value.</returns>
        [AllConverter]
        public static T All<T>(Sql<T> sub) { throw new InvalitContextException(nameof(All)); }

        /// <summary>
        /// EXISTS keyword.
        /// </summary>
        /// <param name="expression">Sub query.</param>
        /// <returns>Returns TRUE if there is at least one record returned by expression, FALSE otherwise.</returns>
        [ClauseStyleConverter]
        public static bool Exists(object expression) { throw new InvalitContextException(nameof(Exists)); }

        /// <summary>
        /// IS NULL clause.
        /// </summary>
        /// <param name="target"></param>
        /// <returns>IS NULL</returns>
        [MethodFormatConverter(Format = "[0] IS NULL|")]
        public static bool IsNull(object target) { throw new InvalitContextException(nameof(IsNull)); }

        /// <summary>
        /// IS NOT NULL clause.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>IS NOT NULL</returns>
        [MethodFormatConverter(Format = "[0] IS NOT NULL|")]
        public static bool IsNotNull(object target) { throw new InvalitContextException(nameof(IsNull)); }

        /// <summary>
        /// CREATE TABLE clause.
        /// </summary>
        /// <returns>Clause.</returns>
        [MethodFormatConverter(Format = "CREATE TABLE [0](|[#$<,>1])", FormatDirection = FormatDirection.Vertical)]
        public static Clause<Non> CreateTable(object table, params TableDefinitionElement[] designer) { throw new InvalitContextException(nameof(CreateTable)); }

        /// <summary>
        /// CREATE TABLE clause.
        /// </summary>
        /// <returns>Clause.</returns>
        [MethodFormatConverter(Format = "CREATE TABLE IF NOT EXISTS [0](|[#$<,>1])", FormatDirection = FormatDirection.Vertical)]
        public static Clause<Non> CreateTableIfNotExists(object table, params TableDefinitionElement[] designer) { throw new InvalitContextException(nameof(CreateTable)); }

        /// <summary>
        /// CONSTRAINT clause.
        /// </summary>
        /// <param name="name">Constraint name.</param>
        /// <returns>Clause.</returns>
        [MethodFormatConverter(Format = "CONSTRAINT [!0]")]
        public static Clause<ConstraintElement> Constraint(string name) { throw new InvalitContextException(nameof(Constraint)); }

        /// <summary>
        /// PRIMARY KEY clause.
        /// </summary>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Name = "PRIMARY KEY")]
        public static Clause<ConstraintElement> PrimaryKey() { throw new InvalitContextException(nameof(PrimaryKey)); }

        /// <summary>
        /// PRIMARY KEY clause.
        /// </summary>
        /// <param name="columns">Columns.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter(Name = "PRIMARY KEY")]
        public static Clause<ConstraintElement> PrimaryKey(params object[] columns) { throw new InvalitContextException(nameof(PrimaryKey)); }

        /// <summary>
        /// PRIMARY KEY clause.
        /// </summary>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Columns.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter(Name = "PRIMARY KEY", Indent = 1)]
        public static Clause<ConstraintElement> PrimaryKey(this Clause<ConstraintElement> before, params object[] columns) { throw new InvalitContextException(nameof(PrimaryKey)); }

        /// <summary>
        /// CHECK clause.
        /// </summary>
        /// <param name="condition">Condition.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter]
        public static Clause<ConstraintElement> Check(bool condition) { throw new InvalitContextException(nameof(Check)); }

        /// <summary>
        /// CHECK clause.
        /// </summary>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="condition">Condition.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter(Indent = 1)]
        public static Clause<ConstraintElement> Check(this Clause<ConstraintElement> before, bool condition) { throw new InvalitContextException(nameof(Check)); }

        /// <summary>
        /// UNIQUE clause.
        /// </summary>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<ConstraintElement> Unique() { throw new InvalitContextException(nameof(Unique)); }

        /// <summary>
        /// UNIQUE clause.
        /// </summary>
        /// <param name="columns">Columns.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter]
        public static Clause<ConstraintElement> Unique(params object[] columns) { throw new InvalitContextException(nameof(Unique)); }

        /// <summary>
        /// UNIQUE clause.
        /// </summary>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Columns.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter(Indent = 1)]
        public static Clause<ConstraintElement> Unique(this Clause<ConstraintElement> before, params object[] columns) { throw new InvalitContextException(nameof(Unique)); }

        /// <summary>
        /// FOREIGN KEY clause.
        /// </summary>
        /// <param name="columns">Columns.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter(Name = "FOREIGN KEY")]
        public static Clause<ConstraintElement> ForeignKey(params object[] columns) { throw new InvalitContextException(nameof(ForeignKey)); }

        /// <summary>
        /// FOREIGN KEY clause.
        /// </summary>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Columns.</param>
        /// <returns>Clause.</returns>
        [FuncStyleConverter(Name = "FOREIGN KEY", Indent = 1)]
        public static Clause<ConstraintElement> ForeignKey(this Clause<ConstraintElement> before, params object[] columns) { throw new InvalitContextException(nameof(ForeignKey)); }

        /// <summary>
        /// NOT NULL
        /// </summary>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Name = "NOT NULL")]
        public static Clause<ConstraintElement> NotNull() { throw new InvalitContextException(nameof(NotNull)); }

        /// <summary>
        /// DEFAULT
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter]
        public static Clause<ConstraintElement> Default(object value) { throw new InvalitContextException(nameof(NotNull)); }

        /// <summary>
        /// REFERENCES clause.
        /// </summary>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table.</param>
        /// <param name="columns">Columns.</param>
        /// <returns>Clause.</returns>
        [MethodFormatConverter(Format = "REFERENCES [1](|[<, >2])", Indent = 1)]
        public static Clause<ConstraintElement> References(this Clause<ConstraintElement> before, object table, params object[] columns) { throw new InvalitContextException(nameof(References)); }

        /// <summary>
        /// DROP TABLE clause.
        /// </summary>
        /// <param name="tables">Tables.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Name = "DROP TABLE")]
        public static Clause<Non> DropTable(params object[] tables) { throw new InvalitContextException(nameof(DropTable)); }

        /// <summary>
        /// DROP TABLE IF EXISTS clause.
        /// </summary>
        /// <param name="tables">Tables.</param>
        /// <returns>Clause.</returns>
        [ClauseStyleConverter(Name = "DROP TABLE IF EXISTS")]
        public static Clause<Non> DropTableIfExists(params object[] tables) { throw new InvalitContextException(nameof(DropTableIfExists)); }


        /// <summary>
        /// DROP DATABASE clause.
        /// </summary>
        /// <param name="name">DataBase name.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "CREATE DATABASE [!0]")]
        public static Clause<Non> CreateDataBase(string name) { throw new InvalitContextException(nameof(CreateDataBase)); }

        /// <summary>
        /// DROP DATABASE clause.
        /// </summary>
        /// <param name="name">DataBase name.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "DROP DATABASE [!0]")]
        public static Clause<Non> DropDataBase(string name) { throw new InvalitContextException(nameof(DropDataBase)); }

        [MethodFormatConverter(Format = "Object_Id([%0])")]
        public static int Object_Id(object name) { return 0; }

        [MethodFormatConverter(Format = "SCHEMA_ID([%0])")]
        public static int? Schema_Id(object schema) => throw new InvalitContextException(nameof(Schema_Id));
        
        [ClauseStyleConverter(Name = "@@DBTS")]
        public static byte[] AtAtDbts() => throw new InvalitContextException(nameof(AtAtDbts));
    }




    class AllDisableBinaryExpressionBracketsCode : ICode, IDisableBinaryExpressionBrackets
    {
        ICode _core;

        internal AllDisableBinaryExpressionBracketsCode(ICode core)
        {
            _core = core;
        }

        public bool IsEmpty => _core.IsEmpty;

        public bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public string ToString(BuildingContext context) => _core.ToString(context);

        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;
            return new AllDisableBinaryExpressionBracketsCode(_core.Accept(customizer));
        }
    }


    class SubQueryAndNameCode : ICode
    {
        string _body;
        ICode _define;

        internal SubQueryAndNameCode(string body, ICode table)
        {
            _body = body;
            _define = new HCode(table, _body.ToCode()) { Separator = " ", EnableChangeLine = false };
        }

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context)
        {
            object obj;
            if (!context.UserData.TryGetValue(typeof(WithEntriedCode), out obj)) return _define.IsSingleLine(context);

            var withEntied = (Dictionary<string, bool>)obj;
            return withEntied.ContainsKey(_body) ? true : _define.IsSingleLine(context);
        }

        public string ToString(BuildingContext context)
        {
            object obj;
            if (!context.UserData.TryGetValue(typeof(WithEntriedCode), out obj)) return _define.ToString(context);

            var withEntied = (Dictionary<string, bool>)obj;
            return withEntied.ContainsKey(_body) ?
                    (PartsUtils.GetIndent(context.Indent) + _body) :
                    _define.ToString(context);
        }

        public ICode Accept(ICodeCustomizer customizer) => customizer.Visit(this);
    }

    class WithEntriedCode : ICode
    {
        ICode _core;
        string[] _names;

        internal WithEntriedCode(ICode core, string[] names)
        {
            _core = core;
            _names = names;
        }

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public string ToString(BuildingContext context)
        {
            Dictionary<string, bool> withEntied = null;
            object obj;
            if (context.UserData.TryGetValue(typeof(WithEntriedCode), out obj))
            {
                withEntied = (Dictionary<string, bool>)obj;
            }
            else
            {
                withEntied = new Dictionary<string, bool>();
                context.UserData[typeof(WithEntriedCode)] = withEntied;
            }

            foreach (var e in _names) withEntied[e] = true;
            return _core.ToString(context);
        }

        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;
            return new WithEntriedCode(_core.Accept(customizer), _names);
        }
    }

    internal class SelectQueryCode : ISelectQueryCode
    {
        public ICode Core { get; private set; }

        internal SelectQueryCode(ICode core)
        {
            Core = core;
        }

        public bool IsEmpty => Core.IsEmpty;

        public bool IsSingleLine(BuildingContext context) => Core.IsSingleLine(context);

        public string ToString(BuildingContext context)
        {
            var target = context.IsTopLevelQuery ? Core : new AroundCode(Core, "(", ")");
            return target.ToString(context.ChangeTopLevelQuery(false));
        }

        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;
            return new SelectQueryCode(Core.Accept(customizer));
        }

        public ISelectQueryCode Create(ICode core)
            => new SelectQueryCode(core);
    }

    class SelectClauseCode : ISelectQueryCode
    {
        public ICode Core { get; }

        internal SelectClauseCode(ICode core)
        {
            Core = core;
        }

        public bool IsEmpty => Core.IsEmpty;

        public bool IsSingleLine(BuildingContext context) => Core.IsSingleLine(context);

        public string ToString(BuildingContext context) => Core.ToString(context);

        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;
            return new SelectClauseCode(Core.Accept(customizer));
        }

        public ISelectQueryCode Create(ICode core)
            => new SelectQueryCode(core);
    }

    /// <summary>
    /// Converter for SELECT clause conversion.
    /// </summary>
    public class SelectConverterAttribute : MethodConverterAttribute
    {
        static readonly ICode SelectClause = "SELECT".ToCode();
        static readonly ICode AsClause = "AS".ToCode();

        /// <summary>
        /// Set when using names other than Select.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            //ALL, DISTINCT, TOP
            int expressionIndex = expression.SkipMethodChain(0);
            var selectParts = new ICode[expression.Arguments.Count - expressionIndex];
            selectParts[0] = string.IsNullOrEmpty(Name) ? SelectClause : Name.ToCode();
            for (int i = 0; i < selectParts.Length - 1; i++, expressionIndex++)
            {
                selectParts[i + 1] = converter.ConvertToCode(expression.Arguments[expressionIndex]);
            }

            var select = LineSpace(selectParts);

            //select elemnts.
            var selectTargets = expression.Arguments[expression.Arguments.Count - 1];

            //*
            if (typeof(AsteriskElement).IsAssignableFromEx(selectTargets.Type))
            {
                select.Add("*".ToCode());
                return new SelectClauseCode(select);
            }

            //new []{ a, b, c} recursive.
            else if (selectTargets.Type.IsArray)
            {
                var newArrayExp = selectTargets as NewArrayExpression;
                if (newArrayExp != null)
                {
                    var array = new ICode[newArrayExp.Expressions.Count];
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = converter.ConvertToCode(newArrayExp.Expressions[i]);
                    }

                    var coreCode = new VCode(select, new VCode(array) { Indent = 1, Separator = "," });
                    return new SelectClauseCode(coreCode);
                }
            }

            //new { item = db.tbl.column }
            {
                var createInfo = ObjectCreateAnalyzer.MakeObjectCreateInfo(selectTargets);
                var elements = new ICode[createInfo.Members.Length];
                for (int i = 0; i < elements.Length; i++)
                {
                    elements[i] = ConvertSelectedElement(converter, createInfo.Members[i]);
                }

                var coreCode = new VCode(select, new VCode(elements) { Indent = 1, Separator = "," });
                return new SelectClauseCode(coreCode);
            }
        }

        static ICode ConvertSelectedElement(ExpressionConverter converter, ObjectCreateMemberInfo element)
        {
            //single select.
            //for example, COUNT(*).
            if (string.IsNullOrEmpty(element.Name)) return converter.ConvertToCode(element.Expression);

            //normal select.
            var exp = converter.ConvertToCode(element.Expression);
            return exp.IsEmpty ? exp : LineSpace(exp, AsClause, element.Name.ToCode());
        }
    }


    /// <summary>
    /// Converter for ALL clause conversion.
    /// </summary>
    public class AllConverterAttribute : MethodConverterAttribute
    {
        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var args = expression.Arguments.Select(e => converter.ConvertToCode(e)).ToArray();
            return new AllDisableBinaryExpressionBracketsCode(Func("ALL".ToCode(), args[0]));
        }
    }

    /// <summary>
    /// Converter for WHERE and HAVING clause conversion.
    /// </summary>
    public class ConditionClauseConverterAttribute : MethodConverterAttribute
    {
        ICode _nameCode;
        string _name;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _nameCode = value.ToCode();
            }
        }

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var condition = converter.ConvertToCode(expression.Arguments[expression.SkipMethodChain(0)]);
            if (condition.IsEmpty) return string.Empty.ToCode();
            return Clause(_nameCode, condition);
        }
    }

    /// <summary>
    /// Converter for FROM clause conversion.
    /// </summary>
    public class FromConverterAttribute : MethodConverterAttribute
    {
        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var startIndex = expression.SkipMethodChain(0);
            var table = ConvertTable(converter, expression.Arguments[startIndex]);
            if (table.IsEmpty) return string.Empty.ToCode();
            return Clause("FROM".ToCode(), table);
        }

        internal static ICode ConvertTable(ExpressionConverter decoder, Expression exp)
        {
            //where query, write tables side by side.
            var arry = exp as NewArrayExpression;
            if (arry != null)
            {
                var multiTables = new ICode[arry.Expressions.Count];
                for (int i = 0; i < multiTables.Length; i++)
                {
                    multiTables[i] = ConvertTable(decoder, arry.Expressions[i]);
                }
                return Arguments(multiTables);
            }

            var table = decoder.ConvertToCode(exp);
            if (table.IsEmpty) return string.Empty.ToCode();

            //sub query.
            var body = GetSubQuery(exp);
            if (body != null) return new SubQueryAndNameCode(body, table);

            return table;
        }

        internal static string GetSubQuery(Expression exp)
        {
            var member = exp as MemberExpression;
            while (member != null)
            {
                if (typeof(Sql).IsAssignableFromEx(member.Type)) return member.Member.Name;
                member = member.Expression as MemberExpression;
            }
            return null;
        }
    }

    /// <summary>
    /// Converter for XXX JOIN clause conversion.
    /// </summary>
    public class JoinConverterAttribute : MethodConverterAttribute
    {
        ICode _nameCode;
        string _name;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _nameCode = value.ToCode();
            }
        }

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            //TODO ‚±‚ê‚¿‚å‚Á‚Æ‚¨‚©‚µ‚¢
            var startIndex = expression.SkipMethodChain(0);
            var table = FromConverterAttribute.ConvertTable(converter, expression.Arguments[startIndex]);
            if (table.IsEmpty) return string.Empty.ToCode();

            var condition = ((startIndex + 1) < expression.Arguments.Count) ? converter.ConvertToCode(expression.Arguments[startIndex + 1]) : null;

            var join = new HCode() { AddIndentNewLine = true, Separator = " ", Indent = 1 };
            join.Add(_nameCode);
            join.Add(table);
            if (condition != null)
            {
                join.Add("ON".ToCode());
                join.Add(condition);
            }
            return join;
        }
    }

    /// <summary>
    /// Data type.
    /// </summary>
    public abstract class DataTypeElement { }

    /// <summary>
    /// It's *.
    /// Used in Select clause and Count function.
    /// </summary>
    public abstract class AsteriskElement { }

    /// <summary>
    /// It's *.
    /// Used in Select clause and Count function.
    /// </summary>
    /// <typeparam name="T">It represents the type to select when used in the Select clause.</typeparam>
    public abstract class AsteriskElement<T> : AsteriskElement { }

    /// <summary>
    /// It is an object representing the sort order
    /// Implemented classes include Asc and Desc.
    /// </summary>
    public abstract class OrderByElement { }

    /// <summary>
    /// Aggregation predicate.
    /// ALL or DISTINCT.
    /// </summary>
    public abstract class AggregatePredicateElement { }

    /// <summary>
    /// Aggregation predicate.
    /// ALL
    /// </summary>
    public abstract class AggregatePredicateAllElement : AggregatePredicateElement { }

    /// <summary>
    /// OVER clause argument.
    /// </summary>
    public abstract class OverElement { }

    /// <summary>
    /// OVER clause result.
    /// </summary>
    public abstract class OverReturnValue : SqlExpression
    {
        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static object operator +(object value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static string operator +(string value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static bool operator +(bool value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static bool? operator +(bool? value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static byte operator +(byte value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static byte? operator +(byte? value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static short operator +(short value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static short? operator +(short? value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static int operator +(int value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static int? operator +(int? value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static long operator +(long value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static long? operator +(long? value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static float operator +(float value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static float? operator +(float? value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static double operator +(double value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static double? operator +(double? value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static decimal operator +(decimal value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static decimal? operator +(decimal? value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static DateTime operator +(DateTime value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static DateTime? operator +(DateTime? value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static DateTimeOffset operator +(DateTimeOffset value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static DateTimeOffset? operator +(DateTimeOffset? value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static TimeSpan operator +(TimeSpan value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static TimeSpan? operator +(TimeSpan? value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static byte[] operator +(byte[] value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static char[] operator +(char[] value, OverReturnValue returnValue) { throw new InvalitContextException("additional operator"); }
    }

    /// <summary>
    /// Data type.
    /// It can only be used within lambda of the LambdicSql.
    /// </summary>
    public static class DataType
    {
        /// <summary>
        /// INT
        /// </summary>
        /// <returns>INT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Int() { throw new InvalitContextException(nameof(Int)); }

        /// <summary>
        /// CHAR
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>CHAR</returns>
        [FuncStyleConverter]
        public static DataTypeElement Char(int n) { throw new InvalitContextException(nameof(Char)); }
    }

    /// <summary>
    /// Table definition item.
    /// </summary>
    public abstract class TableDefinitionElement { }

    /// <summary>
    /// Constraint object.
    /// </summary>
    public abstract class ConstraintElement : TableDefinitionElement { }

    /// <summary>
    /// Column definition.
    /// It can only be used within lambda of the LambdicSql.
    /// </summary>
    public class Column : TableDefinitionElement
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="column">Column.</param>
        /// <param name="type">Type.</param>
        [NewFormatConverter(Format = "[0] [1]")]
        public Column(object column, DataTypeElement type) { throw new InvalitContextException("new " + nameof(Column)); }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="column">Column.</param>
        /// <param name="type">Type.</param>
        /// <param name="constraints">Constraints.</param>
        [NewFormatConverter(Format = "[0] [1] [< >2]")]
        public Column(object column, DataTypeElement type, params ConstraintElement[] constraints) { throw new InvalitContextException("new " + nameof(Column)); }
    }

    /// <summary>
    /// It represents assignment. It is used in the Set clause.
    /// new Assign(db.tbl_staff.name, name) -> tbl_staff.name = "@name"
    /// It can only be used within lambda of the LambdicSql.
    /// </summary>
    public class Assign
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rhs">Rvalue</param>
        /// <param name="lhs">Lvalue</param>
        [NewFormatConverter(Format = "[#0] = [1]")]
        public Assign(object rhs, object lhs) { throw new InvalitContextException("new " + nameof(Assign)); }
    }

    static class PartsUtils
    {
        internal static string GetIndent(int indent)
        {
            switch (indent)
            {
                case 0: return string.Empty;
                case 1: return "\t";
                case 2: return "\t\t";
                case 3: return "\t\t\t";
                case 4: return "\t\t\t\t";
                case 5: return "\t\t\t\t\t";
                case 6: return "\t\t\t\t\t\t";
                case 7: return "\t\t\t\t\t\t\t";
                case 8: return "\t\t\t\t\t\t\t\t";
                case 9: return "\t\t\t\t\t\t\t\t\t";
                case 10: return "\t\t\t\t\t\t\t\t\t\t";
            }

            var array = new char[indent];
            for (int i = 0; i < indent; i++)
            {
                array[i] = '\t';
            }
            return new string(array);
        }

        internal static int SkipMethodChain(this MethodCallExpression exp, int index)
        {
            if (exp.Arguments.Count == 0) return index;
            return typeof(Clause).IsAssignableFromEx(exp.Arguments[0].Type) ? index + 1 : index;
        }
    }
    static class PartsFactoryUtils
    {
        internal static HCode Arguments(params ICode[] args)
            => new HCode(args) { Separator = ", " };

        internal static ICode Blanket(params ICode[] args)
            => new AroundCode(Arguments(args), "(", ")");

        internal static HCode Func(ICode func, params ICode[] args)
            => Func(func, ", ", args);

        internal static HCode Clause(ICode clause, params ICode[] args)
        {
            var code = new HCode() { AddIndentNewLine = true, Separator = " " };
            code.Add(clause);
            code.AddRange(args);
            return code;
        }

        internal static HCode Line(params ICode[] args)
            => new HCode(args) { EnableChangeLine = false };

        internal static HCode LineSpace(params ICode[] args)
             => new HCode(args) { EnableChangeLine = false, Separator = " " };

        static HCode Func(ICode func, string separator, params ICode[] args)
        {
            var hArgs = new AroundCode(new HCode(args) { Separator = separator }, "", ")");
            return new HCode(Line(func, "(".ToCode()), hArgs) { AddIndentNewLine = true };
        }

        internal static SingleTextCode ToCode(this string src)
            => new SingleTextCode(src);
    }
}
