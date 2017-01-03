using LambdicSql.SqlBuilder.Sentences;
using System.Linq.Expressions;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{

    class SqlSyntaxConditionAttribute : SqlSyntaxConverterNewAttribute
    {
        public override Sentence Convert(ExpressionConverter converter, NewExpression exp)
        {
            var obj = converter.ToObject(exp.Arguments[0]);
            return (bool)obj ? converter.Convert(exp.Arguments[1]) : (Sentence)string.Empty;
        }
    }
}
