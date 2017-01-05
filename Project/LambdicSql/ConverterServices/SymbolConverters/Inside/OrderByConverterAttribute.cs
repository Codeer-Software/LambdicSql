using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Syntaxes;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class OrderByConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var arg = expression.Arguments[expression.SkipMethodChain(0)];
            var array = arg as NewArrayExpression;

            var orderBy = new VSyntax();
            orderBy.Add("ORDER BY");
            var sort = new VSyntax() { Separator = "," };
            sort.AddRange(1, array.Expressions.Select(e => converter.Convert(e)).ToList());
            orderBy.Add(sort);
            return orderBy;
        }
    }

}
