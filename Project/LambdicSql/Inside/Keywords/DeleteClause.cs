using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class DeleteClause
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
            => Environment.NewLine + "DELETE";
    }
}
