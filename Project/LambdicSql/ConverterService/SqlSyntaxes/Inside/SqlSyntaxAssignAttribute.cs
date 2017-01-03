using LambdicSql.SqlBuilder.Sentences;
using System.Linq.Expressions;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{

    class SqlSyntaxAssignAttribute : SqlSyntaxConverterNewAttribute
    {
        public override Sentence Convert(ExpressionConverter converter, NewExpression exp)
        {
            Sentence arg1 = converter.Convert(exp.Arguments[0]).Customize(new CustomizeColumnOnly());
            return new HSentence(arg1, "=", converter.Convert(exp.Arguments[1])) { Separator = " " };
        }
    }
}
