using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Syntaxes;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Syntaxes.Inside.SyntaxFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class AllConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var args = expression.Arguments.Select(e => converter.Convert(e)).ToArray();
            return new DisableBracketsText(Func("ALL", args[0]));
        }

        //TODO そもそも括弧つけの手法が見直せたら良い
        internal class DisableBracketsText : Syntax
        {
            Syntax _core;

            internal DisableBracketsText(Syntax core)
            {
                _core = core;
            }

            public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

            public override bool IsEmpty => _core.IsEmpty;

            public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            {
                return _core.ToString(false, indent, context);
            }

            public override Syntax ConcatAround(string front, string back) => this;

            public override Syntax ConcatToFront(string front) => new DisableBracketsText(_core.ConcatToFront(front));

            public override Syntax ConcatToBack(string back) => new DisableBracketsText(_core.ConcatToBack(back));

            public override Syntax Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
        }
    }
}
