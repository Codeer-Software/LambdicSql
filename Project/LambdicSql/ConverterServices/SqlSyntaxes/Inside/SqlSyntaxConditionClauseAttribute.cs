using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Parts;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Parts.Inside.BuildingPartsFactoryUtils;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
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
