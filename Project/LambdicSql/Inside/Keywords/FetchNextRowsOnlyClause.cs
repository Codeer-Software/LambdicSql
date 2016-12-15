using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class FetchNextRowsOnlyClause
    {
        internal static TextParts Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var count = converter.Convert(method.Arguments[method.SkipMethodChain(0)]);
            return new HText("FETCH NEXT", count, "ROWS ONLY") { Separator = " " };
        }
    }
}
