using LambdicSql.BuilderServices.Parts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CustomCodeParts;
using System.Linq.Expressions;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class ColumnOnlyConverterAttribute : SymbolConverterMethodAttribute
    {
        public override CodeParts Convert(MethodCallExpression expression, ExpressionConverter converter)
            => converter.Convert(expression.Arguments[0]).Customize(new CustomizeColumnOnly());
    }
}
