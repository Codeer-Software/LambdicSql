using LambdicSql.ConverterServices.Inside;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.BuilderServices.Parts;
using System.Linq.Expressions;
using static LambdicSql.Inside.CustomCodeParts.PartsFactoryUtils;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class SpecialNameClauseConverterAttribute : SymbolConverterMethodAttribute
    {
        public string Name { get; set; }
        
        public override CodeParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var index = expression.SkipMethodChain(0);
            var name = (string)converter.ToObject(expression.Arguments[index]);
            return Clause(Name, name);
        }
    }
}
