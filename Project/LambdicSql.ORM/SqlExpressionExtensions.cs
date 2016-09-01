using LambdicSql.ORM;
using LambdicSql.SqlBase;
using System;
using System.Data;

namespace LambdicSql
{
    public static class SqlOption
    {
        public static Action<string> Log { get; set; }
    }

    public static class SqlExpressionExtensions
    {
        public static ISqlExecutor<TSelected> ToExecutor<TSelected>
            (this ISqlExpressionBase<IQuery<TSelected>> exp, IDbConnection connection)
        where TSelected : class
            => new SqlExecutor<TSelected>(connection, exp.ToSqlInfo(connection.GetType()));
    }
}
