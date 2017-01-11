using LambdicSql.BuilderServices.Parts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using System.Linq.Expressions;
using static LambdicSql.Inside.CustomCodeParts.PartsFactoryUtils;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class SortedByConverterAttribute : SymbolConverterMethodAttribute
    {
        public override CodeParts Convert(MethodCallExpression expression, ExpressionConverter converter)
            => LineSpace(converter.Convert(expression.Arguments[0]), expression.Method.Name.ToUpper());
    }
}
