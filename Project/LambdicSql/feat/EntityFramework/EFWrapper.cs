using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.feat.EntityFramework
{
    //EntityFramework.dll
        //  public int ExecuteSqlCommand(string sql, params object[] parameters);
        //   public DbRawSqlQuery<TElement> SqlQuery<TElement>(string sql, params object[] parameters);
    
    static class EFWrapper
    {
        internal delegate int ExecuteSqlCommandDelegate(object dbContext, string sql, object[] parameters);
        internal static ExecuteSqlCommandDelegate ExecuteSqlCommand;

        internal delegate DbConnection GetConnectionDelegate(object dbContext);
        internal static GetConnectionDelegate GetConnection;

        static EFWrapper()
        {
            Assembly asm = null;
            try
            {
                asm = Assembly.LoadFrom("EntityFramework.dll");
            }
            catch { throw new PackageIsNotInstalledException("EntityFramework is not installed. Please install EntityFramework of your faverit version."); }
            if (asm == null) throw new PackageIsNotInstalledException("EntityFramework is not installed. Please install EntityFramework of your faverit version.");


            var sql = Expression.Parameter(typeof(string), "sql");
            var paramsArray = Expression.Parameter(typeof(object[]), "paramsArray");
            var dbContext = Expression.Parameter(typeof(object), "dbContext");
            var dbContextType = asm.GetType("System.Data.Entity.DbContext");
            var database = Expression.PropertyOrField(Expression.Convert(dbContext, dbContextType), "Database");
            var connection = Expression.PropertyOrField(database, "Connection");

            GetConnection = Expression.Lambda<GetConnectionDelegate>(connection, new[] { dbContext }).Compile();

            ExecuteSqlCommand = Expression.Lambda<ExecuteSqlCommandDelegate>(
                Expression.Call(database, "ExecuteSqlCommand", new Type[0], new[] { sql, paramsArray }),
                new[] { dbContext, sql, paramsArray }).Compile();
        }
    }

    static class EFWrapper<T>
    {
        internal delegate IEnumerable<T> SqlQueryDelegate(object dbContext, string sql, object[] parameters);
        internal static SqlQueryDelegate SqlQuery;

        static EFWrapper()
        {
            Assembly asm = null;
            try
            {
                asm = Assembly.LoadFrom("EntityFramework.dll");
            }
            catch { throw new PackageIsNotInstalledException("EntityFramework is not installed. Please install EntityFramework of your faverit version."); }
            if (asm == null) throw new PackageIsNotInstalledException("EntityFramework is not installed. Please install EntityFramework of your faverit version.");

            var dbContextType = asm.GetType("System.Data.Entity.DbContext");

            var dbContext = Expression.Parameter(typeof(object), "dbContext");
            var sql = Expression.Parameter(typeof(string), "sql");
            var paramsArray = Expression.Parameter(typeof(object[]), "paramsArray");

            var database = Expression.PropertyOrField(Expression.Convert(dbContext, dbContextType), "Database");


            SqlQuery = Expression.Lambda<SqlQueryDelegate>(
                Expression.Call(database, "SqlQuery", new[] { typeof(T) }, new[] { sql, paramsArray }), 
                new[] { dbContext, sql, paramsArray }).Compile();
        }
    }
}
