using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;

namespace LambdicSql.Inside
{
    class SqlExpressionCoupled<TSelected> : SqlExpression<TSelected>
    {
        ISqlExpressionBase _before;
        ISqlExpressionBase _after;
        public override DbInfo DbInfo { get; protected set; }

        public SqlExpressionCoupled(ISqlExpressionBase before, ISqlExpressionBase after)
        {
            _before = before;
            _after = after;
            if (_before.DbInfo != null) DbInfo = _before.DbInfo;
            else if (_after.DbInfo != null) DbInfo = _after.DbInfo;
        }

        public override SqlText Convert(ISqlStringConverter converter)
            => new VText(_before.Convert(converter), _after.Convert(converter));
    }
}
