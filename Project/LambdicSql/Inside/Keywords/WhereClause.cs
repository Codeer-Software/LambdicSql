using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class WhereClause
    {
        internal static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var text = converter.Convert(method.Arguments[method.SkipMethodChain(0)]);
            if (text.IsEmpty) return string.Empty;
            return new HText("WHERE", text) { Separator = " ", IsFunctional = true };
        }
    }
}
