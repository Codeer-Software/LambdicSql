using LambdicSql.BuilderServices.Code;
using LambdicSql.BuilderServices.Code.Inside;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
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
