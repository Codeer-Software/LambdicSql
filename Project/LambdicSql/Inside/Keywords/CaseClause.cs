using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using LambdicSql.SqlBase.TextParts;

namespace LambdicSql.Inside.Keywords
{
    static class CaseClause
    {
        internal static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var texts = new VText();
            HText whenThen = null;
            foreach(var m in methods)
            {
                var argSrc = m.Arguments.Skip(m.SkipMethodChain(0)).Select(e => converter.Convert(e)).ToArray();

                switch (m.Method.Name)
                {
                    case nameof(LambdicSql.Keywords.Case):
                        {
                            var text = new HText("CASE") { Separator = " ", IsFunctional = true };
                            if (argSrc.Length == 1)
                            {
                                text.Add(argSrc[0]);
                            }
                            texts.Add(text);
                        }
                        break;
                    case nameof(LambdicSql.Keywords.When):
                        {
                            whenThen = new HText("WHEN", argSrc[0]) { Separator = " ", IsFunctional = true, Indent = 1 };
                        }
                        break;
                    case nameof(LambdicSql.Keywords.Then):
                        {
                            if (whenThen != null)
                            {
                                whenThen.Add(new HText("THEN", argSrc[0]) { Separator = " ", IsFunctional = true });
                                texts.Add(whenThen);
                            }
                            else
                            {
                                texts.Add(new HText("THEN", argSrc[0]) { Separator = " ", IsFunctional = true, Indent = 1 });
                            }
                            whenThen = null;
                            break;
                        }
                    case nameof(LambdicSql.Keywords.Else):
                        texts.Add(new HText("ELSE", argSrc[0]) { Separator = " ", IsFunctional = true, Indent = 1 });
                        break;
                    case nameof(LambdicSql.Keywords.End):
                        texts.Add("END");
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            return texts;
        }
    }
}
