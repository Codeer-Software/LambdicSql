using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    [SqlSyntax]
    public class Rows
    {
        public Rows(int preceding) { InvalitContext.Throw("new " + nameof(Rows)); }
        public Rows(int preceding, int following) { InvalitContext.Throw("new " + nameof(Rows)); }
        public static string ToString(ISqlStringConverter converter, NewExpression exp)
        {
            var args = exp.Arguments.Select(e => converter.ToString(e)).ToArray();
            if (exp.Arguments.Count == 1)
            {
                return Environment.NewLine + "\tROWS " + args[0] + " PRECEDING";
            }
            else
            {
                return Environment.NewLine + "\tROWS BETWEEN " + converter.Context.Parameters.ResolvePrepare(args[0]) +
                    " PRECEDING AND " + converter.Context.Parameters.ResolvePrepare(args[1]) + " FOLLOWING";
            }
        }
    }
}
