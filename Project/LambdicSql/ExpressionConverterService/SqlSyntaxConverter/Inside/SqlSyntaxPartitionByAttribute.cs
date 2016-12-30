using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxConverter.Inside
{
    class SqlSyntaxPartitionByAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var partitionBy = new VText();
            partitionBy.Add("PARTITION BY");

            var elements = new VText() { Indent = 1, Separator = "," };
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
