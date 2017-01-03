using LambdicSql.ConverterService.Inside;
using LambdicSql.SqlBuilder.Parts;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.Parts.Inside.SqlTextUtils;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxConditionClauseAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var condition = converter.Convert(method.Arguments[method.SkipMethodChain(0)]);
            if (condition.IsEmpty) return string.Empty;
            return Clause(method.Method.Name.ToUpper(), condition);
        }
    }
}
