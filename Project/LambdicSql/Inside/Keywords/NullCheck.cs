using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class NullCheck
    {
        internal static ExpressionElement ConvertIsNull(IExpressionConverter converter, MethodCallExpression[] methods)
            => LineSpace(converter.Convert(methods[0].Arguments[0]), "IS NULL");

        internal static ExpressionElement ConvertIsNotNull(IExpressionConverter converter, MethodCallExpression[] methods)
            => LineSpace(converter.Convert(methods[0].Arguments[0]), "IS NOT NULL");
    }
}
