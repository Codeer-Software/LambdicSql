using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class HavingClause
    {
        internal static IText ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var text = converter.ToString(method.Arguments[method.SkipMethodChain(0)]);
            return new HorizontalText(" ") { IsFunctional = true } + "HAVING" + text;
        }
    }
}
