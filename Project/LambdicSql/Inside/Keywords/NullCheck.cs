using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class NullCheck
    {
        internal static SqlText ConvertIsNull(ISqlStringConverter converter, MethodCallExpression[] methods)
            => LineSpace(converter.Convert(methods[0].Arguments[0]), "IS NULL");

        internal static SqlText ConvertIsNotNull(ISqlStringConverter converter, MethodCallExpression[] methods)
            => LineSpace(converter.Convert(methods[0].Arguments[0]), "IS NOT NULL");
    }
}
