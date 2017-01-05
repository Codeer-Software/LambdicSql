using LambdicSql.BuilderServices.Syntaxes;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class SetConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var array = expression.Arguments[1] as NewArrayExpression;
            var set = new VSyntax();
            set.Add("SET");
            set.Add(new VSyntax(array.Expressions.Select(e => converter.Convert(e)).ToArray()) { Indent = 1, Separator = "," });
            return set;
        }
    }
}
