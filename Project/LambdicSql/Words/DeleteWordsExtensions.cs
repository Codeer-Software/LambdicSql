using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class DeleteWordsExtensions
    {
        public static ISqlKeyWord<TSelected> Delete<TSelected>(this ISqlKeyWord<TSelected> words) => null;

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            return Environment.NewLine + "DELETE";
        }
    }
}
