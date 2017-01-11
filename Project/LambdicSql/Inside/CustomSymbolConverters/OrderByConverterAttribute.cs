using LambdicSql.BuilderServices.Parts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.Inside;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class OrderByConverterAttribute : SymbolConverterMethodAttribute
    {
        public override CodeParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var arg = expression.Arguments[expression.SkipMethodChain(0)];
            var array = arg as NewArrayExpression;

            var orderBy = new VParts();
            orderBy.Add("ORDER BY");
            var sort = new VParts() { Separator = "," };
            sort.AddRange(1, array.Expressions.Select(e => converter.Convert(e)).ToList());
            orderBy.Add(sort);
            return orderBy;
        }
    }

}
