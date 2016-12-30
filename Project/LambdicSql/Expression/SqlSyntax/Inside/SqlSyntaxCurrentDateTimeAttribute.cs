using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Expression.SqlSyntax.Inside
{
    class SqlSyntaxCurrentDateTimeAttribute : SqlSyntaxMethodAttribute
    {
        class CurrentDateTimeExpressionElement : ExpressionElement
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

            public override bool IsSingleLine(ExpressionConvertingContext context) => true;

            public override bool IsEmpty => false;

            public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
                => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + "CURRENT" + context.Option.CurrentDateTimeSeparator + _core + _back;

            public override ExpressionElement ConcatAround(string front, string back)
                => new CurrentDateTimeExpressionElement(_core, front + _front, _back + back);

            public override ExpressionElement ConcatToFront(string front)
                => new CurrentDateTimeExpressionElement(_core, front + _front, _back);

            public override ExpressionElement ConcatToBack(string back)
                => new CurrentDateTimeExpressionElement(_core, _front, _back + back);

            public override ExpressionElement Customize(ISqlTextCustomizer customizer)
                => customizer.Custom(this);
        }

        public string Name { get; set; }

        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
            => new CurrentDateTimeExpressionElement(Name);
    }

}
