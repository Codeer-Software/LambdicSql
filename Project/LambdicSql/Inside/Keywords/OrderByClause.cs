using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class OrderByWordsClause
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var arg = method.Arguments[method.AdjustSqlSyntaxMethodArgumentIndex(0)];
            var array = arg as NewArrayExpression;
            return Environment.NewLine + "ORDER BY" + Environment.NewLine + "\t" + string.Join("," + Environment.NewLine + "\t", array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }
    }
}
