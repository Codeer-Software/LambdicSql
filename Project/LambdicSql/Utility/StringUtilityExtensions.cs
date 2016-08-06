using LambdicSql.QueryBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class StringUtilityExtensions
    {
        public static object Text(this ISqlUtility words, string text, params object[] args) => null;
        public static T Text<T>(this ISqlUtility words, string text, params object[] args) => default(T);

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];

            object obj;
            ExpressionToObject.GetExpressionObject(method.Arguments[1], out obj);
            var text = (string)obj;

            var array = method.Arguments[2] as NewArrayExpression;
            return string.Format(text, array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }
    }
}
