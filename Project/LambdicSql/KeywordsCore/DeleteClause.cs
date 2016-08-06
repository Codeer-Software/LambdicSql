using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class DeleteClause
    {
        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
            => Environment.NewLine + "DELETE";
    }
}
