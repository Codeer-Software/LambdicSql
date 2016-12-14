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
            var v = new VText();
            HText h = null;
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
                            var text = new HText("CASE") { Separator = " ", IsFunctional = true };
                            if (argSrc.Length == 1)
                            {
                                text.Add(argSrc[0]);
                            }
                            v.Add(text);
                        }
                        break;
                    case nameof(LambdicSql.Keywords.When):
                        {
                            h = new HText("WHEN", SqlDisplayAdjuster.AdjustSubQueryString(argSrc[0])) { Separator = " ", IsFunctional = true, Indent = 1 };
                        }
                        break;
                    case nameof(LambdicSql.Keywords.Then):
                        {
                            if (h != null)
                            {
                                h.Add(new HText("THEN", argSrc[0]) { Separator = " ", IsFunctional = true });
                                v.Add(h);
                            }
                            else
                            {
                                v.Add(new HText("THEN", argSrc[0]) { Separator = " ", IsFunctional = true, Indent = 1 });
                            }
                            break;
                        }
                    case nameof(LambdicSql.Keywords.Else):
                        v.Add(new HText("ELSE", argSrc[0]) { Separator = " ", IsFunctional = true, Indent = 1 });
                        break;
                    case nameof(LambdicSql.Keywords.End):
                        v.Add("END");
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            return v;
        }
    }
}
