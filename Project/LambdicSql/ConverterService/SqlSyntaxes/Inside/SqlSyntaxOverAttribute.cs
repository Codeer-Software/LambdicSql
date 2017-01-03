using LambdicSql.SqlBuilder.Sentences;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{

    class SqlSyntaxOverAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override Sentence Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var v = new VSentence();
            var overMethod = method;
            v.Add(overMethod.Method.Name.ToUpper() + "(");
            v.AddRange(1, overMethod.Arguments.Skip(1).
                Where(e => !(e is ConstantExpression)). //Skip null.
                Select(e => converter.Convert(e)).ToArray());
            return v.ConcatToBack(")");
        }
    }
}
