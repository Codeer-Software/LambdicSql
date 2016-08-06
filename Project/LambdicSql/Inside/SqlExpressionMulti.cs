using LambdicSql.QueryBase;

namespace LambdicSql.Inside
{
    class SqlExpressionMulti<TSelected> : ISqlExpression<TSelected>
    {
        ISqlExpression _before;
        ISqlExpression _after;
        public DbInfo DbInfo { get; }

        public SqlExpressionMulti(ISqlExpression before, ISqlExpression after)
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

        public string ToString(ISqlStringConverter converter)
            => _before.ToString(converter) + _after.ToString(converter);
    }
}
