using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Code;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Code.Inside.PartsFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class SpecialNameClauseConverterAttribute : SymbolConverterMethodAttribute
    {
        public string Name { get; set; }
        
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var index = expression.SkipMethodChain(0);
            var name = (string)converter.ToObject(expression.Arguments[index]);
            return Clause(Name, name);
        }
    }
}
