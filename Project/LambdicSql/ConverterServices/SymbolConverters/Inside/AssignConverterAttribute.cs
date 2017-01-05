using LambdicSql.BuilderServices.Syntaxes;
using LambdicSql.BuilderServices.Syntaxes.Inside;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class AssignConverterAttribute : SymbolConverterNewAttribute
    {
        public override Syntax Convert(NewExpression expression, ExpressionConverter converter)
        {
            Syntax arg1 = converter.Convert(expression.Arguments[0]).Customize(new CustomizeColumnOnly());
            return new HSyntax(arg1, "=", converter.Convert(expression.Arguments[1])) { Separator = " " };
        }
    }
}
