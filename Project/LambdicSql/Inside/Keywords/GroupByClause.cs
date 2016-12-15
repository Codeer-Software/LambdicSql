using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class GroupByClause
    {
        internal static SqlText ConvertGroupBy(ISqlStringConverter converter, MethodCallExpression[] methods)
            => Clause("GROUP BY", converter.Convert(methods[0].Arguments[methods[0].SkipMethodChain(0)]));

        internal static SqlText ConvertGroupByWithRollup(ISqlStringConverter converter, MethodCallExpression[] methods)
           => Clause("GROUP BY", converter.Convert(methods[0].Arguments[methods[0].SkipMethodChain(0)]), "WITH ROLLUP");

        internal static SqlText ConvertGroupByRollup(ISqlStringConverter converter, MethodCallExpression[] methods)
           => Func("GROUP BY ROLLUP", converter.Convert(methods[0].Arguments[methods[0].SkipMethodChain(0)]));

        internal static SqlText ConvertGroupByCube(ISqlStringConverter converter, MethodCallExpression[] methods)
           => Func("GROUP BY CUBE", converter.Convert(methods[0].Arguments[methods[0].SkipMethodChain(0)]));

        internal static SqlText ConvertGroupByGroupingSets(ISqlStringConverter converter, MethodCallExpression[] methods)
           => Func("GROUP BY GROUPING SETS", converter.Convert(methods[0].Arguments[methods[0].SkipMethodChain(0)]));
    }
}
