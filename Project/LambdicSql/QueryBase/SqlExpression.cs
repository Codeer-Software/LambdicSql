using LambdicSql.Inside;
using System.Linq.Expressions;

namespace LambdicSql.QueryBase
{
    class SqlExpression<TSelected> : ISqlExpression<TSelected>
    {
        ISqlExpression _before;
        Expression _core;
        public Expression Expression => _core;
        public DbInfo DbInfo { get; private set; }
        public SqlExpression(DbInfo dbInfo)
        {
            DbInfo = dbInfo;
        }

        public SqlExpression(DbInfo dbInfo, Expression core)
        {
            DbInfo = dbInfo;
            _core = core;
        }

        public SqlExpression(ISqlExpression before, Expression core)
        {
            DbInfo = before.DbInfo;
            _before = before;
            _core = core;
        }

        public string ToString(ISqlStringConverter src)
        {
            if (_core == null)
            {
                return string.Empty;
            }
            var decoder = src as SqlStringConverter;
            var text = decoder.ToString(_core);
            if (_before != null)
            {
                text = _before.ToString(decoder) + text;
            }
            return text;
        }
    }
}
