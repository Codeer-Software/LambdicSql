using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System.Linq.Expressions;
using LambdicSql.SqlBase.TextParts;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

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

        static SqlText Convert(ISqlStringConverter cnv, NewExpression exp)
            => LineSpace(cnv.Convert(exp.Arguments[0]), "ASC");

        static SqlText Convert(ISqlStringConverter cnv, MethodCallExpression[] methods)
            => LineSpace(cnv.Convert(methods[0].Arguments[0]), "ASC");
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

        static SqlText Convert(ISqlStringConverter cnv, NewExpression exp)
            => LineSpace(cnv.Convert(exp.Arguments[0]), "DESC");

        static SqlText Convert(ISqlStringConverter cnv, MethodCallExpression[] methods)
            => LineSpace(cnv.Convert(methods[0].Arguments[0]), "DESC");
    }
}
