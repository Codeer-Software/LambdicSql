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
        internal static ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var texts = new VText();
            foreach(var m in methods)
            {
                var index = m.Method.Name == nameof(LambdicSql.Keywords.Case) ? 0 : 1;
                var argSrc = m.Arguments.Skip(index).Select(e => converter.Convert(e)).ToArray();

                switch (m.Method.Name)
                {
                    case nameof(LambdicSql.Keywords.Case):
                        return Clause("CASE", argSrc);
                    case nameof(LambdicSql.Keywords.When):
                        return SubClause("WHEN", argSrc);
                    case nameof(LambdicSql.Keywords.Then):
                        return SubClause("THEN", argSrc);
                    case nameof(LambdicSql.Keywords.Else):
                        return SubClause("ELSE", argSrc);
                    case nameof(LambdicSql.Keywords.End):
                        return "END";
                    default:
                        throw new NotSupportedException();
                }
            }
            return texts;
        }
    }
}
