using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace LambdicSql.Inside.Keywords
{
    static class CaseClause
    {
        internal static IText ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<IText>();
            foreach (var m in methods)
            {
                var argSrc = m.Arguments.Skip(m.SkipMethodChain(0)).Select(e => converter.ToString(e)).ToArray();
                list.Add(MethodToString(m.Method.Name, argSrc));
            }
            return new VerticalText(list.ToArray());
        }

        static IText MethodToString(string name, IText[] argSrc)
        {
            //TODO
            switch (name)
            {
                case nameof(LambdicSql.Keywords.Case):
                    {
                        var text = new HorizontalText(" ") { IsFunctional = true } + "\tCASE";
                        if (argSrc.Length == 1)
                        {
                            text += argSrc[0];
                        }
                        return text;
                    }
                case nameof(LambdicSql.Keywords.When):
                    return new HorizontalText(" ") { IsFunctional = true, Indent = 1 } + "WHEN " + SqlDisplayAdjuster.AdjustSubQueryString(argSrc[0]);
                case nameof(LambdicSql.Keywords.Then):
                    return new HorizontalText(" ") { IsFunctional = true, Indent = 1 } + "THEN" + argSrc[0];
                case nameof(LambdicSql.Keywords.Else):
                    return new HorizontalText(" ") { IsFunctional = true, Indent = 1 } + "ELSE " + argSrc[0];
                case nameof(LambdicSql.Keywords.End):
                    return new SingleText("\tEND", 1);
            }
            throw new NotSupportedException();
        }
    }
}
