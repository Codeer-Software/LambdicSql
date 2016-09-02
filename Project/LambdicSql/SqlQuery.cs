using LambdicSql.Inside;
using LambdicSql.SqlBase;

namespace LambdicSql
{
    public class SqlQuery<TSelected> : ISqlQuery<TSelected>
    {
        ISqlExpressionBase _core;
        public TSelected Body => InvalitContext.Throw<TSelected>(nameof(Body));
        public SqlQuery(ISqlExpressionBase core) { _core = core; }
        public DbInfo DbInfo => _core.DbInfo;
        public string ToString(ISqlStringConverter decoder) => _core.ToString(decoder);
    }
}
