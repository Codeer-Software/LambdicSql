using LambdicSql.SqlBuilder.ExpressionElements;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{

    class SqlSyntaxConditionAttribute : SqlSyntaxConverterNewAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, NewExpression exp)
        {
            var obj = converter.ToObject(exp.Arguments[0]);
            return (bool)obj ? converter.Convert(exp.Arguments[1]) : (ExpressionElement)string.Empty;
        }
    }
}
