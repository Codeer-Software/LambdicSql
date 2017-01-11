using LambdicSql.BuilderServices.Parts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using System.Linq.Expressions;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class ConditionConverterAttribute : SymbolConverterNewAttribute
    {
        public override CodeParts Convert(NewExpression expression, ExpressionConverter converter)
        {
            var obj = converter.ToObject(expression.Arguments[0]);
            return (bool)obj ? converter.Convert(expression.Arguments[1]) : string.Empty;
        }
    }
}
