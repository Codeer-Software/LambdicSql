using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxConverter.Inside
{
    class SqlSyntaxSetAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var array = method.Arguments[1] as NewArrayExpression;
            var sets = new VText();
            sets.Add("SET");
            sets.Add(new VText(array.Expressions.Select(e => converter.Convert(e)).ToArray()) { Indent = 1, Separator = "," });
            return sets;
        }
    }
}
