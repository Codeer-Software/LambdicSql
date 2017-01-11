using LambdicSql.BuilderServices.Parts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class PartitionByConverterAttribute : SymbolConverterMethodAttribute
    {
        public override CodeParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var partitionBy = new VParts();
            partitionBy.Add("PARTITION BY");

            var array = expression.Arguments[0] as NewArrayExpression;
            var args = new VParts(array.Expressions.Select(e => converter.Convert(e))) { Indent = 1, Separator = "," };
            partitionBy.Add(args);

            return partitionBy;
        }
    }
}
