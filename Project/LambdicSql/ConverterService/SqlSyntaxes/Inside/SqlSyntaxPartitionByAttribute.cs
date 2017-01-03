using LambdicSql.SqlBuilder.Parts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxPartitionByAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var partitionBy = new VBuildingParts();
            partitionBy.Add("PARTITION BY");

            var elements = new VBuildingParts() { Indent = 1, Separator = "," };
            var array = method.Arguments[0] as NewArrayExpression;
            foreach (var e in array.Expressions.Select(e => converter.Convert(e)))
            {
                elements.Add(e);
            }
            partitionBy.Add(elements);

            return partitionBy;
        }
    }
}
