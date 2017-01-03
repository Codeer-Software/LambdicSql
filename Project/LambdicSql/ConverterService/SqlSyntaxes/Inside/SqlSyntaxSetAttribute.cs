using LambdicSql.SqlBuilder.Parts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxSetAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var array = method.Arguments[1] as NewArrayExpression;
            var sets = new VParts();
            sets.Add("SET");
            sets.Add(new VParts(array.Expressions.Select(e => converter.Convert(e)).ToArray()) { Indent = 1, Separator = "," });
            return sets;
        }
    }
}
