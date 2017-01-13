using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.Inside.CodeParts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.SymbolConverters
{
    class TwoWaySqlConverterAttribute : MethodConverterAttribute
    {
        public override Code Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var obj = converter.ToObject(expression.Arguments[0]);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);
            var array = expression.Arguments[1] as NewArrayExpression;
            return new StringFormatCode(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }
    }
}
