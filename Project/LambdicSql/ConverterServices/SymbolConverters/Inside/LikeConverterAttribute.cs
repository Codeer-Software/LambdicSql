using LambdicSql.BuilderServices.Syntaxes;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Syntaxes.Inside.SyntaxFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class LikeConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var args = expression.Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause(LineSpace(args[0], "LIKE"), args[1]);
        }
    }
}
