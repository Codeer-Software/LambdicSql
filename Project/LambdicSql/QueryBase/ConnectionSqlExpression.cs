using System;

namespace LambdicSql.QueryBase
{
    public class ConnectionSqlExpression<T> : IConnectionSqlExpression
    {
        public IQuery Query { get { throw new NotSupportedException("do not call cast except in expression."); } }
        public string ToString(ISqlStringConverter decoder)
        {
            throw new NotSupportedException("do not call cast except in expression.");
        }

        public static implicit operator T(ConnectionSqlExpression<T> src) => default(T);
    }
}
