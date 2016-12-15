using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class OffsetClause
    {
        internal static TextParts Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var count = converter.Convert(method.Arguments[method.SkipMethodChain(0)]);
            return new HText("OFFSET", count) { Separator = " " };
        }
    }
}
