using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace LambdicSql.feat.EntityFramework
{
    static class EFWrapper
    {
        internal static object _sync = new object();

        internal delegate int ExecuteSqlCommandDelegate(object dbContext, string sql, object[] parameters);
        static ExecuteSqlCommandDelegate ExecuteSqlCommand;

        internal delegate DbConnection GetConnectionDelegate(object dbContext);
        static GetConnectionDelegate GetConnection;

        internal static ExecuteSqlCommandDelegate GetExecuteSqlCommand(object obj)
        {
            lock (_sync)
            {
                if (ExecuteSqlCommand != null)
                {
                    return ExecuteSqlCommand;
                }

                var sql = Expression.Parameter(typeof(string), "sql");
                var paramsArray = Expression.Parameter(typeof(object[]), "paramsArray");
                var dbContext = Expression.Parameter(typeof(object), "dbContext");
                var dbContextType = obj.GetType();
                var database = Expression.PropertyOrField(Expression.Convert(dbContext, dbContextType), "Database");

                ExecuteSqlCommand = Expression.Lambda<ExecuteSqlCommandDelegate>(
                    Expression.Call(database, "ExecuteSqlCommand", new Type[0], new[] { sql, paramsArray }),
                    new[] { dbContext, sql, paramsArray }).Compile();

                return ExecuteSqlCommand;
            }
        }

        internal static GetConnectionDelegate GetGetConnection(object obj)
        {
            lock (_sync)
            {
                if (GetConnection != null)
                {
                    return GetConnection;
                }

                var sql = Expression.Parameter(typeof(string), "sql");
                var dbContext = Expression.Parameter(typeof(object), "dbContext");
                var dbContextType = obj.GetType();
                var database = Expression.PropertyOrField(Expression.Convert(dbContext, dbContextType), "Database");
                var connection = Expression.PropertyOrField(database, "Connection");

                GetConnection = Expression.Lambda<GetConnectionDelegate>(connection, new[] { dbContext }).Compile();

                return GetConnection;
            }
        }
    }

    static class EFWrapper<T>
    {
        internal static object _sync = new object();

        internal delegate IEnumerable<T> SqlQueryDelegate(object dbContext, string sql, object[] parameters);
        static SqlQueryDelegate SqlQuery;

        internal static SqlQueryDelegate GetSqlQuery(object obj)
        {
            lock (_sync)
            {
                if (SqlQuery != null)
                {
                    return SqlQuery;
                }
                
                var dbContextType = obj.GetType();
                var dbContext = Expression.Parameter(typeof(object), "dbContext");
                var sql = Expression.Parameter(typeof(string), "sql");
                var paramsArray = Expression.Parameter(typeof(object[]), "paramsArray");
                var database = Expression.PropertyOrField(Expression.Convert(dbContext, dbContextType), "Database");

                SqlQuery = Expression.Lambda<SqlQueryDelegate>(
                    Expression.Call(database, "SqlQuery", new[] { typeof(T) }, new[] { sql, paramsArray }),
                    new[] { dbContext, sql, paramsArray }).Compile();

                return SqlQuery;
            }
        }
    }
}
