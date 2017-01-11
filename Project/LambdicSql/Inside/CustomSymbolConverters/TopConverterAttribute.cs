using LambdicSql.BuilderServices.Parts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CustomCodeParts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.Inside.CustomCodeParts.PartsFactoryUtils;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class TopConverterAttribute : SymbolConverterMethodAttribute
    {
        public override CodeParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var args = expression.Arguments.Select(e => converter.Convert(e)).ToArray();
            return LineSpace(expression.Method.Name.ToUpper(), args[0].Customize(new CustomizeParameterToObject()));
        }
    }
}
