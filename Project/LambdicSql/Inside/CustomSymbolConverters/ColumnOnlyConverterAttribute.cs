using LambdicSql.BuilderServices.Code;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CustomCodeParts;
using System.Linq.Expressions;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class ColumnOnlyConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
            => converter.Convert(expression.Arguments[0]).Customize(new CustomizeColumnOnly());
    }
}
