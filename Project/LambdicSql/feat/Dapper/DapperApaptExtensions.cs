using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Data;

namespace LambdicSql.feat.Dapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class DapperApaptExtensions
    {
        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// For details, refer to the document of Dapper.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="cnn">Connection.</param>
        /// <param name="exp">Query expression.</param>
        /// <param name="transaction">Transactions to be executed at the data source.</param>
        /// <param name="buffered">Is buffered,</param>
        /// <param name="commandTimeout">Command timeout.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        ///     queried then the data from the first column in assumed, otherwise an instance
        ///     is created per row, and a direct column-name===member-name mapping is assumed
        ///     (case insensitive).
        /// </returns>
        public static IEnumerable<T> Query<T>(this IDbConnection cnn, ISqlExpressionBase<IClauseChain<T>> exp, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
            => Query<T>(cnn, (ISqlExpressionBase)exp, transaction, buffered, commandTimeout, commandType);

        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// For details, refer to the document of Dapper.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="cnn">Connection.</param>
        /// <param name="exp">Query expression.</param>
        /// <param name="transaction">Transactions to be executed at the data source.</param>
        /// <param name="buffered">Is buffered,</param>
        /// <param name="commandTimeout">Command timeout.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        ///     queried then the data from the first column in assumed, otherwise an instance
        ///     is created per row, and a direct column-name===member-name mapping is assumed
        ///     (case insensitive).
        /// </returns>
        public static IEnumerable<T> Query<T>(this IDbConnection cnn, ISqlExpressionBase exp, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            var info = exp.ToSqlInfo(cnn.GetType());

            //for testing.
            if (DapperApaptExtensionsForTest.Query != null) return new T[DapperApaptExtensionsForTest.Query(cnn, info)];

            try
            {
                return DapperWrapper<T>.Query(cnn, info.SqlText, CreateDynamicParam(info.DbParams), transaction, buffered, commandTimeout, commandType);
            }
            catch (Exception e)
            {
                throw GetCoreException(e);
            }
        }

        /// <summary>
        /// Execute parameterized SQL.
        /// For details, refer to the document of Dapper.
        /// </summary>
        /// <param name="cnn">Connection.</param>
        /// <param name="exp">Query expression.</param>
        /// <param name="transaction">Transactions to be executed at the data source.</param>
        /// <param name="commandTimeout">Command timeout.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns>Number of rows affected.</returns>
        public static int Execute(this IDbConnection cnn, ISqlExpressionBase exp, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            var info = exp.ToSqlInfo(cnn.GetType());

            //for testing.
            if (DapperApaptExtensionsForTest.Execute != null) return DapperApaptExtensionsForTest.Execute(cnn, info);

            try
            {
                return DapperWrapper.Execute(cnn, info.SqlText, CreateDynamicParam(info.DbParams), transaction, commandTimeout, commandType);
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

        private static object CreateDynamicParam(Dictionary<string, DbParam> dbParams)
        {
            var target = DynamicParametersWrapper.Create();
            foreach (var e in dbParams)
            {
                DynamicParametersWrapper.Add(target, e.Key, e.Value.Value, e.Value.DbType, e.Value.Direction, e.Value.Size, e.Value.Precision, e.Value.Scale);
            }
            return target;
        }
    }

    /// <summary>
    /// For testing.
    /// </summary>
    public static class DapperApaptExtensionsForTest
    {
        /// <summary>
        /// For testing.
        /// </summary>
        /// <param name="cnn">For testing.</param>
        /// <param name="info">For testing.</param>
        /// <returns>For testing.</returns>
        public delegate int QueryDelegate(IDbConnection cnn, SqlInfo info);

        /// <summary>
        /// For testing.
        /// </summary>
        /// <param name="cnn">For testing.</param>
        /// <param name="info">For testing.</param>
        /// <returns>For testing.</returns>
        public delegate int ExecuteDelegate(IDbConnection cnn, SqlInfo info);

        /// <summary>
        /// For testing.
        /// </summary>
        public static QueryDelegate Query { get; set; }

        /// <summary>
        /// For testing.
        /// </summary>
        public static ExecuteDelegate Execute { get; set; }
    }
}
