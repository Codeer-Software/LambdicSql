using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// It is an object representing the sort order
    /// Implemented classes include Asc and Desc.
    /// </summary>
    public interface ISortedBy { }

    /// <summary>
    /// Ascending order.
    /// </summary>
    [SqlSyntax]
    public class Asc : ISortedBy
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="target">target column.</param>
        public Asc(object target) { InvalitContext.Throw("new " + nameof(Asc)); }

        static string ToString(ISqlStringConverter cnv, NewExpression exp)
            => cnv.ToString(exp.Arguments[0]) + " ASC";

        static string ToString(ISqlStringConverter cnv, MethodCallExpression[] methods)
            => cnv.ToString(methods[0].Arguments[0]) + " ASC";
    }

    /// <summary>
    /// Descending order.
    /// </summary>
    [SqlSyntax]
    public class Desc : ISortedBy
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="target">target column.</param>
        public Desc(object target) { InvalitContext.Throw("new " + nameof(Desc)); }

        static string ToString(ISqlStringConverter cnv, NewExpression exp)
            => cnv.ToString(exp.Arguments[0]) + " DESC";

        static string ToString(ISqlStringConverter cnv, MethodCallExpression[] methods)
            => cnv.ToString(methods[0].Arguments[0]) + " DESC";
    }
}
