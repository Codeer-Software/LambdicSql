using LambdicSql.BuilderServices.Code;
using LambdicSql.BuilderServices.Code.Inside;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class ColumnOnlyConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
            => converter.Convert(expression.Arguments[0]).Customize(new CustomizeColumnOnly());
    }
}
