using LambdicSql.SqlBuilder;
using LambdicSql.SqlBuilder.Sentences;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxCurrentDateTimeAttribute : SqlSyntaxConverterMethodAttribute
    {
        class CurrentDateTimeExpressionElement : Sentence
        {
            string _front = string.Empty;
            string _back = string.Empty;
            string _core;

            internal CurrentDateTimeExpressionElement(string core)
            {
                _core = core;
            }

            CurrentDateTimeExpressionElement(string core, string front, string back)
            {
                _core = core;
                _front = front;
                _back = back;
            }

            public override bool IsSingleLine(SqlBuildingContext context) => true;

            public override bool IsEmpty => false;

            public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
                => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + "CURRENT" + context.Option.CurrentDateTimeSeparator + _core + _back;

            public override Sentence ConcatAround(string front, string back)
                => new CurrentDateTimeExpressionElement(_core, front + _front, _back + back);

            public override Sentence ConcatToFront(string front)
                => new CurrentDateTimeExpressionElement(_core, front + _front, _back);

            public override Sentence ConcatToBack(string back)
                => new CurrentDateTimeExpressionElement(_core, _front, _back + back);

            public override Sentence Customize(ISqlTextCustomizer customizer)
                => customizer.Custom(this);
        }

        public string Name { get; set; }

        public override Sentence Convert(ExpressionConverter converter, MethodCallExpression method)
            => new CurrentDateTimeExpressionElement(Name);
    }

}
