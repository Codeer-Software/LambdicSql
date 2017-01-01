using LambdicSql.SqlBuilder.ExpressionElements;
using LambdicSql.SqlBuilder.ExpressionElements.Inside;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.ExpressionElements.Inside.SqlTextUtils;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxCastAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return FuncSpace("CAST", args[0], "AS", args[1].Customize(new CustomizeParameterToObject()));
        }
    }
}
