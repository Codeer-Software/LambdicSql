using LambdicSql.SqlBuilder.ExpressionElements;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.ExpressionElements.Inside.SqlTextUtils;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxBetweenAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause(LineSpace(args[0], "BETWEEN"), args[1], "AND", args[2]);
        }
    }
}
