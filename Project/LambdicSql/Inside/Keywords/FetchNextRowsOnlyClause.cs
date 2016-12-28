using LambdicSql.SqlBase;
using System.Linq.Expressions;
using LambdicSql.SqlBase.TextParts;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class FetchNextRowsOnlyClause
    {
        internal static ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var count = converter.Convert(method.Arguments[method.SkipMethodChain(0)]);
            return Clause("FETCH NEXT", count, "ROWS ONLY");
        }
    }
}
