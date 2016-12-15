using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class UpdateClause
    {
        internal static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var clause = new VText();
            foreach (var m in methods)
            {
                clause.Add(MethodToString(converter, m));
            }
            return clause;
        }

        static SqlText MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.Update):
                    return Clause("UPDATE", converter.Convert(method.Arguments[0]));
                case nameof(LambdicSql.Keywords.Set):
                    {
                        var array = method.Arguments[1] as NewArrayExpression;
                        var sets = new VText();
                        sets.Add("SET");
                        sets.Add(new VText(array.Expressions.Select(e => converter.Convert(e)).ToArray()) { Indent = 1, Separator = "," });
                        return sets;
                    }
            }
            throw new NotSupportedException();
        }
    }
}
