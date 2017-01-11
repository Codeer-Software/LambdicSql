using LambdicSql.BuilderServices.Code;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.Inside;
using System.Linq.Expressions;
using static LambdicSql.Inside.CustomCodeParts.PartsFactoryUtils;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class ConditionClauseConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var condition = converter.Convert(expression.Arguments[expression.SkipMethodChain(0)]);
            if (condition.IsEmpty) return string.Empty;
            return Clause(expression.Method.Name.ToUpper(), condition);
        }
    }
}
