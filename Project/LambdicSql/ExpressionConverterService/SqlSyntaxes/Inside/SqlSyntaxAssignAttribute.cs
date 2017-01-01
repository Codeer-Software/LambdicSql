using LambdicSql.SqlBuilder.ExpressionElements;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{

    class SqlSyntaxAssignAttribute : SqlSyntaxConverterNewAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, NewExpression exp)
        {
            ExpressionElement arg1 = converter.Convert(exp.Arguments[0]).Customize(new CustomizeColumnOnly());
            return new HText(arg1, "=", converter.Convert(exp.Arguments[1])) { Separator = " " };
        }
    }
}
