using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class FetchNextRowsOnlyClause
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var count = converter.ToString(method.Arguments[method.SkipMethodChain(0)]);
            return Environment.NewLine + "FETCH NEXT " + count + " ROWS ONLY";
        }
    }
}
