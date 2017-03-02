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

    /// <summary>
    /// Class representing argument of recursive part.
    /// </summary>
    /// <typeparam name="T">Type representing argument.</typeparam>
    public abstract class RecursiveArguments<T>
    {
        RecursiveArguments() { }
    }

    /// <summary>
    /// Condition building helper.
    /// condition is used if enable is valid.
    /// It can only be used within lambda of the LambdicSql.
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
}
