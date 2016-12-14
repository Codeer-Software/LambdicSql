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
            var text = new VText();
            text.Add("ORDER BY");
            var text2 = new VText(",");
            text2.AddRange(1, array.Expressions.Select(e => converter.ToString(e)).ToList());
            text.Add(text2);
            return text;
        }
    }
}
