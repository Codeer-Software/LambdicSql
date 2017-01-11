using LambdicSql.BuilderServices.Code;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CustomCodeParts;
using System.Linq.Expressions;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class AssignConverterAttribute : SymbolConverterNewAttribute
    {
        public override Parts Convert(NewExpression expression, ExpressionConverter converter)
        {
            Parts arg1 = converter.Convert(expression.Arguments[0]).Customize(new CustomizeColumnOnly());
            return new HParts(arg1, "=", converter.Convert(expression.Arguments[1])) { Separator = " " };
        }
    }
}
