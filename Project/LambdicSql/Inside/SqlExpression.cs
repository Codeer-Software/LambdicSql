using LambdicSql.QueryBase;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    class SqlExpression<TSelected> : ISqlExpression<TSelected>
    {
        Expression _core;
        public DbInfo DbInfo { get; private set; }

        public SqlExpression(DbInfo dbInfo, Expression core)
        {
            DbInfo = dbInfo;
            _core = core;
        }
        
        public string ToString(ISqlStringConverter converter)
            => (_core == null) ? string.Empty : converter.ToString(_core);
    }
}
