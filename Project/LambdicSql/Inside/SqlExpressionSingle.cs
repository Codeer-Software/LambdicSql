using LambdicSql.SqlBase;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    class SqlExpressionSingle<TSelected> : SqlExpression<TSelected>
    {
        Expression _core;
        public override DbInfo DbInfo { get; protected set; }
        public override object DbContext { get; protected set; }

        public SqlExpressionSingle(DbInfo dbInfo, Expression core, object dbContext = null)
        {
            DbInfo = dbInfo;
            _core = core;
            DbContext = dbContext;
        }

        public override string ToString(ISqlStringConverter converter)
            => (_core == null) ? string.Empty : converter.ToString(_core);
    }
}
