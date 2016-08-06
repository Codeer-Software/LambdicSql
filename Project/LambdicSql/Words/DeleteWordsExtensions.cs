using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class DeleteWordsExtensions
    {
        public static ISqlKeyWord<TSelected> Delete<TSelected>(this ISqlKeyWord<TSelected> words) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(Delete));

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            return Environment.NewLine + "DELETE";
        }
    }
}
