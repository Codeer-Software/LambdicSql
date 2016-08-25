namespace LambdicSql.SqlBase
{
    public abstract class SqlExpression<T> : ISqlExpression<T>
    {
        public abstract object DbContext { get; protected set; }
        public abstract DbInfo DbInfo { get; protected set; }
        public abstract string ToString(ISqlStringConverter decoder);
        public static implicit operator T(SqlExpression<T> src) => default(T);
    }
}
