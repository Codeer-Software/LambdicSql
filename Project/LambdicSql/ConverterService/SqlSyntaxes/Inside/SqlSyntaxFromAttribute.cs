using LambdicSql.ConverterService.Inside;
using LambdicSql.SqlBuilder;
using LambdicSql.SqlBuilder.Sentences;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.Sentences.Inside.SqlTextUtils;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxFromAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override Sentence Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var startIndex = method.SkipMethodChain(0);
            var table = ToTableName(converter, method.Arguments[startIndex]);
            return new HSentence(new Sentence[] { "FROM", table }) { IsFunctional = true, Separator = " " };
        }

        internal static Sentence ToTableName(ExpressionConverter decoder, Expression exp)
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

        class SubQueryAndNameText : Sentence
        {
            string _front = string.Empty;
            string _back = string.Empty;
            string _body;
            Sentence _define;

            internal SubQueryAndNameText(string body, Sentence table)
            {
                _body = body;
                _define = new HSentence(table, _body) { Separator = " ", EnableChangeLine = false };
            }

            SubQueryAndNameText(string body, Sentence define, string front, string back)
            {
                _body = body;
                _define = define;
                _front = front;
                _back = back;
            }

            public override bool IsSingleLine(SqlBuildingContext context) => context.WithEntied.ContainsKey(_body) ? true : _define.IsSingleLine(context);

            public override bool IsEmpty => false;

            public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
            {
                if (context.WithEntied.ContainsKey(_body))
                {
                    return string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + _body + _back;
                }
                return _define.ToString(isTopLevel, indent, context);
            }

            public override Sentence ConcatAround(string front, string back)
                => new SubQueryAndNameText(_body, _define.ConcatAround(front, back), front + _front, _back + back);

            public override Sentence ConcatToFront(string front)
                => new SubQueryAndNameText(_body, _define.ConcatToFront(front), front + _front, _back);

            public override Sentence ConcatToBack(string back)
                => new SubQueryAndNameText(_body, _define.ConcatToBack(back), _front, _back + back);

            public override Sentence Customize(ISqlTextCustomizer customizer)
                => customizer.Custom(this);
        }
    }

}
