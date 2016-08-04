using LambdicSql.ORM;
using LambdicSql.QueryBase;
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
            (this ISqlExpression<ISqlKeyWord<TSelected>> exp, IDbConnection connection)
        where TSelected : class
            => new SqlExecutor<TSelected>(connection, exp.ToSqlInfo(connection.GetType()));
    }
}
