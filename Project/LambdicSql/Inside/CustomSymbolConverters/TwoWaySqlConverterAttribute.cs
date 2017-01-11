using LambdicSql.BuilderServices.Parts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.Inside.CustomCodeParts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class TwoWaySqlConverterAttribute : SymbolConverterMethodAttribute
    {
        public override CodeParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var obj = converter.ToObject(expression.Arguments[0]);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);
            var array = expression.Arguments[1] as NewArrayExpression;
            return new StringFormatParts(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }
    }
}
