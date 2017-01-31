using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.feat.EntityFramework
{
    /// <summary>
    /// Extensions for adapt Entity Framework.
    /// </summary>
    public static class EFAdapter
    {
        /// <summary>
        /// Debug Log.
        /// </summary>
        public static Action<string> Log { get; set; }

        /// <summary>
        /// Get entity.
        /// It can only be used within methods of the LambdicSql.Db class.
        /// </summary>
        /// <typeparam name="TEntity">Entity.</typeparam>
        /// <param name="queryable">Queryable.</param>
        /// <returns>Entity.</returns>
        [TConverter]
        public static TEntity T<TEntity>(this IQueryable<TEntity> queryable) { throw new InvalitContextException(nameof(T)); }

        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="dbContext">DbContext object.</param>
        /// <param name="sql">Sql.</param>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        ///     queried then the data from the first column in assumed, otherwise an instance
        ///     is created per row, and a direct column-name===member-name mapping is assumed
        ///     (case insensitive).
        /// </returns>
        public static IEnumerable<T> Query<T>(this IDisposable dbContext, Sql<T> sql)
            => Query<T>(dbContext, (Sql)sql);

        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="dbContext">DbContext object.</param>
        /// <param name="sql">Sql.</param>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        ///     queried then the data from the first column in assumed, otherwise an instance
        ///     is created per row, and a direct column-name===member-name mapping is assumed
        ///     (case insensitive).
        /// </returns>
        public static IEnumerable<T> Query<T>(this IDisposable dbContext, BuildedSql<T> sql)
            => Query<T>(dbContext, (BuildedSql)sql);

        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="dbContext">DbContext object.</param>
        /// <param name="sql">Sql.</param>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        ///     queried then the data from the first column in assumed, otherwise an instance
        ///     is created per row, and a direct column-name===member-name mapping is assumed
        ///     (case insensitive).
        /// </returns>
        public static IEnumerable<T> Query<T>(this IDisposable dbContext, Sql sql)
            => Query<T>(dbContext, sql.Build(dbContext.GetType()));

        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="dbContext">DbContext object.</param>
        /// <param name="sql">Sql.</param>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        ///     queried then the data from the first column in assumed, otherwise an instance
        ///     is created per row, and a direct column-name===member-name mapping is assumed
        ///     (case insensitive).
        /// </returns>
        public static IEnumerable<T> Query<T>(this IDisposable dbContext, BuildedSql sql)
        {
            var cnn = EFWrapper.GetGetConnection(dbContext)(dbContext);

            //debug.
            Debug(sql);

            object[] args;
            using (var com = cnn.CreateCommand())
            {
                args = sql.GetParams().Select(e => CreateParameter(com, e.Key, e.Value)).ToArray();

                try
                {
                    return EFWrapper<T>.GetSqlQuery(dbContext)(dbContext, sql.Text, args);
                }
                catch (Exception e)
                {
                    throw GetCoreException(e);
                }
            }
        }

        /// <summary>
        /// Execute parameterized SQL.
        /// </summary>
        /// <param name="dbContext">DbContext object.</param>
        /// <param name="sql">Sql.</param>
        /// <returns>Number of rows affected.</returns>
        public static int Execute(this IDisposable dbContext, Sql sql)
            => Execute(dbContext, sql.Build(dbContext.GetType()));

        /// <summary>
        /// Execute parameterized SQL.
        /// </summary>
        /// <param name="dbContext">DbContext object.</param>
        /// <param name="sql">Sql.</param>
        /// <returns>Number of rows affected.</returns>
        public static int Execute(this IDisposable dbContext, BuildedSql sql)
        {
            var cnn = EFWrapper.GetGetConnection(dbContext)(dbContext);

            //debug.
            Debug(sql);

            object[] args;
            using (var com = cnn.CreateCommand())
            {
                args = sql.GetParams().Select(e => CreateParameter(com, e.Key, e.Value)).ToArray();
            }

            try
            {
                return EFWrapper.GetExecuteSqlCommand(dbContext)(dbContext, sql.Text, args);
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

        static void Debug(BuildedSql info)
        {
            if (Log == null) return;
            Log(info.Text);
            foreach (var e in info.GetParams())
            {
                Log(e.Key + " = " + (e.Value.Value == null ? string.Empty : e.Value.Value.ToString()));
            }
            Log(string.Empty);
        }
    }

    class TConverterAttribute : MethodConverterAttribute
    {
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter)
            => converter.ConvertToCode(expression.Arguments[0]);
    }
}
