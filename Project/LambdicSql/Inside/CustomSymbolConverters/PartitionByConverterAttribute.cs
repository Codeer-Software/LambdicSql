using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class PartitionByConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Code Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var partitionBy = new VCode();
            partitionBy.Add("PARTITION BY");

            var array = expression.Arguments[0] as NewArrayExpression;
            var args = new VCode(array.Expressions.Select(e => converter.Convert(e))) { Indent = 1, Separator = "," };
            partitionBy.Add(args);

            return partitionBy;
        }
    }
}
