using LambdicSql.BuilderServices.Syntaxes;
using LambdicSql.BuilderServices.Syntaxes.Inside;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Syntaxes.Inside.SyntaxFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class InsertIntoConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var table = converter.Convert(expression.Arguments[0]);

            //column should not have a table name.
            var args = converter.Convert(expression.Arguments[1]).Customize(new CustomizeColumnOnly());
            return Func(LineSpace("INSERT INTO", table), args);
        }
    }
}
