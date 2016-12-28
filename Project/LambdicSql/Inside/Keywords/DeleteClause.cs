using System.Linq.Expressions;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;

namespace LambdicSql.Inside.Keywords
{
    static class DeleteClause
    {
        internal static ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression[] methods)
            => methods[0].Method.Name.ToUpper();
    }
}
