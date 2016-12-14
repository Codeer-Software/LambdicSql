using LambdicSql.SqlBase;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace LambdicSql.Inside.Keywords
{
    static class ConditionKeyWords
    {
        //TODO ToStringではなくなったな
        internal static TextParts ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var args = method.Arguments.Select(e => SqlDisplayAdjuster.AdjustSubQuery(e, converter.ToString(e))).ToArray();
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.Like):
                    {
                        var x = new HText(args[0], " LIKE") { EnableChangeLine = false };
                        return new HText(x, args[1]) { Separator = " ", IsFunctional = true };
                    }
                case nameof(LambdicSql.Keywords.Between):
                    {
                        var x = new HText(args[0], " BETWEEN") { EnableChangeLine = false };
                        return new HText(x, args[1], "AND", args[2]) { Separator = " ", IsFunctional = true };
                    }
                case nameof(LambdicSql.Keywords.In):
                    {
                        var x = new HText(args[0], " IN") { EnableChangeLine = false };
                        var h = new HText() { IsFunctional = true };
                        h.Add(x);
                        h.Add(args[1].ConcatAround("(", ")"));
                        return h;
                    }
                case nameof(LambdicSql.Keywords.Exists): return new HText("EXISTS", args[0]) { Separator = " ", IsFunctional = true };
            }
            return null;
        }
    }
}
