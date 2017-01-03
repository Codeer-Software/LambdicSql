using LambdicSql.SqlBuilder.Sentences;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.Sentences.Inside.SqlTextUtils;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxValuesAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override Sentence Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var values = Func("VALUES", converter.Convert(method.Arguments[1]));
            values.Indent = 1;
            return values;
        }
    }
}
