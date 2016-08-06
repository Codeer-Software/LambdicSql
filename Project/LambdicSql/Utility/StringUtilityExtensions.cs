using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class StringUtilityExtensions
    {
        public static object FormatText(this ISqlUtility words, string text, params object[] args) => null;
        public static T FormatText<T>(this ISqlUtility words, string text, params object[] args) => default(T);
        public static object Format2WaySql(this ISqlUtility words, string text, params object[] args) => null;
        public static T Format2WaySql<T>(this ISqlUtility words, string text, params object[] args) => default(T);

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            switch (method.Method.Name)
            {
                case nameof(FormatText): return FormatText(converter, method);
                case nameof(Format2WaySql): return Format2WaySql(converter, method);
            }
            throw new NotSupportedException();
        }

        static string FormatText(ISqlStringConverter converter, MethodCallExpression method)
        {
            object obj;
            ExpressionToObject.GetExpressionObject(method.Arguments[1], out obj);
            var text = (string)obj;

            var array = method.Arguments[2] as NewArrayExpression;
            return string.Format(text, array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }

        static string Format2WaySql(ISqlStringConverter converter, MethodCallExpression method)
        {
            object obj;
            ExpressionToObject.GetExpressionObject(method.Arguments[1], out obj);
            var text = (string)obj;

            for (int i = 0; true; i++)
            {
                var start = "/*" + i + "*/";
                var startIndex = text.IndexOf(start);
                if (startIndex == -1)
                {
                    break;
                }
                var end = "/**/";
                var endIndex = text.IndexOf(end, startIndex + start.Length);
                if (endIndex == -1)
                {
                    throw new NotSupportedException("Invalid 2WaySqlFormat");
                }

                var before = text.Substring(0, startIndex);
                var after = text.Substring(endIndex + end.Length);
                text = before + "{" + i + "}" + after;
            }

            var array = method.Arguments[2] as NewArrayExpression;
            return string.Format(text, array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }
    }
}
