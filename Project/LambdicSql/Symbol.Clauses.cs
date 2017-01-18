using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Specialized.SymbolConverters;

namespace LambdicSql
{
    /// <summary>
    /// SQL Symbol.
    /// It can only be used within methods of the LambdicSql.Db class.
    /// Use[using static LambdicSql.Keywords;], you can use to write natural SQL.
    /// </summary>
    public static partial class Symbol
    {
        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(TSelected selected) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, TSelected selected) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(ITop top, TSelected selected) { throw new InvalitContextException(nameof(Select)); }

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
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, ITop top, TSelected selected) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select(IAsterisk asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select<TSrcSelected>(this ClauseChain<TSrcSelected> before, IAsterisk asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(IAsterisk<TSelected> asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAsterisk<TSelected> asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select(ITop top, IAsterisk asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select<TSrcSelected>(this ClauseChain<TSrcSelected> before, ITop top, IAsterisk asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(ITop top, IAsterisk<TSelected> asterisk) { throw new InvalitContextException(nameof(Select)); }

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
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, ITop top, IAsterisk<TSelected> asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(IAggregatePredicate predicate, TSelected selected) { throw new InvalitContextException(nameof(Select)); }

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
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, TSelected selected) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="selected">Specify a new expression to represent the selection.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(IAggregatePredicate predicate, ITop top, TSelected selected) { throw new InvalitContextException(nameof(Select)); }

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
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, ITop top, TSelected selected) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select(IAggregatePredicate predicate, IAsterisk asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected">If there is a Select clause before, it is the type selected there. Normally, the Select clause does not exist before the Select clause, and object is specified.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select<TSrcSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, IAsterisk asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(IAggregatePredicate predicate, IAsterisk<TSelected> asterisk) { throw new InvalitContextException(nameof(Select)); }

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
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, IAsterisk<TSelected> asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<Non> Select(IAggregatePredicate predicate, ITop top, IAsterisk asterisk) { throw new InvalitContextException(nameof(Select)); }

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
        public static ClauseChain<Non> Select<TSrcSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, ITop top, IAsterisk asterisk) { throw new InvalitContextException(nameof(Select)); }

        /// <summary>
        /// SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate">ALL or DISTINCT.</param>
        /// <param name="top">TOP keyword.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [SelectConverter]
        public static ClauseChain<TSelected> Select<TSelected>(IAggregatePredicate predicate, ITop top, IAsterisk<TSelected> asterisk) { throw new InvalitContextException(nameof(Select)); }

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
        public static ClauseChain<TSelected> Select<TSrcSelected, TSelected>(this ClauseChain<TSrcSelected> before, IAggregatePredicate predicate, ITop top, IAsterisk<TSelected> asterisk) { throw new InvalitContextException(nameof(Select)); }

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
        [ClauseStyleConverter]
        public static ICaseAfter Case() { throw new InvalitContextException(nameof(Case)); }

        /// <summary>
        /// CASE clause.
        /// </summary>
        /// <param name="target">It's target of CASE branch.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [ClauseStyleConverter]
        public static ICaseAfter Case(object target) { throw new InvalitContextException(nameof(Case)); }

        /// <summary>
        /// WHEN clause.
        /// </summary>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="expression">It is a conditional expression of the WHEN clause.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [ClauseStyleConverter(Indent =1)]
        public static IWhenAfter When(this ICaseAfter before, object expression) { throw new InvalitContextException(nameof(When)); }

        /// <summary>
        /// WHEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="expression">It is a conditional expression of the WHEN clause.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [ClauseStyleConverter(Indent = 1)]
        public static IWhenAfter<T> When<T>(this IThenAfter<T> before, object expression) { throw new InvalitContextException(nameof(When)); }

        /// <summary>
        /// THEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the THEN clause is valid.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [ClauseStyleConverter(Indent = 1)]
        public static IThenAfter<T> Then<T>(this IWhenAfter before, T result) { throw new InvalitContextException(nameof(Then)); }

        /// <summary>
        /// THEN clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the THEN clause is valid.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [ClauseStyleConverter(Indent = 1)]
        public static IThenAfter<T> Then<T>(this IWhenAfter<T> before, T result) { throw new InvalitContextException(nameof(Then)); }

        /// <summary>
        /// ELSE clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <param name="result">It is an item to return to when the ELSE clause is valid.</param>
        /// <returns>It is an object for describing the continuation of the CASE expression.</returns>
        [ClauseStyleConverter(Indent = 1)]
        public static IElseAfter<T> Else<T>(this IThenAfter<T> before, T result) { throw new InvalitContextException(nameof(Then)); }

        /// <summary>
        /// END clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <returns>It is the result of CASE expression.</returns>
        [ClauseStyleConverter]
        public static T End<T>(this IThenAfter<T> before) { throw new InvalitContextException(nameof(End)); }

        /// <summary>
        /// END clause.
        /// </summary>
        /// <typeparam name="T">Type represented by CASE expression.</typeparam>
        /// <param name="before">It is an before expression in the CASE clause.</param>
        /// <returns>It is the result of CASE expression.</returns>
        [ClauseStyleConverter]
        public static T End<T>(this IElseAfter<T> before) { throw new InvalitContextException(nameof(End)); }

        /// <summary>
        /// FROM clause.
        /// </summary>
        /// <param name="expressions">Table or subquery.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [FromConverter]
        public static ClauseChain<Non> From(params object[] expressions) { throw new InvalitContextException(nameof(From)); }

        /// <summary>
        /// FROM clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="tables">Table or subquery.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [FromConverter]
        public static ClauseChain<TSelected> From<TSelected>(this ClauseChain<TSelected> before, params object[] tables) { throw new InvalitContextException(nameof(From)); }

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "JOIN")]
        public static ClauseChain<Non> Join(object table, bool condition) { throw new InvalitContextException(nameof(Join)); }

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "JOIN")]
        public static ClauseChain<TSelected> Join<TSelected>(this ClauseChain<TSelected> before, object table, bool condition) { throw new InvalitContextException(nameof(Join)); }

        /// <summary>
        /// JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of LEFT JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "LEFT JOIN")]
        public static ClauseChain<Non> LeftJoin(object table, bool condition) { throw new InvalitContextException(nameof(LeftJoin)); }

        /// <summary>
        /// LEFT JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of LEFT JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "LEFT JOIN")]
        public static ClauseChain<TSelected> LeftJoin<TSelected>(this ClauseChain<TSelected> before, object table, bool condition) { throw new InvalitContextException(nameof(LeftJoin)); }

        /// <summary>
        /// RIGHT JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "RIGHT JOIN")]
        public static ClauseChain<Non> RightJoin(object table, bool condition) { throw new InvalitContextException(nameof(RightJoin)); }

        /// <summary>
        /// RIGHT JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "RIGHT JOIN")]
        public static ClauseChain<TSelected> RightJoin<TSelected>(this ClauseChain<TSelected> before, object table, bool condition) { throw new InvalitContextException(nameof(RightJoin)); }

        /// <summary>
        /// FULL JOIN clause.
        /// </summary>
        /// <param name="table">Table or subquery.</param>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "FULL JOIN")]
        public static ClauseChain<Non> FullJoin(object table, bool condition) { throw new InvalitContextException(nameof(FullJoin)); }

        /// <summary>
        /// FULL JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>>
        /// <param name="condition">It is a condition of RIGHT JOIN.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "FULL JOIN")]
        public static ClauseChain<TSelected> FullJoin<TSelected>(this ClauseChain<TSelected> before, object table, bool condition) { throw new InvalitContextException(nameof(FullJoin)); }

        /// <summary>
        /// CROSS JOIN clause.
        /// </summary>
        /// <param name="expression">Table or subquery.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "CROSS JOIN")]
        public static ClauseChain<Non> CrossJoin(object expression) { throw new InvalitContextException(nameof(CrossJoin)); }

        /// <summary>
        /// CROSS JOIN clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table or subquery.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [JoinConverter(Name = "CROSS JOIN")]
        public static ClauseChain<TSelected> CrossJoin<TSelected>(this ClauseChain<TSelected> before, object table) { throw new InvalitContextException(nameof(CrossJoin)); }

        /// <summary>
        /// WHERE clause.
        /// </summary>
        /// <param name="condition">It is a conditional expression of WHERE.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ConditionClauseConverter(Name = "WHERE")]
        public static ClauseChain<Non> Where(bool condition) { throw new InvalitContextException(nameof(Where)); }

        /// <summary>
        /// WHERE clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="condition">It is a conditional expression of WHERE.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ConditionClauseConverter(Name = "WHERE")]
        public static ClauseChain<TSelected> Where<TSelected>(this ClauseChain<TSelected> before, bool condition) { throw new InvalitContextException(nameof(Where)); }

        /// <summary>
        /// GROUP BY clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "GROUP BY |[<, >0]")]
        public static ClauseChain<Non> GroupBy(params object[] columns) { throw new InvalitContextException(nameof(GroupBy)); }

        /// <summary>
        /// GROUP BY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "GROUP BY |[<, >1]")]
        public static ClauseChain<TSelected> GroupBy<TSelected>(this ClauseChain<TSelected> before, params object[] columns) { throw new InvalitContextException(nameof(GroupBy)); }

        /// <summary>
        /// GROUP BY ROLLUP clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "GROUP BY ROLLUP(|[<, >0])")]
        public static ClauseChain<Non> GroupByRollup(params object[] columns) { throw new InvalitContextException(nameof(GroupByRollup)); }

        /// <summary>
        /// GROUP BY ROLLUP clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "GROUP BY ROLLUP(|[<, >1])")]
        public static ClauseChain<TSelected> GroupByRollup<TSelected>(this ClauseChain<TSelected> before, params object[] columns) { throw new InvalitContextException(nameof(GroupByRollup)); }

        /// <summary>
        /// GROUP BY columns WITH ROLLUP clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        /// 
        [MethodFormatConverter(Format = "GROUP BY |[<, >0] WITH ROLLUP")]
        public static ClauseChain<Non> GroupByWithRollup(params object[] columns) { throw new InvalitContextException(nameof(GroupByRollup)); }

        /// <summary>
        /// GROUP BY columns WITH ROLLUP clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "GROUP BY |[<, >1] WITH ROLLUP")]
        public static ClauseChain<TSelected> GroupByWithRollup<TSelected>(this ClauseChain<TSelected> before, params object[] columns) { throw new InvalitContextException(nameof(GroupByRollup)); }

        /// <summary>
        /// GROUP BY CUBE clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "GROUP BY CUBE(|[<, >0])")]
        public static ClauseChain<Non> GroupByCube(params object[] columns) { throw new InvalitContextException(nameof(GroupByCube)); }

        /// <summary>
        /// GROUP BY CUBE clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "GROUP BY CUBE(|[<, >1])")]
        public static ClauseChain<TSelected> GroupByCube<TSelected>(this ClauseChain<TSelected> before, params object[] columns) { throw new InvalitContextException(nameof(GroupByCube)); }

        /// <summary>
        /// GROUP BY GROUPING SETS clause.
        /// </summary>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "GROUP BY GROUPING SETS(|[<, >0])")]
        public static ClauseChain<Non> GroupByGroupingSets(params object[] columns) { throw new InvalitContextException(nameof(GroupByGroupingSets)); }

        /// <summary>
        /// GROUP BY GROUPING SETS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Specify the target column of GROUP BY.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "GROUP BY GROUPING SETS(|[<, >1])")]
        public static ClauseChain<TSelected> GroupByGroupingSets<TSelected>(this ClauseChain<TSelected> before, params object[] columns) { throw new InvalitContextException(nameof(GroupByGroupingSets)); }

        /// <summary>
        /// HAVING clause.
        /// </summary>
        /// <param name="condition">It is a conditional expression of HAVING.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ConditionClauseConverter(Name = "HAVING")]
        public static ClauseChain<Non> Having(bool condition) { throw new InvalitContextException(nameof(Having)); }

        /// <summary>
        /// HAVING clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="condition">It is a conditional expression of HAVING.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ConditionClauseConverter(Name = "HAVING")]
        public static ClauseChain<TSelected> Having<TSelected>(this ClauseChain<TSelected> before, bool condition) { throw new InvalitContextException(nameof(Having)); }

        /// <summary>
        /// ORDER BY clause.
        /// </summary>
        /// <param name="elements">Specify column and sort order. Asc(column) or Desc(column).</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "ORDER BY |[<, >0]", FormatDirection = FormatDirection.Vertical)]
        public static OrderBy OrderBy(params ISortedBy[] elements) { throw new InvalitContextException(nameof(OrderBy)); }

        /// <summary>
        /// ORDER BY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="elements">Specify column and sort order. Asc(column) or Desc(column).</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "ORDER BY |[<, >1]", FormatDirection = FormatDirection.Vertical)]
        public static ClauseChain<TSelected> OrderBy<TSelected>(this ClauseChain<TSelected> before, params ISortedBy[] elements) { throw new InvalitContextException(nameof(OrderBy)); }

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<Non> Limit(object count) { throw new InvalitContextException(nameof(Limit)); }

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<TSelected> Limit<TSelected>(this ClauseChain<TSelected> before, object count) { throw new InvalitContextException(nameof(Limit)); }

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <param name="offset">Start position.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format ="LIMIT |[0], [1]")]
        public static ClauseChain<Non> Limit(object offset, object count) { throw new InvalitContextException(nameof(Limit)); }

        /// <summary>
        /// LIMIT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="offset">Start position.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "LIMIT |[1], [2]")]
        public static ClauseChain<TSelected> Limit<TSelected>(this ClauseChain<TSelected> before, object offset, object count) { throw new InvalitContextException(nameof(Limit)); }

        /// <summary>
        /// OFFSET clause.
        /// </summary>
        /// <param name="offset">Start position.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<Non> Offset(object offset) { throw new InvalitContextException(nameof(Offset)); }

        /// <summary>
        /// OFFSET clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="offset">Start position.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<TSelected> Offset<TSelected>(this ClauseChain<TSelected> before, object offset) { throw new InvalitContextException(nameof(Offset)); }

        /// <summary>
        /// OFFSET count ROWS clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "OFFSET |[0] ROWS")]
        public static ClauseChain<Non> OffsetRows(object count) { throw new InvalitContextException(nameof(OffsetRows)); }

        /// <summary>
        /// OFFSET count ROWS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "OFFSET |[1] ROWS")]
        public static ClauseChain<TSelected> OffsetRows<TSelected>(this ClauseChain<TSelected> before, object count) { throw new InvalitContextException(nameof(OffsetRows)); }

        /// <summary>
        /// FETCH NEXT count ROWS ONLY clause.
        /// </summary>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "FETCH NEXT |[0] ROWS ONLY")]
        public static ClauseChain<Non> FetchNextRowsOnly(object count) { throw new InvalitContextException(nameof(FetchNextRowsOnly)); }

        /// <summary>
        /// FETCH NEXT count ROWS ONLY clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="count">Number of rows to acquire.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "FETCH NEXT |[1] ROWS ONLY")]
        public static ClauseChain<TSelected> FetchNextRowsOnly<TSelected>(this ClauseChain<TSelected> before, object count) { throw new InvalitContextException(nameof(FetchNextRowsOnly)); }

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<Non> Union() { throw new InvalitContextException(nameof(Union)); }

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<TSelected> Union<TSelected>(this ClauseChain<TSelected> before) { throw new InvalitContextException(nameof(Union)); }

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<Non> Union(IAggregatePredicateAll all) { throw new InvalitContextException(nameof(Union)); }

        /// <summary>
        /// UNION clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<TSelected> Union<TSelected>(this ClauseChain<TSelected> before, IAggregatePredicateAll all) { throw new InvalitContextException(nameof(Union)); }

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<Non> Intersect() { throw new InvalitContextException(nameof(Intersect)); }

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<TSelected> Intersect<TSelected>(this ClauseChain<TSelected> before) { throw new InvalitContextException(nameof(Intersect)); }

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<Non> Intersect(IAggregatePredicateAll all) { throw new InvalitContextException(nameof(Intersect)); }

        /// <summary>
        /// INTERSECT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<TSelected> Intersect<TSelected>(this ClauseChain<TSelected> before, IAggregatePredicateAll all) { throw new InvalitContextException(nameof(Intersect)); }

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<Non> Except() { throw new InvalitContextException(nameof(Except)); }

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<TSelected> Except<TSelected>(this ClauseChain<TSelected> before) { throw new InvalitContextException(nameof(Except)); }

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<Non> Except(IAggregatePredicateAll all) { throw new InvalitContextException(nameof(Except)); }

        /// <summary>
        /// EXCEPT clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<TSelected> Except<TSelected>(this ClauseChain<TSelected> before, IAggregatePredicateAll all) { throw new InvalitContextException(nameof(Except)); }

        /// <summary>
        /// MINUS clause.
        /// </summary>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<Non> Minus() { throw new InvalitContextException(nameof(Minus)); }

        /// <summary>
        /// MINUS clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<TSelected> Minus<TSelected>(this ClauseChain<TSelected> before) { throw new InvalitContextException(nameof(Minus)); }

        /// <summary>
        /// UPDATE clause.
        /// </summary>
        /// <param name="table">Table for UPDATE.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<Non> Update(object table) { throw new InvalitContextException(nameof(Update)); }

        //TODO test
        /// <summary>
        /// SET clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="assigns">Assignment in the SET clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "SET |[<,>0]", FormatDirection = FormatDirection.Vertical)]
        public static ClauseChain<TSelected> Set<TSelected>(params Assign[] assigns) { throw new InvalitContextException(nameof(Set)); }
        
        /// <summary>
        /// SET clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="assigns">Assignment in the SET clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "SET |[<,>1]", FormatDirection = FormatDirection.Vertical)]
        public static ClauseChain<TSelected> Set<TSelected>(this ClauseChain<TSelected> before, params Assign[] assigns) { throw new InvalitContextException(nameof(Set)); }

        /// <summary>
        /// DELETE clause.
        /// </summary>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<Non> Delete() { throw new InvalitContextException(nameof(Delete)); }

        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <param name="table">Table for INSERT.</param>
        /// <param name="columns">It is a column that performs INSERT.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "INSERT INTO [0](|[#<, >1])")]
        public static ClauseChain<Non> InsertInto(object table, params object[] columns) { throw new InvalitContextException(nameof(InsertInto)); }

        //TODO test
        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="values">It is the value to be Inserted.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "VALUES([<, >1])", Indent = 0)]
        public static ClauseChain<TSelected> Values<TSelected>(params object[] values) { throw new InvalitContextException(nameof(Values)); }

        /// <summary>
        /// INSERT INTO clause.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="values">It is the value to be Inserted.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "VALUES([<, >1])", Indent = 1)]
        public static ClauseChain<TSelected> Values<TSelected>(this ClauseChain<TSelected> before, params object[] values) { throw new InvalitContextException(nameof(Values)); }

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
        /// <returns></returns>
        [ClauseStyleConverter]
        public static IAggregatePredicateAll All() => null;

        /// <summary>
        /// Distinct Keyword
        /// </summary>
        /// <returns></returns>
        [ClauseStyleConverter]
        public static IAggregatePredicate Distinct() { throw new InvalitContextException(nameof(All)); }

        /// <summary>
        /// ALL Keyword
        /// </summary>
        /// <typeparam name="T">Retunn type.</typeparam>
        /// <param name="sub">Sub query.</param>
        /// <returns>Sub query's selected value.</returns>
        [AllConverter]
        public static T All<T>(object sub) { throw new InvalitContextException(nameof(All)); }

        /// <summary>
        /// EXISTS keyword.
        /// </summary>
        /// <param name="expression">Sub query.</param>
        /// <returns>Returns TRUE if there is at least one record returned by expression, FALSE otherwise.</returns>
        [ClauseStyleConverter]
        public static bool Exists(object expression) { throw new InvalitContextException(nameof(Exists)); }

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
        public static ClauseChain<Non> With(params Sql[] subQuerys) { throw new InvalitContextException(nameof(With)); }

        /// <summary>
        /// WITH clause.
        /// </summary>
        /// <typeparam name="T">Type representing argument of recursive part.</typeparam>
        /// <param name="args">Argument of recursive part.</param>
        /// <param name="subQuery">sub query.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [WithConverter]
        public static ClauseChain<T> With<T>(SqlRecursiveArguments<T> args, Sql subQuery) { throw new InvalitContextException(nameof(With)); }

        /// <summary>
        /// RECURSIVE clause.
        /// </summary>
        /// <typeparam name="T">Type representing argument of recursive part.</typeparam>
        /// <param name="args">Argument of recursive part.</param>
        /// <returns>Class representing argument of recursive part.</returns>
        [RecursiveConverter]
        public static RecursiveArguments<T> Recursive<T>(T args) { throw new InvalitContextException(nameof(Select)); }

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


        //TODO なかったら作ると、あったら消すを全DBでできるようにしておくか

        //TODO test.
        /// <summary>
        /// CREATE TABLE clause.
        /// </summary>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "CREATE TABLE [0](|[#$<,>1])", FormatDirection = FormatDirection.Vertical)]
        public static ClauseChain<Non> CreateTable(object table, params ITableDefinition[] designer) { throw new InvalitContextException(nameof(CreateTable)); }

        /// <summary>
        /// CREATE TABLE clause.
        /// </summary>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "CREATE TABLE IF NOT EXISTS [0](|[#$<,>1])", FormatDirection = FormatDirection.Vertical)]
        public static ClauseChain<Non> CreateTableIfNotExists(object table, params ITableDefinition[] designer) { throw new InvalitContextException(nameof(CreateTable)); }
        
        /// <summary>
        /// CONSTRAINT clause.
        /// </summary>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static IConstraint Constraint(this ClauseChain<Non> before) { throw new InvalitContextException(nameof(Constraint)); }

        /// <summary>
        /// CONSTRAINT clause.
        /// </summary>
        /// <param name="name">Constraint name.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "CONSTRAINT [!0]")]
        public static IConstraint Constraint(string name) { throw new InvalitContextException(nameof(Constraint)); }

        /// <summary>
        /// PRIMARY KEY clause.
        /// </summary>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter(Name = "PRIMARY KEY")]
        public static IConstraint PrimaryKey() { throw new InvalitContextException(nameof(PrimaryKey)); }

        /// <summary>
        /// PRIMARY KEY clause.
        /// </summary>
        /// <param name="columns">Columns.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "PRIMARY KEY([<, >0])")]
        public static IConstraint PrimaryKey(params object[] columns) { throw new InvalitContextException(nameof(PrimaryKey)); }

        /// <summary>
        /// PRIMARY KEY clause.
        /// </summary>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Columns.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "PRIMARY KEY([<, >1])")]
        public static IConstraint PrimaryKey(this IConstraint before, params object[] columns) { throw new InvalitContextException(nameof(PrimaryKey)); }

        /// <summary>
        /// CHECK clause.
        /// </summary>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="condition">Condition.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [FuncStyleConverter(Indent = 1)]
        public static IConstraint Check(this IConstraint before, bool condition) { throw new InvalitContextException(nameof(Check)); }

        /// <summary>
        /// UNIQUE clause.
        /// </summary>
        /// <param name="columns">Columns.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "UNIQUE([<, >0])")]
        public static IConstraint Unique(params object[] columns) { throw new InvalitContextException(nameof(Unique)); }

        /// <summary>
        /// UNIQUE clause.
        /// </summary>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Columns.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "UNIQUE([<, >1])")]
        public static IConstraint Unique(this IConstraint before, params object[] columns) { throw new InvalitContextException(nameof(Unique)); }

        /// <summary>
        /// FOREIGN KEY clause.
        /// </summary>
        /// <param name="columns">Columns.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "FOREIGN KEY([<, >0])")]
        public static IConstraint ForeignKey(params object[] columns) { throw new InvalitContextException(nameof(ForeignKey)); }

        /// <summary>
        /// FOREIGN KEY clause.
        /// </summary>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="columns">Columns.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "FOREIGN KEY([<, >1])")]
        public static IConstraint ForeignKey(this IConstraint before, params object[] columns) { throw new InvalitContextException(nameof(ForeignKey)); }
        
        /// <summary>
        /// NOT NULL
        /// </summary>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter(Name = "NOT NULL")]
        public static IConstraint NotNull() { throw new InvalitContextException(nameof(NotNull)); }

        /// <summary>
        /// DEFAULT
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static IConstraint Default(object value) { throw new InvalitContextException(nameof(NotNull)); }

        /// <summary>
        /// REFERENCES clause.
        /// </summary>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="table">Table.</param>
        /// <param name="columns">Columns.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "REFERENCES [1](|[<, >2])", Indent = 1)]
        public static IConstraint References(this IConstraint before, object table, params object[] columns) { throw new InvalitContextException(nameof(References)); }

        /// <summary>
        /// RESTRICT clause.
        /// </summary>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<Non> Restrict(this ClauseChain<Non> before) { throw new InvalitContextException(nameof(Restrict)); }

        /// <summary>
        /// CASCADE clause.
        /// </summary>
        /// <param name="before">It is the previous clause.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<Non> Cascade(this ClauseChain<Non> before) { throw new InvalitContextException(nameof(Cascade)); }

        /// <summary>
        /// CASCADE clause.
        /// </summary>
        /// <param name="before">It is the previous clause.</param>
        /// <param name="constraint">Constraint.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [ClauseStyleConverter]
        public static ClauseChain<Non> Cascade(this ClauseChain<Non> before, IConstraint constraint) { throw new InvalitContextException(nameof(Cascade)); }

        /// <summary>
        /// DROP TABLE clause.
        /// </summary>
        /// <param name="tables">Tables.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "DROP TABLE [<, >0]")]
        public static ClauseChain<Non> DropTable(params object[] tables) { throw new InvalitContextException(nameof(DropTable)); }

        /// <summary>
        /// DROP TABLE IF EXISTS clause.
        /// </summary>
        /// <param name="tables">Tables.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "DROP TABLE IF EXISTS [<, >0]")]
        public static ClauseChain<Non> DropTableIfExists(params object[] tables) { throw new InvalitContextException(nameof(DropTable)); }

        /// <summary>
        /// DROP DATABASE clause.
        /// </summary>
        /// <param name="name">DataBase name.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "CREATE DATABASE [!0]")]
        public static ClauseChain<Non> CreateDataBase(string name) { throw new InvalitContextException(nameof(CreateDataBase)); }

        /// <summary>
        /// DROP DATABASE clause.
        /// </summary>
        /// <param name="name">DataBase name.</param>
        /// <returns>Clause chain. You can write SQL statements in succession, of course you can end it.</returns>
        [MethodFormatConverter(Format = "DROP DATABASE [!0]")]
        public static ClauseChain<Non> DropDataBase(string name) { throw new InvalitContextException(nameof(CreateDataBase)); }
    }
}
