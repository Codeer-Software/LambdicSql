using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class OffsetRowsClause
    {
        internal static ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var count = converter.Convert(method.Arguments[method.SkipMethodChain(0)]);
            return Clause("OFFSET", count, "ROWS");
        }
    }
}
