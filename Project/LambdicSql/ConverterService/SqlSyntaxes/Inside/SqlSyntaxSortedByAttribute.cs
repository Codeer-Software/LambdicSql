using LambdicSql.SqlBuilder.Sentences;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.Sentences.Inside.SqlTextUtils;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{

    class SqlSyntaxSortedByAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override Sentence Convert(ExpressionConverter converter, MethodCallExpression method)
            => LineSpace(converter.Convert(method.Arguments[0]), method.Method.Name.ToUpper());
    }
}
