using LambdicSql.BuilderServices.Syntaxes;
using LambdicSql.BuilderServices.Syntaxes.Inside;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Syntaxes.Inside.SyntaxFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class AllConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var args = expression.Arguments.Select(e => converter.Convert(e)).ToArray();
            return new DisableBracketsSyntax(Func("ALL", args[0]));
        }
    }
}
