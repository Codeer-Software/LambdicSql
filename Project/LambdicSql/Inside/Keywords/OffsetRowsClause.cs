using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class OffsetRowsClause
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var count = converter.ToString(method.Arguments[method.AdjustSqlSyntaxMethodArgumentIndex(0)]);
            return Environment.NewLine + "OFFSET " + count + " ROWS";
        }
    }
}
