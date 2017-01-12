using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CustomCodeParts;
using System.Linq.Expressions;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class AssignConverterAttribute : SymbolConverterNewAttribute
    {
        public override Code Convert(NewExpression expression, ExpressionConverter converter)
        {
            Code arg1 = converter.Convert(expression.Arguments[0]).Customize(new CustomizeColumnOnly());
            return new HCode(arg1, "=", converter.Convert(expression.Arguments[1])) { Separator = " " };
        }
    }
}
