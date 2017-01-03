using LambdicSql.SqlBuilder.Parts;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.Parts.Inside.SqlTextUtils;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{

    class SqlSyntaxSortedByAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
            => LineSpace(converter.Convert(method.Arguments[0]), method.Method.Name.ToUpper());
    }
}
