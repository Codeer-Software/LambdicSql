using LambdicSql.SqlBuilder.ExpressionElements;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.ExpressionElements.Inside.SqlTextUtils;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{

    class SqlSyntaxSortedByAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
            => LineSpace(converter.Convert(method.Arguments[0]), method.Method.Name.ToUpper());
    }
}
