using LambdicSql.SqlBase;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace LambdicSql.Inside.Keywords
{
    static class ConditionKeyWords
    {
        //TODO ToStringではなくなったな
        internal static IText ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var args = method.Arguments.Select(e => SqlDisplayAdjuster.AdjustSubQuery(e, converter.ToString(e))).ToArray();
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.Like):
                    {
                        var x = new HorizontalText() { IsNotLineChange = true } + args[0] + " LIKE";
                        return new HorizontalText() { Separator = " ", IsFunctional = true } + x + args[1];
                    }
                case nameof(LambdicSql.Keywords.Between):
                    {
                        var x = new HorizontalText() { IsNotLineChange = true } + args[0] + " BETWEEN";
                        return new HorizontalText() { Separator = " ", IsFunctional = true } +x + args[1] + "AND" + args[2];
                    }
                case nameof(LambdicSql.Keywords.In):
                    {
                        var x = new HorizontalText() { IsNotLineChange = true } + args[0] + " IN";
                        var h = new HorizontalText() { IsFunctional = true };
                        h.Add(x);
                        h.Add(args[1].ConcatAround("(", ")"));
                        return h;
                    }
                case nameof(LambdicSql.Keywords.Exists): return new HorizontalText() { Separator = " ", IsFunctional = true } + "EXISTS" + args[0];
            }
            return null;
        }
    }
}
