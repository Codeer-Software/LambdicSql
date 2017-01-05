using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Syntaxes;
using LambdicSql.BuilderServices.Syntaxes.Inside;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class TwoWaySqlConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var obj = converter.ToObject(expression.Arguments[0]);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);
            var array = expression.Arguments[1] as NewArrayExpression;
            return new StringFormatSyntax(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }
    }
}
