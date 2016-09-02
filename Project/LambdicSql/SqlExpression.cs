using LambdicSql.Inside;
using LambdicSql.SqlBase;

namespace LambdicSql
{
    public abstract class SqlExpression<T> : ISqlExpression<T>
    {
        public abstract DbInfo DbInfo { get; protected set; }
        public T Body => InvalitContext.Throw<T>(nameof(Body));
        public abstract string ToString(ISqlStringConverter decoder);
        public static implicit operator T(SqlExpression<T> src) => default(T);
    }
}
