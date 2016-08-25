using LambdicSql.SqlBase;

namespace LambdicSql.Inside
{
    class SqlExpressionCoupled<TSelected> : SqlExpression<TSelected>
    {
        ISqlExpression _before;
        ISqlExpression _after;
        public override DbInfo DbInfo { get; protected set; }
        public override object DbContext { get; protected set; }

        public SqlExpressionCoupled(ISqlExpression before, ISqlExpression after)
        {
            _before = before;
            _after = after;
            if (_before.DbInfo != null)
            {
                DbInfo = _before.DbInfo;
            }
            else if (_after.DbInfo != null)
            {
                DbInfo = _after.DbInfo;
            }

            if (_before.DbContext != null)
            {
                DbContext = _before.DbContext;
            }
            else if (_after.DbContext != null)
            {
                DbContext = _after.DbContext;
            }
        }

        public override string ToString(ISqlStringConverter converter)
            => _before.ToString(converter) + _after.ToString(converter);
    }
}
