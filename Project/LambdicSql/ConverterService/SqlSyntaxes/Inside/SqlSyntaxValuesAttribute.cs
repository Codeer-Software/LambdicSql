using LambdicSql.SqlBuilder.Parts;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.Parts.Inside.SqlTextUtils;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxValuesAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var values = Func("VALUES", converter.Convert(method.Arguments[1]));
            values.Indent = 1;
            return values;
        }
    }
}
