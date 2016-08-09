using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System.Linq.Expressions;

namespace LambdicSql
{
    public interface IOrderElement { }

    [SqlSyntax]
    public class Asc : IOrderElement
    {
        public Asc(object target) { InvalitContext.Throw("new " + nameof(Asc)); }
        public static string NewToString(ISqlStringConverter cnv, NewExpression exp)
        {
            return cnv.ToString(exp.Arguments[0]) + " " + "ASC";
        }
    }

    [SqlSyntax]
    public class Desc : IOrderElement
    {
        public Desc(object target) { InvalitContext.Throw("new " + nameof(Desc)); }
        public static string NewToString(ISqlStringConverter cnv, NewExpression exp)
        {
            return cnv.ToString(exp.Arguments[0]) + " " + "DESC";
        }
    }
}
