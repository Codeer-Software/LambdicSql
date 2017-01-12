using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CustomCodeParts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Inside.PartsFactoryUtils;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class AllConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Code Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var args = expression.Arguments.Select(e => converter.Convert(e)).ToArray();
            return new DisableBracketsCode(Func("ALL", args[0]));
        }
    }
}
