using LambdicSql.BuilderServices.Parts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{

    class SqlSyntaxOverAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var v = new VParts();
            v.Add(expression.Method.Name.ToUpper() + "(");
            v.AddRange(1, expression.Arguments.Skip(1).
                Where(e => !(e is ConstantExpression)). //Skip null.
                Select(e => converter.Convert(e)).ToArray());
            return v.ConcatToBack(")");
        }
    }
}
