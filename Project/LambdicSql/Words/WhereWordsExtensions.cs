using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class WhereWordsExtensions
    {
        public static ISqlKeyWord<TSelected> Where<TSelected>(this ISqlKeyWord<TSelected> words, bool condition) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(Where));

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var text = converter.ToString(method.Arguments[1]);
            return string.IsNullOrEmpty(text.Trim()) ? string.Empty : Environment.NewLine + "WHERE " + converter.ToString(method.Arguments[1]);
        }
    }
}
