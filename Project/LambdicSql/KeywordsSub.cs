using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;

namespace LambdicSql
{
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
        internal OrderBy() { }
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
    /// Element of DateTime.
    /// </summary>
    [SqlSyntax]
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
    [SqlSyntax]
    public class Assign
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rhs">Rvalue</param>
        /// <param name="lhs">Lvalue</param>
        public Assign(object rhs, object lhs) { InvalitContext.Throw("new " + nameof(Assign)); }

        static ExpressionElement Convert(IExpressionConverter converter, NewExpression exp)
        {
            ExpressionElement arg1 = converter.Convert(exp.Arguments[0]).Customize(new CustomizeColumnOnly());
            return new HText(arg1, "=", converter.Convert(exp.Arguments[1])) { Separator = " " };
        }
    }

    /// <summary>
    /// Condition building helper.
    /// condition is used if enable is valid.
    /// </summary>
    [SqlSyntax]
    public class Condition
    {
        /// <summary>
        /// Condition building helper.
        /// condition is used if enable is valid.
        /// </summary>
        /// <param name="enable">Whether condition is valid.</param>
        /// <param name="condition">Condition expression.</param>
        public Condition(object enable, object condition) { InvalitContext.Throw("new " + nameof(Condition)); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        public static implicit operator bool(Condition src) => InvalitContext.Throw<bool>("implicit operator bool(Condition src)");

        static ExpressionElement Convert(IExpressionConverter converter, NewExpression exp)
        {
            var obj = converter.ToObject(exp.Arguments[0]);
            return (bool)obj ? converter.Convert(exp.Arguments[1]) : (ExpressionElement)string.Empty;
        }
    }

    /// <summary>
    /// SYSIBM keyword.
    /// </summary>
    [SqlSyntax]
    public class SysIBM
    {
        internal SysIBM() { }

        /// <summary>
        /// SYSDUMMY1 keyword.
        /// </summary>
        public static object SysDummy1() => InvalitContext.Throw<long>(nameof(SysDummy1));

        static ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression[] methods)
            => "SYSIBM." + methods[0].Method.Name.ToUpper();
    }
}
