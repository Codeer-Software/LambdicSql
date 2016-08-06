using LambdicSql.QueryBase;

namespace LambdicSql.Inside
{
    class SqlExpressionMulti<TSelected> : ISqlExpression<TSelected>
    {
        ISqlExpression _before;
        ISqlExpression _after;
        public DbInfo DbInfo => _before.DbInfo;

        public SqlExpressionMulti(ISqlExpression before, ISqlExpression after)
        {
            _before = before;
            _after = after;
        }

        public string ToString(ISqlStringConverter converter)
            => _before.ToString(converter) + _after.ToString(converter);
    }
}
