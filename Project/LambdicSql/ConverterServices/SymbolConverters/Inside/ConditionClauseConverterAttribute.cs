using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Code;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Code.Inside.PartsFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
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
