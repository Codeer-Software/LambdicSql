using System;
using System.Linq.Expressions;
using System.Linq;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

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
                        texts.Add(Clause("CASE", argSrc));
                        break;
                    case nameof(LambdicSql.Keywords.When):
                        whenThen = SubClause("WHEN", argSrc);
                        break;
                    case nameof(LambdicSql.Keywords.Then):
                        if (whenThen == null) throw new NotSupportedException();
                        whenThen.Add(Clause("THEN", argSrc));
                        texts.Add(whenThen);
                        whenThen = null;
                        break;
                    case nameof(LambdicSql.Keywords.Else):
                        texts.Add(SubClause("ELSE", argSrc));
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
