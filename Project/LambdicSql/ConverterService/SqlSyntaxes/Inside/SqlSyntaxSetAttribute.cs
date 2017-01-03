using LambdicSql.SqlBuilder.Sentences;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxSetAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override Sentence Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var array = method.Arguments[1] as NewArrayExpression;
            var sets = new VSentence();
            sets.Add("SET");
            sets.Add(new VSentence(array.Expressions.Select(e => converter.Convert(e)).ToArray()) { Indent = 1, Separator = "," });
            return sets;
        }
    }
}
