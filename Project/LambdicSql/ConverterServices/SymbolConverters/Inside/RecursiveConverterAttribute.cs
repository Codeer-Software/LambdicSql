using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Syntaxes;
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
            return Blanket(createInfo.Members.Select(e => (Syntax)e.Name).ToArray());
        }
    }
}
