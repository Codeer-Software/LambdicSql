using LambdicSql.SqlBase;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace LambdicSql.Inside.Keywords
{
    static class ConditionKeyWords
    {
        internal static IText ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var args = method.Arguments.Select(e => SqlDisplayAdjuster.AdjustSubQuery(e, converter.ToString(e))).ToArray();
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.Like):
                    return new HorizontalText(" ") + args[0] + "LIKE" + args[1];
                case nameof(LambdicSql.Keywords.Between):
                    return new HorizontalText(" ") + args[0] + "BETWEEN" + args[1] + "AND" + args[2];
                case nameof(LambdicSql.Keywords.In):
                    return new HorizontalText() + args[0] + "IN(" + new HorizontalText(", ", args.Skip(1).ToArray()) + ")";
                case nameof(LambdicSql.Keywords.Exists): return new HorizontalText() + "EXISTS" + args[0];
            }
            return null;
        }
    }
}
