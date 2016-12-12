using LambdicSql.SqlBase;
using System;

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

        public override IText ToString(ISqlStringConverter converter)
            => new VerticalText(_before.ToString(converter), _after.ToString(converter));
    }
}
