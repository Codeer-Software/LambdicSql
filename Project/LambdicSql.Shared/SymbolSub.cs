using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Specialized.SymbolConverters;

namespace LambdicSql
{
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
    /// OVER clause argument.
    /// </summary>
    public class OverArgument { }

    /// <summary>
    /// Aggregation predicate.
    /// ALL or DISTINCT.
    /// </summary>
    public interface IAggregatePredicate { }

    /// <summary>
    /// Aggregation predicate.
    /// ALL
    /// </summary>
    public interface IAggregatePredicateAll : IAggregatePredicate { }

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

    /// <summary>
    /// Utility.
    /// It can only be used within methods of the LambdicSql.Db class.
    /// </summary>
    public static class UtilitySymbolExtensions
    {
        /// <summary>
        /// Put the text in the expression of LamblicSql.
        /// </summary>
        /// <param name="text">Text.You can use the same format as System.String's Format method.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>LamblicSql's expression.</returns>
        [ToSqlConverter]
        public static object ToSql(this string text, params object[] args) { throw new InvalitContextException(nameof(ToSql)); }

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
        public static object TwoWaySql(this string text, params object[] args) { throw new InvalitContextException(nameof(ToSql)); }

        /// <summary>
        /// Embed values directly into SQL without parameterization.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="value">Value.</param>
        /// <returns>Direct value.</returns>
        [MethodFormatConverter(Format = "[$0]")]
        public static T DirectValue<T>(this T value) { throw new InvalitContextException(nameof(DirectValue)); }

        /// <summary>
        /// Schema and table names are omitted and only columns are used.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="column">Column.</param>
        /// <returns>Column only.</returns>
        [MethodFormatConverter(Format ="[#0]")]
        public static T ColumnOnly<T>(this T column) { throw new InvalitContextException(nameof(ColumnOnly)); }
    }

    /// <summary>
    /// Table definition item.
    /// </summary>
    public interface ITableDefinition { }

    /// <summary>
    /// Constraint object.
    /// </summary>
    public interface IConstraint : ITableDefinition { }

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
        [NewFormatConverter(Format ="[0] [1] [< >2]")]
        public Column(object column, IDataType type, params IConstraint[] constraints) { throw new InvalitContextException("new " + nameof(Column)); }
    }
}
