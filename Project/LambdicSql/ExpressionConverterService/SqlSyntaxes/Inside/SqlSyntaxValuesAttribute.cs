using LambdicSql.SqlBuilder.ExpressionElements;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.ExpressionElements.Inside.SqlTextUtils;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxValuesAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var values = Func("VALUES", converter.Convert(method.Arguments[1]));
            values.Indent = 1;
            return values;
        }
    }
}
