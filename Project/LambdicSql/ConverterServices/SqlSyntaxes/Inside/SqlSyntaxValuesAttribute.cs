using LambdicSql.BuilderServices.Parts;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Parts.Inside.BuildingPartsFactoryUtils;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{
    class SqlSyntaxValuesAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var values = Func("VALUES", converter.Convert(method.Arguments[1]));
            values.Indent = 1;
            return values;
        }
    }
}
