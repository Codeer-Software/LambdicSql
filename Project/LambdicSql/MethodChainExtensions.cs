using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System.Linq.Expressions;

namespace LambdicSql
{
    [SqlSyntax]
    public static class MethodChainExtensions
    {
        public static T Cast<T>(this IMethodChain words) => InvalitContext.Throw<T>(nameof(Cast));
        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods) => string.Empty;
    }
}
