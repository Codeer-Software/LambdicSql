using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class OrderByWordsClause
    {
        internal static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var arg = method.Arguments[method.SkipMethodChain(0)];
            var array = arg as NewArrayExpression;

            var orderBy = new VText();
            orderBy.Add("ORDER BY");
            var sort = new VText() { Separator = "," };
            sort.AddRange(1, array.Expressions.Select(e => converter.Convert(e)).ToList());
            orderBy.Add(sort);
            return orderBy;
        }
    }
}
