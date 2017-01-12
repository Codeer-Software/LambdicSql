using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class SetConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Code Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var array = expression.Arguments[1] as NewArrayExpression;
            var set = new VCode();
            set.Add("SET");
            set.Add(new VCode(array.Expressions.Select(e => converter.Convert(e)).ToArray()) { Indent = 1, Separator = "," });
            return set;
        }
    }
}
