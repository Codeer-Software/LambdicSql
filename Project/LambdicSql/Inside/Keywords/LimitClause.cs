using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.Inside.Keywords
{
    static class LimitClause
    {
        internal static IText ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var args = new HorizontalText(method.Arguments.Skip(method.SkipMethodChain(0)).Select(e=>converter.ToString(e)).ToArray()) { Separator = ", " };
            return new HorizontalText() { IsFunctional = true} + "LIMIT " + args;
        }
    }
}
