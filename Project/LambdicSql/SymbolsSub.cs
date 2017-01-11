﻿using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CustomSymbolConverters;

namespace LambdicSql
{    
    /// <summary>
    /// 
    /// </summary>
    public abstract class Non { }

    /// <summary>
    /// Clause chain.
    /// </summary>
    /// <typeparam name="TSelected"></typeparam>
    public abstract class ClauseChain<TSelected> : IMethodChain
    {
        /// <summary>
        /// Implicitly convert to the type represented by SqlExpression.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        /// <param name="src"></param>
        public static implicit operator TSelected(ClauseChain<TSelected> src) => InvalitContext.Throw<TSelected>("implicit operator");
    }

    /// <summary>
    /// Data type.
    /// </summary>
    public interface IDataType { }

    /// <summary>
    /// TOP keyword.
    /// </summary>
    public interface ITop { }

    /// <summary>
    /// It's *.
    /// Used in Select clause and Count function.
    /// </summary>
    public interface IAsterisk { }

    /// <summary>
    /// It's *.
    /// Used in Select clause and Count function.
    /// </summary>
    /// <typeparam name="T">It represents the type to select when used in the Select clause.</typeparam>
    public interface IAsterisk<T> : IAsterisk { }

    /// <summary>
    /// It is an object representing the sort order
    /// Implemented classes include Asc and Desc.
    /// </summary>
    public interface ISortedBy { }

    /// <summary>
    /// ORDERBY keyword.
    /// Use it with the OVER function.
    /// </summary>
    public interface IOrderBy { }

    /// <summary>
    /// ORDERBY keyword.
    /// Use it with the OVER function.
    /// </summary>
    public class OrderBy : ClauseChain<object>, IOrderBy
    {
        OrderBy() { }
    }

    /// <summary>
    /// PARTITION BY keyword.
    /// Use it with the OVER function.
    /// </summary>
    public interface IPartitionBy { }

    /// <summary>
    /// Rows keyword.
    /// Use it with the OVER function.
    /// </summary>
    public interface IRows { }

    /// <summary>
    /// Aggregation predicate.
    /// All or Distinct.
    /// </summary>
    public interface IAggregatePredicate { }

    /// <summary>
    /// Aggregation predicate.
    /// All or Distinct.
    /// </summary>
    public interface IAggregatePredicateAll : IAggregatePredicate { }

    /// <summary>
    /// Element of DateTime.
    /// </summary>
    [KeywordObjectConverter]
    public enum DateTimeElement
    {
        /// <summary>
        /// Year.
        /// </summary>
        Year,

        /// <summary>
        /// Quarter.
        /// </summary>
        Quarter,

        /// <summary>
        /// Month.
        /// </summary>
        Month,

        /// <summary>
        /// Dayofyear.
        /// </summary>
        Dayofyear,

        /// <summary>
        /// Day.
        /// </summary>
        Day,

        /// <summary>
        /// Week.
        /// </summary>
        Week,

        /// <summary>
        /// Weekday.
        /// </summary>
        Weekday,

        /// <summary>
        /// Hour.
        /// </summary>
        Hour,

        /// <summary>
        /// Minute.
        /// </summary>
        Minute,

        /// <summary>
        /// Second.
        /// </summary>
        Second,

        /// <summary>
        /// Millisecond.
        /// </summary>
        Millisecond,

        /// <summary>
        /// Microsecond.
        /// </summary>
        Microsecond,

        /// <summary>
        /// Nanosecond.
        /// </summary>
        Nanosecond,

        /// <summary>
        /// ISO_WEEK.
        /// </summary>
        ISO_WEEK,
    }

    /// <summary>
    /// It represents assignment. It is used in the Set clause.
    /// new Assign(db.tbl_staff.name, name) -> tbl_staff.name = "@name"
    /// </summary>
    public class Assign
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rhs">Rvalue</param>
        /// <param name="lhs">Lvalue</param>
        [AssignConverter]
        public Assign(object rhs, object lhs) { InvalitContext.Throw("new " + nameof(Assign)); }
    }

    /// <summary>
    /// Condition building helper.
    /// condition is used if enable is valid.
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// Condition building helper.
        /// condition is used if enable is valid.
        /// </summary>
        /// <param name="enable">Whether condition is valid.</param>
        /// <param name="condition">Condition expression.</param>
        [ConditionConverter]
        public Condition(object enable, object condition) { InvalitContext.Throw("new " + nameof(Condition)); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        public static implicit operator bool(Condition src) => InvalitContext.Throw<bool>("implicit operator bool(Condition src)");
    }

    /// <summary>
    /// SYSIBM keyword.
    /// </summary>
    public class SysIBM
    {
        internal SysIBM() { }

        /// <summary>
        /// SYSDUMMY1 keyword.
        /// </summary>
        [KeywordMemberConverter(Name = "SYSIBM.SYSDUMMY1")]
        public static object SysDummy1 => InvalitContext.Throw<long>(nameof(SysDummy1));
    }

    /// <summary>
    /// Utility.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// </summary>
    public static class UtilityKeywordExtensions
    {
        /// <summary>
        /// Put the text in the expression of LamblicSql.
        /// </summary>
        /// <param name="text">Text.You can use the same format as System.String's Format method.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>LamblicSql's expression.</returns>
        [ToSqlConverter]
        public static object ToSql(this string text, params object[] args) => InvalitContext.Throw<object>(nameof(ToSql));

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
        [TwoWaySqlConverter]
        public static object TwoWaySql(this string text, params object[] args) => InvalitContext.Throw<object>(nameof(ToSql));

        /// <summary>
        /// Get column name only.
        /// It's Removing table name and schema name.
        /// </summary>
        /// <typeparam name="T">Column type.</typeparam>
        /// <param name="column">Column.</param>
        /// <returns>Column name only.</returns>
        [ColumnOnlyConverter]
        public static T ColumnOnly<T>(this T column) => InvalitContext.Throw<T>(nameof(ColumnOnly));

        /// <summary>
        /// Embed a direct value in SQL without using parameters.
        /// </summary>
        /// <typeparam name="T">Value type.</typeparam>
        /// <param name="value">Value.</param>
        /// <returns>Direct Value.</returns>
        [DirectValueConverter]
        public static T DirectValue<T>(this T value) => InvalitContext.Throw<T>(nameof(DirectValue));
    }

    /// <summary>
    /// Table definition item.
    /// </summary>
    public interface ITableDefinition { }

    /// <summary>
    /// Constraint object.
    /// </summary>
    public interface IConstraint : ITableDefinition, IMethodChain { }

    /// <summary>
    /// Column definition.
    /// </summary>
    public class Column : ITableDefinition
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="column">Column.</param>
        /// <param name="type">Type.</param>
        /// <param name="constraints">Constraints.</param>
        [ColumnDefineConverter]
        public Column(object column, IDataType type, params IConstraint[] constraints) { InvalitContext.Throw("new " + nameof(Column)); }
    }
}
