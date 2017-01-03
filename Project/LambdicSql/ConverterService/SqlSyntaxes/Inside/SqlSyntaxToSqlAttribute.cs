using LambdicSql.SqlBuilder.Sentences;
using LambdicSql.SqlBuilder.Sentences.Inside;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxToSqlAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override Sentence Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var text = (string)converter.ToObject(method.Arguments[0]);
            var array = method.Arguments[1] as NewArrayExpression;
            return new StringFormatText(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }
    }
}
