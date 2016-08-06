using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public interface IOrderElement : ISqlSyntaxObject { }

    public class Asc : IOrderElement
    {
        public Asc(object target) { }
        public static string NewToString(ISqlStringConverter cnv, NewExpression exp)
        {
            return Environment.NewLine + "\t" + cnv.ToString(exp.Arguments[0]) + " " + "ASC";
        }
    }

    public class Desc : IOrderElement
    {
        public Desc(object target) { }
        public static string NewToString(ISqlStringConverter cnv, NewExpression exp)
        {
            return Environment.NewLine + "\t" + cnv.ToString(exp.Arguments[0]) + " " + "DESC";
        }
    }
}
