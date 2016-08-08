using LambdicSql.QueryBase;
using System.Linq.Expressions;

namespace LambdicSql
{
    public interface IOrderElement { }

    [SqlSyntax]
    public class Asc : IOrderElement
    {
        public Asc(object target) { }
        public static string NewToString(ISqlStringConverter cnv, NewExpression exp)
        {
            return cnv.ToString(exp.Arguments[0]) + " " + "ASC";
        }
    }

    [SqlSyntax]
    public class Desc : IOrderElement
    {
        public Desc(object target) { }
        public static string NewToString(ISqlStringConverter cnv, NewExpression exp)
        {
            return cnv.ToString(exp.Arguments[0]) + " " + "DESC";
        }
    }
}
