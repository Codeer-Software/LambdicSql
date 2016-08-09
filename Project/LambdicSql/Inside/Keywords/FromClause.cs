using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class FromClause
    {
        internal static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
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
            string[] argSrc = method.Arguments.Skip(1).Select(e => converter.ToString(e)).ToArray();
            switch (name)
            {
                case nameof(LambdicSql.Keywords.From):
                    return Environment.NewLine + "FROM " + ExpressionToTableName(converter, method.Arguments[method.AdjustSqlSyntaxMethodArgumentIndex(0)]);
                case nameof(LambdicSql.Keywords.CrossJoin):
                    return Environment.NewLine + "\tCROSS JOIN " + ExpressionToTableName(converter, method.Arguments[1]);
            }
            return Environment.NewLine + "\t" + name.ToUpper() + " " + ExpressionToTableName(converter, method.Arguments[1]) + " ON " + argSrc[1];
        }

        static string ExpressionToTableName(ISqlStringConverter decoder, Expression exp)
        {
            var text = decoder.ToString(exp);
            var methodCall = exp as MethodCallExpression;
            if (methodCall != null)
            {
                //TODO oracl custom
                var x = ((MemberExpression)methodCall.Arguments[0]).Member.Name;
                return text + " AS " + x;
            }
            if (typeof(ISqlExpression).IsAssignableFrom(exp.Type))
            {
                var member = exp as MemberExpression;
                if (member != null)
                {
                    //TODO oracl custom
                    return text + " AS " + member.Member.Name;
                }
            }

            var table = decoder.Context.DbInfo.GetLambdaNameAndTable()[text];
            if (table.SubQuery == null)
            {
                return table.SqlFullName;
            }

            throw new NotSupportedException();
        }
    }
}
