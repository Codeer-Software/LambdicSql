using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxConverter.Inside
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
