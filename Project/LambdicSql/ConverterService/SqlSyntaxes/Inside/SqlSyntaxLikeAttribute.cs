using LambdicSql.SqlBuilder.Sentences;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.Sentences.Inside.SqlTextUtils;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{

    class SqlSyntaxLikeAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override Sentence Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause(LineSpace(args[0], "LIKE"), args[1]);
        }
    }
}
