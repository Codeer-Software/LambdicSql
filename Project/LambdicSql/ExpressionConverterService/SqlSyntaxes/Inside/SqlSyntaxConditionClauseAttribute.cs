using LambdicSql.ExpressionConverterService.Inside;
using LambdicSql.SqlBuilder.ExpressionElements;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.ExpressionElements.Inside.SqlTextUtils;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxConditionClauseAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var condition = converter.Convert(method.Arguments[method.SkipMethodChain(0)]);
            if (condition.IsEmpty) return string.Empty;
            return Clause(method.Method.Name.ToUpper(), condition);
        }
    }
}
