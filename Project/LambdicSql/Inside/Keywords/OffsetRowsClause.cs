using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class OffsetRowsClause
    {
        internal static IText ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var count = converter.ToString(method.Arguments[method.SkipMethodChain(0)]);
            return new HText("OFFSET", count, "ROWS") { Separator = " " };
        }
    }
}
