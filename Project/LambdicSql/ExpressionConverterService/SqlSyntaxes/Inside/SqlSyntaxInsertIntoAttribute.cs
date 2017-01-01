using LambdicSql.SqlBuilder.ExpressionElements;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.ExpressionElements.Inside.SqlTextUtils;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxInsertIntoAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var table = converter.Convert(method.Arguments[0]);

            //column should not have a table name.
            var arg = converter.Convert(method.Arguments[1]).Customize(new CustomizeColumnOnly());
            return Func(LineSpace("INSERT INTO", table), arg);
        }
    }
}
