using LambdicSql.BuilderServices.Code;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class OverConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var over = new VParts();
            over.Add(expression.Method.Name.ToUpper() + "(");
            over.AddRange(1, expression.Arguments.Skip(1).
                Where(e => !(e is ConstantExpression)). //Skip null.
                Select(e => converter.Convert(e)));
            return over.ConcatToBack(")");
        }
    }
}
