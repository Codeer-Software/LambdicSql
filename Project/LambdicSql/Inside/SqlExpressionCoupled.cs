using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;

namespace LambdicSql.Inside
{
    class SqlExpressionCoupled<TSelected> : SqlExpression<TSelected>
    {
        public override DbInfo DbInfo { get; protected set; }
        public override ExpressionElement ExpressionElement { get; }

        public SqlExpressionCoupled(ISqlExpression before, ISqlExpression after)
        {
            if (before.DbInfo != null) DbInfo = before.DbInfo;
            else if (before.DbInfo != null) DbInfo = before.DbInfo;
            ExpressionElement = new VText(before.ExpressionElement, after.ExpressionElement);
        }
    }
}
