using LambdicSql.SqlBase;
using System.Linq.Expressions;
using LambdicSql.SqlBase.TextParts;

namespace LambdicSql.Inside.Keywords
{
    static class FetchNextRowsOnlyClause
    {
        internal static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var count = converter.Convert(method.Arguments[method.SkipMethodChain(0)]);
            return new HText("FETCH NEXT", count, "ROWS ONLY") { Separator = " " };
        }
    }
}
