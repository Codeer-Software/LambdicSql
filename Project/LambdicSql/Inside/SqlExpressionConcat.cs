using LambdicSql.SqlBase;

namespace LambdicSql.Inside
{
    class SqlExpressionConcat<TSelected> : SqlExpression<TSelected>
    {
        ISqlExpression _before;
        ISqlExpression _after;
        public override DbInfo DbInfo { get; protected set; }

        public SqlExpressionConcat(ISqlExpression before, ISqlExpression after)
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
        }

        public override string ToString(ISqlStringConverter converter)
            => _before.ToString(converter) + _after.ToString(converter);
    }
}
