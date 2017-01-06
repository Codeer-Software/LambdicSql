using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Code;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Code.Inside.PartsFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class RecursiveConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var selectTargets = expression.Arguments[expression.Arguments.Count - 1];
            var createInfo = ObjectCreateAnalyzer.MakeSelectInfo(selectTargets);
            return Blanket(createInfo.Members.Select(e => (Parts)e.Name).ToArray());
        }
    }
}
