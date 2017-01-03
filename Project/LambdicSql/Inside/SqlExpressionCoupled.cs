using LambdicSql.ConverterServices;
using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.Inside
{
    class SqlExpressionCoupled<TSelected> : SqlExpression<TSelected>
    {
        public override BuildingParts BuildingParts { get; }

        public SqlExpressionCoupled(ISqlExpression before, ISqlExpression after)
        {
            BuildingParts = new VParts(before.BuildingParts, after.BuildingParts);
        }
    }
}
