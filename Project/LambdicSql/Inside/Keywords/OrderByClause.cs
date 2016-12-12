using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class OrderByWordsClause
    {
        internal static IText ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var arg = method.Arguments[method.SkipMethodChain(0)];
            var array = arg as NewArrayExpression;
            var text = new VerticalText();
            text.Add("ORDER BY");
            text.AddRange(1, array.Expressions.Select(e => converter.ToString(e)).ToList());
            return text;
        }
    }
}
