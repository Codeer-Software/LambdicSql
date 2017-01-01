using LambdicSql.SqlBuilder.ExpressionElements;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.ExpressionElements.Inside.SqlTextUtils;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxExtractAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return FuncSpace(method.Method.Name.ToUpper(), args[0], "FROM", args[1]);
        }
    }
}
