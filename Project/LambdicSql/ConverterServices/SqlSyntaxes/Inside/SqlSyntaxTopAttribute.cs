using LambdicSql.BuilderServices.Parts;
using LambdicSql.BuilderServices.Parts.Inside;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Parts.Inside.BuildingPartsFactoryUtils;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{
    class SqlSyntaxTopAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var args = expression.Arguments.Select(e => converter.Convert(e)).ToArray();
            return LineSpace(expression.Method.Name.ToUpper(), args[0].Customize(new CustomizeParameterToObject()));
        }
    }
}
