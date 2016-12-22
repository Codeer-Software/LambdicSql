using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class FromClause
    {
        internal static SqlText ConvertFrom(ISqlStringConverter converter, MethodCallExpression[] methods)
            => ConvertNonCodition(Clause, "FROM", converter, methods);

        internal static SqlText ConvertCrossJoin(ISqlStringConverter converter, MethodCallExpression[] methods)
            => ConvertNonCodition(SubClause, "CROSS JOIN", converter, methods);

        internal static SqlText ConvertLeftJoin(ISqlStringConverter converter, MethodCallExpression[] methods)
            => ConvertCondition("LEFT JOIN", converter, methods);

        internal static SqlText ConvertRightJoin(ISqlStringConverter converter, MethodCallExpression[] methods)
            => ConvertCondition("RIGHT JOIN", converter, methods);

        internal static SqlText ConvertJoin(ISqlStringConverter converter, MethodCallExpression[] methods)
            => ConvertCondition("JOIN", converter, methods);

        static SqlText ConvertNonCodition(Func<SqlText, SqlText[], HText> makeSqlText, string name, ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0]; var startIndex = method.SkipMethodChain(0);
            var table = ToTableName(converter, method.Arguments[startIndex]);
            return makeSqlText(name, new[] { table });
        }

        static SqlText ConvertCondition(string name, ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0]; var startIndex = method.SkipMethodChain(0);
            var table = ToTableName(converter, method.Arguments[startIndex]);
            var condition = (startIndex + 1) < method.Arguments.Count ? converter.Convert(method.Arguments[startIndex + 1]) : null;
            return SubClause(name, table, "ON", condition);
        }

        static SqlText ToTableName(ISqlStringConverter decoder, Expression exp)
        {
            //where query, write tables side by side
            var arry = exp as NewArrayExpression;
            if (arry != null) return Arguments(arry.Expressions.Select(e => ToTableName(decoder, e)).ToArray());

            var table = decoder.Convert(exp);

            //TODO refactoring.
            var body = GetSqlExpressionBody(exp);
            if (body != null) return new HText(table, body) { Separator = " ", EnableChangeLine = false };

            return table;
        }

        static string GetSqlExpressionBody(Expression exp)
        {
            var member = exp as MemberExpression;
            while (member != null)
            {
                if (typeof(ISqlExpressionBase).IsAssignableFrom(member.Type)) return member.Member.Name;
                member = member.Expression as MemberExpression;
            }

            var method = exp as MethodCallExpression;
            if (method != null)
            {
                member = method.Arguments[0] as MemberExpression;
                if (member != null)
                {
                    if (typeof(ISqlExpressionBase).IsAssignableFrom(member.Type)) return member.Member.Name;
                }
            //    return ((MemberExpression)method.Arguments[0]).Member.Name;
            }
            return null;
        }
    }
}
