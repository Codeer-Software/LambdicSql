using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class HavingClause
    {
        internal static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var text = converter.ToString(method.Arguments[method.AdjustSqlSyntaxMethodArgumentIndex(0)]);
            return string.IsNullOrEmpty(text.Trim()) ? string.Empty : Environment.NewLine + "HAVING " + text;
        }
    }
}
