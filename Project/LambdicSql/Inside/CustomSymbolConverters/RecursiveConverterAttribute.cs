using LambdicSql.BuilderServices.Parts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.Inside;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.Inside.CustomCodeParts.PartsFactoryUtils;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class RecursiveConverterAttribute : SymbolConverterMethodAttribute
    {
        public override CodeParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var selectTargets = expression.Arguments[expression.Arguments.Count - 1];
            var createInfo = ObjectCreateAnalyzer.MakeSelectInfo(selectTargets);
            return Blanket(createInfo.Members.Select(e => (CodeParts)e.Name).ToArray());
        }
    }
}
