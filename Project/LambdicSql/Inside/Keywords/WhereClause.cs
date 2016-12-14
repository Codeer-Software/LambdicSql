using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class WhereClause
    {
        internal static TextParts ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var text = converter.ToString(method.Arguments[method.SkipMethodChain(0)]);
            if (text.IsEmpty) return string.Empty;
            return new HText("WHERE", text) { Separator = " ", IsFunctional = true };
        }
    }
}
