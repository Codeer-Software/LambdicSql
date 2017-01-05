using LambdicSql.BuilderServices.Syntaxes;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Syntaxes.Inside.SyntaxFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class SortedByConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
            => LineSpace(converter.Convert(expression.Arguments[0]), expression.Method.Name.ToUpper());
    }
}
