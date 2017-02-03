using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Specialized.SymbolConverters;
using System;

namespace LambdicSql
{
    /// <summary>
    /// Data type.
    /// </summary>
    public abstract class DataTypeElement { }

    /// <summary>
    /// TOP keyword.
    /// </summary>
    public abstract class TopElement { }

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
    public abstract class SortedByElement { }

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
    /// Element of DateTime.
    /// </summary>
    [EnumToStringConverter]
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
    /// Interval Type.
    /// </summary>
    [EnumToStringConverter]
    public enum IntervalType
    {
        /// <summary>
        /// YEAR
        /// </summary>
        Year,

        /// <summary>
        /// MONTH
        /// </summary>
        Month,

        /// <summary>
        /// DAY
        /// </summary>
        Day,

        /// <summary>
        /// HOUR
        /// </summary>
        Hour,

        /// <summary>
        /// MINUTE
        /// </summary>
        Minute,

        /// <summary>
        /// SECOND
        /// </summary>
        Second,

        /// <summary>
        /// YEAR TO MONTH
        /// </summary>
        [FieldSqlName("YEAR TO MONTH")]
        YearToMonth,

        /// <summary>
        /// DAY TO HOUR
        /// </summary>
        [FieldSqlName("DAY TO HOUR")]
        DayToHour,

        /// <summary>
        /// DAY TO MINUTE
        /// </summary>
        [FieldSqlName("DAY TO MINUTE")]
        DayToMinute,

        /// <summary>
        /// DAY TO SECOND
        /// </summary>
        [FieldSqlName("DAY TO SECOND")]
        DayToSecond,

        /// <summary>
        /// HOUR TO MINUTE
        /// </summary>
        [FieldSqlName("HOUR TO MINUTE")]
        HOURToMinute,

        /// <summary>
        /// HOUR TO SECOND
        /// </summary>
        [FieldSqlName("HOUR TO SECOND")]
        HOURToSecond,

        /// <summary>
        /// MINUTE TO SECOND
        /// </summary>
        [FieldSqlName("MINUTE TO SECOND")]
        MinuteToSecond,
    }

    /// <summary>
    /// OVER clause argument.
    /// </summary>
    public class OverArgument { }

    /// <summary>
    /// OVER clause result.
    /// </summary>
    public class OverReturnValue : SqlExpression
    {
        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static object operator +(object value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static string operator +(string value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static bool operator +(bool value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static bool? operator +(bool? value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static byte operator +(byte value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static byte? operator +(byte? value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static short operator +(short value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static short? operator +(short? value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static int operator +(int value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static int? operator +(int? value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static long operator +(long value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static long? operator +(long? value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static float operator +(float value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static float? operator +(float? value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static double operator +(double value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static double? operator +(double? value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static decimal operator +(decimal value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static decimal? operator +(decimal? value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static DateTime operator +(DateTime value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static DateTime? operator +(DateTime? value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static DateTimeOffset operator +(DateTimeOffset value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static DateTimeOffset? operator +(DateTimeOffset? value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static TimeSpan operator +(TimeSpan value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static TimeSpan? operator +(TimeSpan? value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static byte[] operator +(byte[] value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Additional operator.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="returnValue">Return value.</param>
        /// <returns>Value.</returns>
        public static char[] operator +(char[] value, OverReturnValue returnValue) { throw new InvalitContextException("implicit operator"); }
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
    /// Class representing argument of recursive part.
    /// </summary>
    /// <typeparam name="T">Type representing argument.</typeparam>
    public class RecursiveArguments<T>
    {
        RecursiveArguments() { }
    }

    /// <summary>
    /// Column definition.
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
        public Condition(object enable, object condition) { throw new InvalitContextException("new " + nameof(Condition)); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        public static implicit operator bool(Condition src) { throw new InvalitContextException("implicit operator bool(Condition src)"); }
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
        [MemberConverter(Name = "SYSIBM.SYSDUMMY1")]
        public static object SysDummy1 { get { throw new InvalitContextException(nameof(SysDummy1)); } }
    }
}
