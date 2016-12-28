using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class GroupByClause
    {
        internal static ExpressionElement ConvertGroupBy(IExpressionConverter converter, MethodCallExpression[] methods)
            => Clause("GROUP BY", converter.Convert(methods[0].Arguments[methods[0].SkipMethodChain(0)]));

        internal static ExpressionElement ConvertGroupByWithRollup(IExpressionConverter converter, MethodCallExpression[] methods)
           => Clause("GROUP BY", converter.Convert(methods[0].Arguments[methods[0].SkipMethodChain(0)]), "WITH ROLLUP");

        internal static ExpressionElement ConvertGroupByRollup(IExpressionConverter converter, MethodCallExpression[] methods)
           => Func("GROUP BY ROLLUP", converter.Convert(methods[0].Arguments[methods[0].SkipMethodChain(0)]));

        internal static ExpressionElement ConvertGroupByCube(IExpressionConverter converter, MethodCallExpression[] methods)
           => Func("GROUP BY CUBE", converter.Convert(methods[0].Arguments[methods[0].SkipMethodChain(0)]));

        internal static ExpressionElement ConvertGroupByGroupingSets(IExpressionConverter converter, MethodCallExpression[] methods)
           => Func("GROUP BY GROUPING SETS", converter.Convert(methods[0].Arguments[methods[0].SkipMethodChain(0)]));
    }
}
