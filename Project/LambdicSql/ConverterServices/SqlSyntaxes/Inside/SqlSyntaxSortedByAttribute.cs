using LambdicSql.BuilderServices.Parts;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Parts.Inside.BuildingPartsFactoryUtils;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{

    class SqlSyntaxSortedByAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
            => LineSpace(converter.Convert(expression.Arguments[0]), expression.Method.Name.ToUpper());
    }
}
