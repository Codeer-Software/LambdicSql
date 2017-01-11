using LambdicSql.BuilderServices.Code;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using System.Linq.Expressions;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class DirectValueConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var obj = converter.ToObject(expression.Arguments[0]);
            return (obj == null) ? "NULL" : obj.ToString();
        }
    }
}
