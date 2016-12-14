using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class FromClause
    {
        internal static IText ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
            => new VerticalText(methods.Select(m=> MethodToString(converter, m)).ToArray());

        static IText MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            HorizontalText name = null;
            var startIndex = method.SkipMethodChain(0);
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.From):
                    return new HorizontalText() { Separator = " ", IsFunctional = true } + "FROM" + ExpressionToTableName(converter, method.Arguments[startIndex]);
                case nameof(LambdicSql.Keywords.CrossJoin):
                    return new HorizontalText() { Separator = " ", IsFunctional = true, Indent = 1 } + "CROSS JOIN" + ExpressionToTableName(converter, method.Arguments[startIndex]);
                case nameof(LambdicSql.Keywords.LeftJoin):
                    name = new HorizontalText() { Separator = " ", IsFunctional = true, Indent = 1 } + "LEFT JOIN";
                    break;
                case nameof(LambdicSql.Keywords.RightJoin):
                    name = new HorizontalText() { Separator = " ", IsFunctional = true, Indent = 1 } + "RIGHT JOIN";
                    break;
                case nameof(LambdicSql.Keywords.Join):
                    name = new HorizontalText() { Separator = " ", IsFunctional = true, Indent = 1 } + "JOIN";
                    break;
            }
            var condition = converter.ToString(method.Arguments[startIndex + 1]);
            return name + ExpressionToTableName(converter, method.Arguments[startIndex]) + "ON" + condition;
        }

        static IText ExpressionToTableName(ISqlStringConverter decoder, Expression exp)
            => SqlDisplayAdjuster.AdjustSubQuery(exp, ExpressionToTableNameCore(decoder, exp));

        static IText ExpressionToTableNameCore(ISqlStringConverter decoder, Expression exp)
        {
            var arry = exp as NewArrayExpression;
            if (arry != null)
            {
                return new HorizontalText(arry.Expressions.Select(e => ExpressionToTableName(decoder, e)).ToArray()) { Separator = "," };
            }

            var text = decoder.ToString(exp);

            var methodCall = exp as MethodCallExpression;
            if (methodCall != null)
            {
                var member = methodCall.Arguments[0] as MemberExpression;
                if (member != null)
                {
                    var x = member.Member.Name;
                    return new HorizontalText() { Separator = " ", IsNotLineChange = true } + text + x;
                }
                return text;
            }

            //From clause only
            var body = GetSqlExpressionBody(exp);
            if (body != null)
            {
                return new HorizontalText() { Separator = " ", IsNotLineChange = true } + text + body;
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
