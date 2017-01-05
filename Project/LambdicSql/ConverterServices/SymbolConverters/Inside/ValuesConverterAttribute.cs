using LambdicSql.BuilderServices.Syntaxes;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Syntaxes.Inside.SyntaxFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class ValuesConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var values = Func("VALUES", converter.Convert(expression.Arguments[1]));
            values.Indent = 1;
            return values;
        }
    }
}
