using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxConverter.Inside
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
