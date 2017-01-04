using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Parts;
using LambdicSql.BuilderServices.Parts.Inside;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{
    class SqlSyntaxTwoWaySqlAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var obj = converter.ToObject(expression.Arguments[0]);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);
            var array = expression.Arguments[1] as NewArrayExpression;
            return new StringFormatParts(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }
    }
}
