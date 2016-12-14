using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class FromClause
    {
        internal static TextParts ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
            => new VText(methods.Select(m=> MethodToString(converter, m)).ToArray());

        static TextParts MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            HText name = null;
            var startIndex = method.SkipMethodChain(0);
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.From):
                    return new HText("FROM", ExpressionToTableName(converter, method.Arguments[startIndex])) { Separator = " ", IsFunctional = true };
                case nameof(LambdicSql.Keywords.CrossJoin):
                    return new HText("CROSS JOIN", ExpressionToTableName(converter, method.Arguments[startIndex])) { Separator = " ", IsFunctional = true, Indent = 1 };
                case nameof(LambdicSql.Keywords.LeftJoin):
                    name = new HText("LEFT JOIN") { Separator = " ", IsFunctional = true, Indent = 1 };
                    break;
                case nameof(LambdicSql.Keywords.RightJoin):
                    name = new HText("RIGHT JOIN") { Separator = " ", IsFunctional = true, Indent = 1 };
                    break;
                case nameof(LambdicSql.Keywords.Join):
                    name = new HText("JOIN") { Separator = " ", IsFunctional = true, Indent = 1 };
                    break;
            }
            var condition = converter.ToString(method.Arguments[startIndex + 1]);
            name.AddRange(ExpressionToTableName(converter, method.Arguments[startIndex]), "ON", condition);
            return name;
        }

        static TextParts ExpressionToTableName(ISqlStringConverter decoder, Expression exp)
            => SqlDisplayAdjuster.AdjustSubQuery(exp, ExpressionToTableNameCore(decoder, exp));

        static TextParts ExpressionToTableNameCore(ISqlStringConverter decoder, Expression exp)
        {
            var arry = exp as NewArrayExpression;
            if (arry != null)
            {
                return new HText(arry.Expressions.Select(e => ExpressionToTableName(decoder, e)).ToArray()) { Separator = "," };
            }

            var text = decoder.ToString(exp);

            var methodCall = exp as MethodCallExpression;
            if (methodCall != null)
            {
                var member = methodCall.Arguments[0] as MemberExpression;
                if (member != null)
                {
                    return new HText(text, member.Member.Name) { Separator = " ", EnableChangeLine = false };
                }
                return text;
            }

            //From clause only
            var body = GetSqlExpressionBody(exp);
            if (body != null)
            {
                return new HText(text, body) { Separator = " ", EnableChangeLine = false };
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
