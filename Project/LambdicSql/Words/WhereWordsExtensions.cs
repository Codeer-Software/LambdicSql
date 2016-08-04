using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class WhereWordsExtensions
    {
        public static ISqlKeyWord<TSelected> Where<TSelected>(this ISqlKeyWord<TSelected> words, bool condition) => null;

        public static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            var text = converter.ToString(method.Arguments[1]);
            return string.IsNullOrEmpty(text.Trim()) ? string.Empty : Environment.NewLine + "WHERE " + converter.ToString(method.Arguments[1]);
        }
    }
}
