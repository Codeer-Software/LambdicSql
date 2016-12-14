using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class GroupByClause
    {
        internal static IText ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var name = string.Empty;
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.GroupBy):
                    name = "GROUP BY";
                    return new HorizontalText() { Separator = " " } + name + converter.ToString(method.Arguments[method.SkipMethodChain(0)]);
                case nameof(LambdicSql.Keywords.GroupByWithRollup):
                    name = "GROUP BY";
                    return new HorizontalText() { Separator = " " } + name + converter.ToString(method.Arguments[method.SkipMethodChain(0)]) + "WITH ROLLUP";
                case nameof(LambdicSql.Keywords.GroupByRollup): name = "GROUP BY ROLLUP"; break;
                case nameof(LambdicSql.Keywords.GroupByCube): name = "GROUP BY CUBE"; break;
                case nameof(LambdicSql.Keywords.GroupByGroupingSets): name = "GROUP BY GROUPING SETS"; break;
                default: throw new NotSupportedException();
            }
            return new HorizontalText() + name + "(" + converter.ToString(method.Arguments[method.SkipMethodChain(0)]) + ")";
        }
    }
}
