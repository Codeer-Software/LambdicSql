using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;
using System.Linq;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class LimitClause
    {
        internal static ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression[] methods)
            => Clause("LIMIT", Arguments(methods[0].Arguments.Skip(methods[0].SkipMethodChain(0)).Select(e => converter.Convert(e)).ToArray()));
    }
}
