using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System.Linq.Expressions;
using System.Linq;
using System;

//TODO やっぱり統合したいよなー
//Window関数だけなんだかなーって感じやねんなー
namespace LambdicSql
{
    [SqlSyntax]
    public static class Utils
    {
        public static T Cast<T>(this ISqlExpressionBase query) => InvalitContext.Throw<T>(nameof(Cast));
        public static T Cast<T>(this IMethodChain words) => InvalitContext.Throw<T>(nameof(Cast));
        public static bool Condition(bool enable, bool condition) => InvalitContext.Throw<bool>(nameof(Condition));
        public static object Text(string text, params object[] args) => InvalitContext.Throw<object>(nameof(Text));
        public static T Text<T>(string text, params object[] args) => InvalitContext.Throw<T>(nameof(Text));
        public static IQuery<Non> TwoWaySql(string text, params object[] args) => InvalitContext.Throw<IQuery<Non>>(nameof(Text));
        public static T ColumnOnly<T>(T target) => InvalitContext.Throw<T>(nameof(ColumnOnly));

        static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            switch (method.Method.Name)
            {
                case nameof(Cast): return string.Empty;
                case nameof(Condition): return Condition(converter, method);
                case nameof(Text): return Text(converter, method);
                case nameof(TwoWaySql): return TwoWaySql(converter, method);
                case nameof(ColumnOnly): return ColumnOnly(converter, method);
            }
            throw new NotSupportedException();
        }

        static string Condition(ISqlStringConverter converter, MethodCallExpression method)
        {
            var obj = converter.ToObject(method.Arguments[0]);
            return (bool)obj ? converter.ToString(method.Arguments[1]) : string.Empty;
        }

        static string Text(ISqlStringConverter converter, MethodCallExpression method)
        {
            var obj = converter.ToObject(method.Arguments[0]);
            var text = (string)obj;

            var array = method.Arguments[1] as NewArrayExpression;
            return string.Format(text, array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }

        static string TwoWaySql(ISqlStringConverter converter, MethodCallExpression method)
        {
            var obj = converter.ToObject(method.Arguments[0]);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);

            var array = method.Arguments[1] as NewArrayExpression;
            return string.Format(text, array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }

        static string ColumnOnly(ISqlStringConverter converter, MethodCallExpression method)
        {
            var dic = converter.Context.DbInfo.GetLambdaNameAndColumn().ToDictionary(e => e.Value.SqlFullName, e => e.Value.SqlColumnName);
            string col;
            if (dic.TryGetValue(converter.ToString(method.Arguments[0]), out col)) return col;
            throw new NotSupportedException("invalid column.");
        }
    }
}

