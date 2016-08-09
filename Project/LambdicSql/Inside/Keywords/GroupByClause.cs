using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class GroupByClause
    {
        internal static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            return Environment.NewLine + "GROUP BY " + converter.ToString(method.Arguments[method.AdjustSqlSyntaxMethodArgumentIndex(0)]);
        }
    }
}
