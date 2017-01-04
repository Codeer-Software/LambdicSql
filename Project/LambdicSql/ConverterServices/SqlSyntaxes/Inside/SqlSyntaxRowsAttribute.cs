using LambdicSql.BuilderServices.Parts;
using LambdicSql.BuilderServices.Parts.Inside;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Parts.Inside.BuildingPartsFactoryUtils;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{

    class SqlSyntaxRowsAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var args = expression.Arguments.Select(e => converter.Convert(e)).ToArray();

            //Sql server can't use parameter.
            if (expression.Arguments.Count == 1)
            {
                return LineSpace("ROWS", args[0].Customize(new CustomizeParameterToObject()), "PRECEDING");
            }
            else
            {
                return LineSpace("ROWS BETWEEN", args[0].Customize(new CustomizeParameterToObject()),
                    "PRECEDING AND", args[1].Customize(new CustomizeParameterToObject()), "FOLLOWING");
            }
        }
    }
}
