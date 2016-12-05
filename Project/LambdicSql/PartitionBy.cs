using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    [SqlSyntax]
    public class PartitionBy
    {
        public PartitionBy(params object[] targets) { InvalitContext.Throw("new " + nameof(PartitionBy)); }
        public static string ToString(ISqlStringConverter converter, NewExpression exp)
        {
            var arg = exp.Arguments[0];
            var array = arg as NewArrayExpression;
            return Environment.NewLine + "\tPARTITION BY" + Environment.NewLine + "\t\t" +
                string.Join("," + Environment.NewLine + "\t\t", array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }
    }
}
