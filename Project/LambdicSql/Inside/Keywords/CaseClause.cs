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
            var v = new VerticalText();
            HorizontalText h = null;
            for (int i = 0; i < methods.Length; i++)
            {
                var m = methods[i];
                var argSrc = m.Arguments.Skip(m.SkipMethodChain(0)).Select(e => converter.ToString(e)).ToArray();


                string name = m.Method.Name;

                //TODO
                switch (name)
                {
                    case nameof(LambdicSql.Keywords.Case):
                        {
                            var text = new HorizontalText() { Separator = " ", IsFunctional = true } + "CASE";
                            if (argSrc.Length == 1)
                            {
                                text += argSrc[0];
                            }
                            v.Add(text);
                        }
                        break;
                    case nameof(LambdicSql.Keywords.When):
                        {
                            h = new HorizontalText() { Separator = " ", IsFunctional = true, Indent = 1 } + "WHEN" + SqlDisplayAdjuster.AdjustSubQueryString(argSrc[0]);
                        }
                        break;
                    case nameof(LambdicSql.Keywords.Then):
                        {
                            if (h != null)
                            {
                                h = h + (new HorizontalText() { Separator = " ", IsFunctional = true } + "THEN" + argSrc[0]);
                                v.Add(h);
                                h = null;
                            }
                            else
                            {
                                v.Add(new HorizontalText() { Separator = " ", IsFunctional = true, Indent = 1 } + "THEN" + argSrc[0]);
                            }
                            break;
                        }
                    case nameof(LambdicSql.Keywords.Else):
                        v.Add(new HorizontalText() { Separator = " ", IsFunctional = true, Indent = 1 } + "ELSE" + argSrc[0]);
                        break;
                    case nameof(LambdicSql.Keywords.End):
                        v.Add(new SingleText("END"));
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            return v;
        }
    }
}
