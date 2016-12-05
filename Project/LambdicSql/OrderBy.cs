using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    [SqlSyntax]
    public class OrderBy
    {
        public OrderBy(params ISortedBy[] elements) { InvalitContext.Throw("new " + nameof(OrderBy)); }
        public static string ToString(ISqlStringConverter converter, NewExpression exp)
        {
            var arg = exp.Arguments[0];
            var array = arg as NewArrayExpression;
            return Environment.NewLine + "\tORDER BY" + Environment.NewLine + "\t\t" +
                string.Join("," + Environment.NewLine + "\t\t", array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }
    }
}
