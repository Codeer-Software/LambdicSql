using System;

namespace LambdicSql.QueryBase
{
    public interface ISqlExpression
    {
        DbInfo DbInfo { get; }
        string ToString(ISqlStringConverter decoder);
    }
    
    public interface ISqlExpression<out TSelected> : ISqlExpression { }

    public abstract class SqlExpression<TSelected> : ISqlExpression<TSelected>
    {
        public abstract DbInfo DbInfo { get; protected set; }
        public abstract string ToString(ISqlStringConverter decoder);
        public static implicit operator TSelected(SqlExpression<TSelected> src) => default(TSelected);
    }
}
