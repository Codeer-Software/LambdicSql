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
        public Asc(object target) { InvalitContext.Throw("new " + nameof(Asc)); }
        public static string ToString(ISqlStringConverter cnv, NewExpression exp)
        {
            return cnv.ToString(exp.Arguments[0]) + " " + "ASC";
        }
    }

    /// <summary>
    /// Descending order.
    /// </summary>
    [SqlSyntax]
    public class Desc : ISortedBy
    {
        public Desc(object target) { InvalitContext.Throw("new " + nameof(Desc)); }
        public static string ToString(ISqlStringConverter cnv, NewExpression exp)
        {
            return cnv.ToString(exp.Arguments[0]) + " " + "DESC";
        }
    }
}
