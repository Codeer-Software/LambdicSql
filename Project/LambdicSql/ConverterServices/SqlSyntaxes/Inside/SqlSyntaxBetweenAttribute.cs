using LambdicSql.BuilderServices.Parts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Parts.Inside.BuildingPartsFactoryUtils;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{
    class SqlSyntaxBetweenAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var args = expression.Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause(LineSpace(args[0], "BETWEEN"), args[1], "AND", args[2]);
        }
    }
}
