using LambdicSql.BuilderServices.Parts;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{

    class ConditionConverterAttribute : SymbolConverterNewAttribute
    {
        public override BuildingParts Convert(NewExpression expression, ExpressionConverter converter)
        {
            var obj = converter.ToObject(expression.Arguments[0]);
            return (bool)obj ? converter.Convert(expression.Arguments[1]) : (BuildingParts)string.Empty;
        }
    }
}
