using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Syntaxes;
using LambdicSql.BuilderServices.Syntaxes.Inside;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Syntaxes.Inside.SyntaxFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class RecursiveConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var selectTargets = expression.Arguments[expression.Arguments.Count - 1];
            var createInfo = ObjectCreateAnalyzer.MakeSelectInfo(selectTargets);
            return new RecursiveClauseText(createInfo, Blanket(createInfo.Members.Select(e => (Syntax)e.Name).ToArray()));
        }

        class RecursiveClauseText : Syntax
        {
            Syntax _core;
            ObjectCreateInfo _createInfo;

            internal RecursiveClauseText(ObjectCreateInfo createInfo, Syntax core)
            {
                _core = core;
                _createInfo = createInfo;
            }

            public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

            public override bool IsEmpty => _core.IsEmpty;

            public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            {
                return _core.ToString(isTopLevel, indent, context);
            }

            public override Syntax ConcatAround(string front, string back) => new SelectClauseSyntax(_createInfo, _core.ConcatAround(front, back));

            public override Syntax ConcatToFront(string front) => new SelectClauseSyntax(_createInfo, _core.ConcatToFront(front));

            public override Syntax ConcatToBack(string back) => new SelectClauseSyntax(_createInfo, _core.ConcatToBack(back));

            public override Syntax Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
        }
    }
}
