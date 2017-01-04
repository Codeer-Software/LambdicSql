using LambdicSql.BuilderServices.Parts;
using LambdicSql.BuilderServices.Parts.Inside;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{
    class SqlSyntaxToSqlAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var text = (string)converter.ToObject(expression.Arguments[0]);
            var array = expression.Arguments[1] as NewArrayExpression;
            return new StringFormatParts(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }
    }
}
