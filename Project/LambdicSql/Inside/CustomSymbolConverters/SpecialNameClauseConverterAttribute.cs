using LambdicSql.ConverterServices.Inside;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.BuilderServices.CodeParts;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Inside.PartsFactoryUtils;

namespace LambdicSql.Inside.CustomSymbolConverters
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
