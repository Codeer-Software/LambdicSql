using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Syntaxes;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Syntaxes.Inside.SyntaxFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class FromConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var startIndex = expression.SkipMethodChain(0);
            var table = ToTableName(converter, expression.Arguments[startIndex]);
            return new HSyntax(new Syntax[] { "FROM", table }) { IsFunctional = true, Separator = " " };
        }

        internal static Syntax ToTableName(ExpressionConverter decoder, Expression exp)
        {
            //where query, write tables side by side
            var arry = exp as NewArrayExpression;
            if (arry != null) return Arguments(arry.Expressions.Select(e => ToTableName(decoder, e)).ToArray());

            var table = decoder.Convert(exp);

            //TODO refactoring.
            var body = GetSqlExpressionBody(exp);
            if (body != null) return new SubQueryAndNameText(body, table);// new HText(table, body) { Separator = " ", EnableChangeLine = false };

            return table;
        }

        internal static string GetSqlExpressionBody(Expression exp)
        {
            var member = exp as MemberExpression;
            while (member != null)
            {
                if (typeof(ISqlExpression).IsAssignableFrom(member.Type)) return member.Member.Name;
                member = member.Expression as MemberExpression;
            }

            var method = exp as MethodCallExpression;
            if (method != null && 0 < method.Arguments.Count)
            {
                member = method.Arguments[0] as MemberExpression;
                if (member != null)
                {
                    if (typeof(ISqlExpression).IsAssignableFrom(member.Type)) return member.Member.Name;
                }
            }
            return null;
        }

        class SubQueryAndNameText : Syntax
        {
            string _front = string.Empty;
            string _back = string.Empty;
            string _body;
            Syntax _define;

            internal SubQueryAndNameText(string body, Syntax table)
            {
                _body = body;
                _define = new HSyntax(table, _body) { Separator = " ", EnableChangeLine = false };
            }

            SubQueryAndNameText(string body, Syntax define, string front, string back)
            {
                _body = body;
                _define = define;
                _front = front;
                _back = back;
            }

            public override bool IsSingleLine(BuildingContext context) => context.WithEntied.ContainsKey(_body) ? true : _define.IsSingleLine(context);

            public override bool IsEmpty => false;

            public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            {
                if (context.WithEntied.ContainsKey(_body))
                {
                    return string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + _body + _back;
                }
                return _define.ToString(isTopLevel, indent, context);
            }

            public override Syntax ConcatAround(string front, string back)
                => new SubQueryAndNameText(_body, _define.ConcatAround(front, back), front + _front, _back + back);

            public override Syntax ConcatToFront(string front)
                => new SubQueryAndNameText(_body, _define.ConcatToFront(front), front + _front, _back);

            public override Syntax ConcatToBack(string back)
                => new SubQueryAndNameText(_body, _define.ConcatToBack(back), _front, _back + back);

            public override Syntax Customize(ISyntaxCustomizer customizer)
                => customizer.Custom(this);
        }
    }

}
