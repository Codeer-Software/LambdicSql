using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxConverter.Inside
{

    class SqlSyntaxRowsAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();

            //Sql server can't use parameter.
            if (method.Arguments.Count == 1)
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
