using LambdicSql.SqlBuilder.ExpressionElements;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{

    class SqlSyntaxOverAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var v = new VText();
            var overMethod = method;
            v.Add(overMethod.Method.Name.ToUpper() + "(");
            v.AddRange(1, overMethod.Arguments.Skip(1).
                Where(e => !(e is ConstantExpression)). //Skip null.
                Select(e => converter.Convert(e)).ToArray());
            return v.ConcatToBack(")");
        }
    }
}
