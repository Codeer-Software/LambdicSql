using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class HavingClause
    {
        internal static ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var condition = converter.Convert(method.Arguments[method.SkipMethodChain(0)]);
            if (condition.IsEmpty) return string.Empty;
            return Clause("HAVING", condition);
        }
    }
}
