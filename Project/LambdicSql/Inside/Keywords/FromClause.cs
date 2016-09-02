using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class FromClause
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
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
                case nameof(LambdicSql.Keywords.LeftJoin):
                    name = "LEFT JOIN";
                    break;
                case nameof(LambdicSql.Keywords.RightJoin):
                    name = "RIGHT JOIN";
                    break;
            }
            return Environment.NewLine + "\t" + name.ToUpper() + " " + ExpressionToTableName(converter, method.Arguments[1]) + " ON " + argSrc[1];
        }

        //TODO refactoring.
        internal static string ExpressionToTableName(ISqlStringConverter decoder, Expression exp)
        {
            var arry = exp as NewArrayExpression;
            if (arry != null)
            {
                return string.Join(",", arry.Expressions.Select(e => ExpressionToTableName(decoder, e)).ToArray());
            }

            var text = decoder.ToString(exp);

            var methodCall = exp as MethodCallExpression;
            if (methodCall != null)
            {
                var member = methodCall.Arguments[0] as MemberExpression;
                if (member != null)
                {
                    var x = member.Member.Name;
                    return text + " " + x;
                }
                return text;
            }

            var body = GetSqlExpressionBody(exp);
            if (body != null)
            {
                return text + " " + body;
            }
            if (typeof(ISqlExpressionBase).IsAssignableFrom(exp.Type))
            {
                var member = exp as MemberExpression;
                if (member != null)
                {
                    return text + " " + member.Member.Name;
                }
            }
            return text;
        }

        static string GetSqlExpressionBody(Expression exp)
        {
            var member = exp as MemberExpression;
            while (member != null)
            {
                if (typeof(ISqlExpressionBase).IsAssignableFrom(member.Type))
                {
                    return member.Member.Name;
                }
                member = member.Expression as MemberExpression;
            }
            return null;
        }
    }
}
