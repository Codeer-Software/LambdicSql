using LambdicSql.SqlBase;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    class SqlExpressionSingle<TSelected> : SqlExpression<TSelected>
    {
        Expression _core;
        public override DbInfo DbInfo { get; protected set; }

        public SqlExpressionSingle(DbInfo dbInfo, Expression core)
        {
            DbInfo = dbInfo;
            _core = core;
        }

        public override SqlText Convert(ISqlStringConverter converter)
            => _core == null ? (SqlText)string.Empty : converter.Convert(_core);
    }
}
