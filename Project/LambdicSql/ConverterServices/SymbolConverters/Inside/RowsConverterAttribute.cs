using LambdicSql.BuilderServices.Code;
using LambdicSql.BuilderServices.Code.Inside;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Code.Inside.PartsFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class RowsConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var args = expression.Arguments.Select(e => converter.Convert(e)).ToArray();
            
            //Sql server can't use parameter.
            if (expression.Arguments.Count == 1)
            {
                return LineSpace("ROWS", args[0].Customize(new CustomizeParameterForRowsToObject()), "PRECEDING");
            }
            else
            {
                return LineSpace("ROWS BETWEEN", args[0].Customize(new CustomizeParameterForRowsToObject()),
                    "PRECEDING AND", args[1].Customize(new CustomizeParameterForRowsToObject()), "FOLLOWING");
            }
        }
    }
}
