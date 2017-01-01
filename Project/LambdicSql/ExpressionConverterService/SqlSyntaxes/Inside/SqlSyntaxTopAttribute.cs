using LambdicSql.SqlBuilder.ExpressionElements;
using LambdicSql.SqlBuilder.ExpressionElements.Inside;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.ExpressionElements.Inside.SqlTextUtils;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxTopAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return LineSpace(method.Method.Name.ToUpper(), args[0].Customize(new CustomizeParameterToObject()));
        }
    }
}
