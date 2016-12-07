using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace LambdicSql.feat.EntityFramework
{
    /// <summary>
    /// Extensions for adjust Entity Framework.
    /// </summary>
    public static class EFApaptExtensions
    {
        /// <summary>
        /// Execute query.
        /// </summary>
        /// <typeparam name="T">Type of result entity.</typeparam>
        /// <param name="query">Query.</param>
        /// <param name="dbContext">DbContext object.</param>
        /// <returns>Query result.</returns>
        public static IEnumerable<T> SqlQuery<T>(this ISqlExpressionBase<IClauseChain<T>> query, object dbContext)
            => SqlQuery<T>((ISqlExpressionBase)query, dbContext);

        /// <summary>
        /// Execute query.
        /// </summary>
        /// <typeparam name="T">Type of result entity.</typeparam>
        /// <param name="query">Query.</param>
        /// <param name="dbContext">DbContext object.</param>
        /// <returns>Query result.</returns>
        public static IEnumerable<T> SqlQuery<T>(this ISqlExpressionBase query, object dbContext)
        {
            var cnn = EFWrapper.GetConnection(dbContext);
            var info = query.ToSqlInfo(cnn.GetType());
            
            object[] args;
            using (var com = cnn.CreateCommand())
            {
                args = info.DbParams.Select(e => CreateParameter(com, e.Key, e.Value)).ToArray();
            }

            try
            {
                return EFWrapper<T>.SqlQuery(dbContext, info.SqlText, args);
            }
            catch (Exception e)
            {
                throw GetCoreException(e);
            }
        }

        /// <summary>
        /// Execute query.
        /// </summary>
        /// <param name="query">Query.</param>
        /// <param name="dbContext">DbContext object.</param>
        /// <returns>Number of rows affected.</returns>
        public static int ExecuteSqlCommand(this ISqlExpressionBase query, object dbContext)
        {
            var cnn = EFWrapper.GetConnection(dbContext);
            var info = query.ToSqlInfo(cnn.GetType());
            
            Debug.Print(info.SqlText);

            object[] args;
            using (var com = cnn.CreateCommand())
            {
                args = info.DbParams.Select(e => CreateParameter(com, e.Key, e.Value)).ToArray();
            }

            try
            {
                return EFWrapper.ExecuteSqlCommand(dbContext, info.SqlText, args);
            }
            catch (Exception e)
            {
                throw GetCoreException(e);
            }
        }

        static Exception GetCoreException(Exception e)
        {
            while (true)
            {
                if (e.InnerException == null) return e;
                e = e.InnerException;
            }
        }

        static IDbDataParameter CreateParameter(IDbCommand com, string name, DbParam src)
        {
            var dst = com.CreateParameter();
            dst.ParameterName = name;
            dst.Value = src.Value;

            if (src.DbType != null) dst.DbType = src.DbType.Value;
            if (src.Direction != null) dst.Direction = src.Direction.Value;
            if (src.SourceColumn != null) dst.SourceColumn = src.SourceColumn;
            if (src.SourceVersion != null) dst.SourceVersion = src.SourceVersion.Value;
            if (src.Precision != null) dst.Precision = src.Precision.Value;
            if (src.Scale != null) dst.Scale = src.Scale.Value;
            if (src.Size != null) dst.Size = src.Size.Value;

            return dst;
        }
    }
}
