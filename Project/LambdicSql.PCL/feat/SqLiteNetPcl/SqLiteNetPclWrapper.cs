using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.feat.SqLiteNetPcl
{
    static class SqLiteNetPclWrapper
    {
        internal static object _sync = new object();
        internal delegate int ExecuteDelegate(object cnn, string sql, object[] arguments);
        internal static ExecuteDelegate Execute;

        internal static ExecuteDelegate GetExecute(object obj)
        {
            lock (_sync)
            {
                if (Execute != null) return Execute;

                var sqlLiteConnectionType = obj.GetType();

                var cnn = Expression.Parameter(typeof(object), "cnn");
                var sql = Expression.Parameter(typeof(string), "sql");
                var arguments = Expression.Parameter(typeof(object[]), "arguments");

                Execute = Expression.Lambda<ExecuteDelegate>(
                    Expression.Call(Expression.Convert(cnn, sqlLiteConnectionType), "Execute", null, new[] { sql, arguments }),
                    new[] { cnn, sql, arguments }
                    ).Compile();

                return Execute;
            }
        }
    }

    static class SqLiteNetPclWrapper<T>
    {
        internal static object _sync = new object();
        internal delegate List<T> QueryDelegate(object cnn, string query, params object[] args);
        internal static QueryDelegate Query;

        internal static QueryDelegate GetQuery(object obj)
        {
            lock (_sync)
            {
                if (Query != null) return Query;

                var sqlLiteConnectionType = obj.GetType();

                var cnn = Expression.Parameter(typeof(object), "cnn");
                var sql = Expression.Parameter(typeof(string), "sql");
                var arguments = Expression.Parameter(typeof(object[]), "arguments");

                Query = Expression.Lambda<QueryDelegate>(
                    Expression.Call(Expression.Convert(cnn, sqlLiteConnectionType), "Query", new[] { typeof(T) }, new[] { sql, arguments }),
                    new[] { cnn, sql, arguments }
                    ).Compile();

                return Query;
            }
        }
    }
}
