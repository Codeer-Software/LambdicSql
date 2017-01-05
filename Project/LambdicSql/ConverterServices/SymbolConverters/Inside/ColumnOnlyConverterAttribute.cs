using LambdicSql.BuilderServices.Syntaxes;
using LambdicSql.BuilderServices.Syntaxes.Inside;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class ColumnOnlyConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
            => converter.Convert(expression.Arguments[0]).Customize(new CustomizeColumnOnly());
    }
}
