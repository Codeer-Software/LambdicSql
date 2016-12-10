using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.Inside.Keywords
{
    static class LimitClause
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var args = string.Join(", ", method.Arguments.Skip(method.SkipMethodChain(0)).Select(e=>converter.ToString(e)).ToArray());
            return Environment.NewLine + "LIMIT " + args;
        }
    }
}
