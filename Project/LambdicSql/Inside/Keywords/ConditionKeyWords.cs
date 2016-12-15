using System.Linq;
using System.Linq.Expressions;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class ConditionKeyWords
    {
        internal static SqlText ConvertLike(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause(LineSpace(args[0], "LIKE"), args[1]);
        }

        internal static SqlText ConvertBetween(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause(LineSpace(args[0], "BETWEEN"), args[1], "AND", args[2]);
        }

        internal static SqlText ConvertIn(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return Func(LineSpace(args[0], "IN"), args[1]);
        }

        internal static SqlText ConvertExists(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause("EXISTS", args[0]);
        }
    }
}
