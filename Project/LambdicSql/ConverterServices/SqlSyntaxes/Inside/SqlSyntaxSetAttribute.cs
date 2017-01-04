using LambdicSql.BuilderServices.Parts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{
    class SqlSyntaxSetAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var array = expression.Arguments[1] as NewArrayExpression;
            var sets = new VParts();
            sets.Add("SET");
            sets.Add(new VParts(array.Expressions.Select(e => converter.Convert(e)).ToArray()) { Indent = 1, Separator = "," });
            return sets;
        }
    }
}
