using LambdicSql.BuilderServices.Code;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Code.Inside.PartsFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class SortedByConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
            => LineSpace(converter.Convert(expression.Arguments[0]), expression.Method.Name.ToUpper());
    }
}
