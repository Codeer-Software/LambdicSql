using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Parts;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Parts.Inside.BuildingPartsFactoryUtils;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{
    class SqlSyntaxConditionClauseAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var condition = converter.Convert(expression.Arguments[expression.SkipMethodChain(0)]);
            if (condition.IsEmpty) return string.Empty;
            return Clause(expression.Method.Name.ToUpper(), condition);
        }
    }
}
