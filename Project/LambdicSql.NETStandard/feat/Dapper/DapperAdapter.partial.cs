using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LambdicSql.feat.Dapper
{
    public static partial class DapperAdapter
    {
        const string AssemblyInitializeComment = "using System.Reflection;\r\nDapperAdapter.Assembly = typeof(Dapper.SqlMapper).GetTypeInfo().Assembly;";

        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// For details, refer to the document of Dapper.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="cnn">Connection.</param>
        /// <param name="sql">Sql.</param>
        /// <param name="transaction">Transactions to be executed at the data source.</param>
        /// <param name="commandTimeout">Command timeout.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        ///     queried then the data from the first column in assumed, otherwise an instance
        ///     is created per row, and a direct column-name===member-name mapping is assumed
        ///     (case insensitive).
        /// </returns>
        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, Sql<T> sql, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
            => QueryAsync<T>(cnn, (Sql)sql, transaction, commandTimeout, commandType);

        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// For details, refer to the document of Dapper.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="cnn">Connection.</param>
        /// <param name="sql">Sql.</param>
        /// <param name="transaction">Transactions to be executed at the data source.</param>
        /// <param name="commandTimeout">Command timeout.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        ///     queried then the data from the first column in assumed, otherwise an instance
        ///     is created per row, and a direct column-name===member-name mapping is assumed
        ///     (case insensitive).
        /// </returns>
        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, BuildedSql<T> sql, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
            => QueryAsync<T>(cnn, (BuildedSql)sql, transaction, commandTimeout, commandType);

        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// For details, refer to the document of Dapper.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="cnn">Connection.</param>
        /// <param name="sql">Sql.</param>
        /// <param name="transaction">Transactions to be executed at the data source.</param>
        /// <param name="commandTimeout">Command timeout.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        ///     queried then the data from the first column in assumed, otherwise an instance
        ///     is created per row, and a direct column-name===member-name mapping is assumed
        ///     (case insensitive).
        /// </returns>
        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, Sql sql, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
             => QueryAsync<T>(cnn, sql.Build(cnn.GetType()), transaction, commandTimeout, commandType);

        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// For details, refer to the document of Dapper.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="cnn">Connection.</param>
        /// <param name="sql">Sql.</param>
        /// <param name="transaction">Transactions to be executed at the data source.</param>
        /// <param name="commandTimeout">Command timeout.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        ///     queried then the data from the first column in assumed, otherwise an instance
        ///     is created per row, and a direct column-name===member-name mapping is assumed
        ///     (case insensitive).
        /// </returns>
        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, BuildedSql sql, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            //for testing.
            if (DapperAdapterTestPlugin.Query != null) return Task.Factory.StartNew(()=> (IEnumerable<T>)new T[DapperAdapterTestPlugin.Query(cnn, sql)]);

            //debug.
            Debug(sql);

            try
            {
                var ret = DapperWrapperAsync<T>.Query(cnn, sql.Text, CreateDynamicParam(sql.GetParams()), transaction, commandTimeout, commandType);
                ResultLog?.Invoke(ret);
                return ret;
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
        /// <param name="sql">Sql.</param>
        /// <param name="transaction">Transactions to be executed at the data source.</param>
        /// <param name="commandTimeout">Command timeout.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns>Number of rows affected.</returns>
        public static Task<int> ExecuteAsync(this IDbConnection cnn, Sql sql, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
            => ExecuteAsync(cnn, sql.Build(cnn.GetType()), transaction, commandTimeout, commandType);

        /// <summary>
        /// Execute parameterized SQL.
        /// For details, refer to the document of Dapper.
        /// </summary>
        /// <param name="cnn">Connection.</param>
        /// <param name="sql">Sql.</param>
        /// <param name="transaction">Transactions to be executed at the data source.</param>
        /// <param name="commandTimeout">Command timeout.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns>Number of rows affected.</returns>
        public static Task<int> ExecuteAsync(this IDbConnection cnn, BuildedSql sql, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            //for testing.
            if (DapperAdapterTestPlugin.Execute != null) return Task.Factory.StartNew(()=> DapperAdapterTestPlugin.Execute(cnn, sql));

            //debug.
            Debug(sql);

            try
            {
                var ret = DapperWrapperAsync.Execute(cnn, sql.Text, CreateDynamicParam(sql.GetParams()), transaction, commandTimeout, commandType);
                ResultLog?.Invoke(ret);
                return ret;
            }
            catch (Exception e)
            {
                throw GetCoreException(e);
            }
        }

        static class DapperWrapperAsync
        {
            internal delegate Task<int> ExecuteDelegate(IDbConnection cnn, string sql, object param, IDbTransaction transaction, int? commandTimeout, CommandType? commandType);
            internal static ExecuteDelegate Execute;

            static DapperWrapperAsync()
            {
                var asm = DapperAdapter.Assembly;

                var sqlMapper = asm.GetType("Dapper.SqlMapper");

                var cnn = Expression.Parameter(typeof(IDbConnection), "cnn");
                var sql = Expression.Parameter(typeof(string), "sql");
                var param = Expression.Parameter(typeof(object), "param");

                var transaction = Expression.Parameter(typeof(IDbTransaction), "transaction");
                var commandTimeout = Expression.Parameter(typeof(int?), "commandTimeout");
                var commandType = Expression.Parameter(typeof(CommandType?), "commandType");

                var executeArgs = new[] { cnn, sql, param, transaction, commandTimeout, commandType };
                Execute = Expression.Lambda<ExecuteDelegate>(Expression.Call(sqlMapper, "ExecuteAsync", new Type[0], executeArgs), executeArgs).Compile();
            }
        }

        static class DapperWrapperAsync<T>
        {
            internal delegate Task<IEnumerable<T>> QueryDelegate(IDbConnection cnn, string sql, object param, IDbTransaction transaction, int? commandTimeout, CommandType? commandType);
            internal static QueryDelegate Query;

            static DapperWrapperAsync()
            {
                var asm = DapperAdapter.Assembly;

                var sqlMapper = asm.GetType("Dapper.SqlMapper");

                var cnn = Expression.Parameter(typeof(IDbConnection), "cnn");
                var sql = Expression.Parameter(typeof(string), "sql");
                var param = Expression.Parameter(typeof(object), "param");

                var transaction = Expression.Parameter(typeof(IDbTransaction), "transaction");
                var commandTimeout = Expression.Parameter(typeof(int?), "commandTimeout");
                var commandType = Expression.Parameter(typeof(CommandType?), "commandType");

                var queryArgs = new[] { cnn, sql, param, transaction, commandTimeout, commandType };
                Query = Expression.Lambda<QueryDelegate>(Expression.Call(sqlMapper, "QueryAsync", new[] { typeof(T) }, queryArgs), queryArgs).Compile(); ;
            }
        }
    }
}
