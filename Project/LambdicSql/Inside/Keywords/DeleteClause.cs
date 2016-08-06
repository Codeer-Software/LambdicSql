using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class DeleteClause
    {
        internal static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
            => Environment.NewLine + "DELETE";
    }
}
