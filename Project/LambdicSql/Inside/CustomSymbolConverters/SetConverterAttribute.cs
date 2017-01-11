using LambdicSql.BuilderServices.Code;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class SetConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var array = expression.Arguments[1] as NewArrayExpression;
            var set = new VParts();
            set.Add("SET");
            set.Add(new VParts(array.Expressions.Select(e => converter.Convert(e)).ToArray()) { Indent = 1, Separator = "," });
            return set;
        }
    }
}
