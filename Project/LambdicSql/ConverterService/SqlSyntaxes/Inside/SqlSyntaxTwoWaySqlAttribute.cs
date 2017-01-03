using LambdicSql.ConverterService.Inside;
using LambdicSql.SqlBuilder.Sentences;
using LambdicSql.SqlBuilder.Sentences.Inside;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxTwoWaySqlAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override Sentence Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var obj = converter.ToObject(method.Arguments[0]);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);
            var array = method.Arguments[1] as NewArrayExpression;
            return new StringFormatText(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }
    }
}
