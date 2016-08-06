using LambdicSql.QueryBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class OrderByWordsClause
    {
        internal static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var arg = method.Arguments[method.SqlSyntaxMethodArgumentAdjuster()(0)];
            var array = arg as NewArrayExpression;
            return Environment.NewLine + "ORDER BY" + Environment.NewLine + "\t" + string.Join("," + Environment.NewLine + "\t", array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }
    }
}
