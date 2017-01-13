using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CodeParts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.SymbolConverters
{
    class ToSqlConverterAttribute : MethodConverterAttribute
    {
        public override Code Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var text = (string)converter.ToObject(expression.Arguments[0]);
            var array = expression.Arguments[1] as NewArrayExpression;
            return new StringFormatCode(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }
    }
}
