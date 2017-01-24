using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace LambdicSql.feat.Dapper
{
    /// <summary>
    /// Extensions for adjust Dapper.
    /// In order to use these, it is necessary that arbitrary Dapper is installed. 
    /// LambdicSql has no dependency on Dapper from the project compositionally with Dapper. 
    /// That is to avoid inconsistencies between versions. Please install your favorite version of Dapper by yourself.
    /// </summary>
    public static class DapperAdapter
    {
        static object _sync = new object();
        static Assembly _assembly;

        /// <summary>
        /// Dapper's Assembly.
        /// </summary>
        public static Assembly Assembly
        {
            get
            {
                lock (_sync)
                {
                    if (_assembly == null)
                    {
                        throw new PackageIsNotInstalledException(
                        "Please set Dapper's Assembly.The following shows the sample.\r\n\r\n" +
                        "using LambdicSql.feat.Dapper;\r\n" +
                        "DapperAdapter.Assembly = typeof(Dapper.SqlMapper).Assembly;");
                    }
                    return _assembly;
                }
            }
            set
            {
                lock (_sync)
                {
                    _assembly = value;
                }
            }
        }
        
        /// <summary>
        /// Debug Log.
        /// </summary>
        public static Action<string> Log { get; set; }

        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// For details, refer to the document of Dapper.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="cnn">Connection.</param>
        /// <param name="query">Query expression.</param>
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
        public static IEnumerable<T> Query<T>(this IDbConnection cnn, Sql<T> query, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
            => Query<T>(cnn, (Sql)query, transaction, buffered, commandTimeout, commandType);
 
        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// For details, refer to the document of Dapper.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="cnn">Connection.</param>
        /// <param name="query">Query expression.</param>
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
        public static IEnumerable<T> Query<T>(this IDbConnection cnn, Sql query, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            var info = query.Build(cnn.GetType());

            //for testing.
            if (DapperApaptExtensionsForTest.Query != null) return new T[DapperApaptExtensionsForTest.Query(cnn, info)];

            //debug.
            Debug(info);

            try
            {
                return DapperWrapper<T>.Query(cnn, info.Text, CreateDynamicParam(info.GetParams()), transaction, buffered, commandTimeout, commandType);
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
        public static int Execute(this IDbConnection cnn, Sql exp, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            var info = exp.Build(cnn.GetType());

            //for testing.
            if (DapperApaptExtensionsForTest.Execute != null) return DapperApaptExtensionsForTest.Execute(cnn, info);

            //debug.
            Debug(info);

            try
            {
                return DapperWrapper.Execute(cnn, info.Text, CreateDynamicParam(info.GetParams()), transaction, commandTimeout, commandType);
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
        public delegate int QueryDelegate(IDbConnection cnn, BuildedSql info);

        /// <summary>
        /// For testing.
        /// </summary>
        /// <param name="cnn">For testing.</param>
        /// <param name="info">For testing.</param>
        /// <returns>For testing.</returns>
        public delegate int ExecuteDelegate(IDbConnection cnn, BuildedSql info);

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
