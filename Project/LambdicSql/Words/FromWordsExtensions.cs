using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class FromWordsExtensions
    {
        public static ISqlKeyWord<TSelected> From<TSelected, T>(this ISqlKeyWord<TSelected> words, T tbale) => null;
        public static ISqlKeyWord<TSelected> Join<TSelected, T>(this ISqlKeyWord<TSelected> words, T tbale, bool condition) => null;
        public static ISqlKeyWord<TSelected> LeftJoin<TSelected, T>(this ISqlKeyWord<TSelected> words, T tbale, bool condition) => null;
        public static ISqlKeyWord<TSelected> RightJoin<TSelected, T>(this ISqlKeyWord<TSelected> words, T tbale, bool condition) => null;
        public static ISqlKeyWord<TSelected> CrossJoin<TSelected, T>(this ISqlKeyWord<TSelected> words, T tbale) => null;

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            foreach (var m in methods)
            {
                list.Add(MethodToString(converter, m));
            }
            return string.Join(string.Empty, list.ToArray());
        }

        static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            string name = method.Method.Name;
            string[] argSrc = method.Arguments.Skip(1).Select(e => converter.ToString(e)).ToArray();//TODO
            switch (name)
            {
                case nameof(From): return Environment.NewLine + "FROM " + ExpressionToTableName(converter, method.Arguments[1]);
                case nameof(CrossJoin): return Environment.NewLine + "\tCROSS JOIN " + ExpressionToTableName(converter, method.Arguments[1]);
            }
            return Environment.NewLine + "\t" + name.ToUpper() + " " + ExpressionToTableName(converter, method.Arguments[1]) + " ON " + argSrc[1];
        }

        static string ExpressionToTableName(ISqlStringConverter decoder, Expression exp)
        {
            var text = decoder.ToString(exp);
            var methodCall = exp as MethodCallExpression;
            if (methodCall != null)
            {
                //TODO if cast expression
                //TODO oracl custom
                var x = ((MemberExpression)methodCall.Arguments[0]).Member.Name;
                return text + " AS " + x;
            }

            var table = decoder.Context.DbInfo.GetLambdaNameAndTable()[text];
            if (table.SubQuery == null)
            {
                return table.SqlFullName;
            }

            //TODO no need
            return decoder.ToString(table.SubQuery) + " AS " + table.SqlFullName;
        }
    }
}
