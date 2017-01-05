using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Syntaxes;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class CurrentDateTimeConverterAttribute : SymbolConverterMethodAttribute
    {
        class CurrentDateTimeExpressionElement : Syntax
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

            public override bool IsSingleLine(BuildingContext context) => true;

            public override bool IsEmpty => false;

            public override string ToString(bool isTopLevel, int indent, BuildingContext context)
                => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + "CURRENT" + context.Option.CurrentDateTimeSeparator + _core + _back;

            public override Syntax ConcatAround(string front, string back)
                => new CurrentDateTimeExpressionElement(_core, front + _front, _back + back);

            public override Syntax ConcatToFront(string front)
                => new CurrentDateTimeExpressionElement(_core, front + _front, _back);

            public override Syntax ConcatToBack(string back)
                => new CurrentDateTimeExpressionElement(_core, _front, _back + back);

            public override Syntax Customize(ISyntaxCustomizer customizer)
                => customizer.Custom(this);
        }

        public string Name { get; set; }

        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
            => new CurrentDateTimeExpressionElement(Name);
    }

}
