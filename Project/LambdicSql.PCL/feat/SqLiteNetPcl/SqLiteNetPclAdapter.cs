﻿using System;
using System.Collections.Generic;

namespace LambdicSql.feat.SQLiteNetPcl
{
    /// <summary>
    /// Extensions for adjust SQLiteConnection.
    /// </summary>
    public static class SQLiteNetPclAdapter
    {
        /// <summary>
        /// Debug Log.
        /// </summary>
        public static Action<string> Log { get; set; }

        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="cnn">Connection.</param>
        /// <param name="sql">Sql.</param>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        ///     queried then the data from the first column in assumed, otherwise an instance
        ///     is created per row, and a direct column-name===member-name mapping is assumed
        ///     (case insensitive).
        /// </returns>
        public static List<T> Query<T>(this IDisposable cnn, Sql<T> sql)
            => Query<T>(cnn, (Sql)sql);
        
        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="cnn">Connection.</param>
        /// <param name="sql">Sql.</param>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        ///     queried then the data from the first column in assumed, otherwise an instance
        ///     is created per row, and a direct column-name===member-name mapping is assumed
        ///     (case insensitive).
        /// </returns>
        public static List<T> Query<T>(this IDisposable cnn, BuildedSql<T> sql)
            => Query<T>(cnn, (BuildedSql)sql);

        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="cnn">Connection.</param>
        /// <param name="sql">Sql.</param>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        ///     queried then the data from the first column in assumed, otherwise an instance
        ///     is created per row, and a direct column-name===member-name mapping is assumed
        ///     (case insensitive).
        /// </returns>
        public static List<T> Query<T>(this IDisposable cnn, Sql sql)
            => Query<T>(cnn, sql.Build(cnn.GetType()));

        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="cnn">Connection.</param>
        /// <param name="sql">Sql.</param>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        ///     queried then the data from the first column in assumed, otherwise an instance
        ///     is created per row, and a direct column-name===member-name mapping is assumed
        ///     (case insensitive).
        /// </returns>
        public static List<T> Query<T>(this IDisposable cnn, BuildedSql sql)
        {
            //debug.
            Debug(sql);

            try
            {
                return SQLiteNetPclWrapper<T>.GetQuery(cnn)(cnn, sql.Text, sql.GetParamValues());
            }
            catch (Exception e)
            {
                throw GetCoreException(e);
            }
        }

        /// <summary>
        /// Execute parameterized SQL.
        /// </summary>
        /// <param name="cnn">Connection.</param>
        /// <param name="sql">Sql.</param>
        /// <returns>Number of rows affected.</returns>
        public static int Execute(this IDisposable cnn, Sql sql)
            => Execute(cnn, sql.Build(cnn.GetType()));

        /// <summary>
        /// Execute parameterized SQL.
        /// </summary>
        /// <param name="cnn">Connection.</param>
        /// <param name="sql">Sql.</param>
        /// <returns>Number of rows affected.</returns>
        public static int Execute(this IDisposable cnn, BuildedSql sql)
        {
            //debug.
            Debug(sql);

            try
            {
                return SQLiteNetPclWrapper.GetExecute(cnn)(cnn, sql.Text, sql.GetParamValues());
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

        static void Debug(BuildedSql info)
        {
            if (Log == null) return;
            Log(info.Text);
            Log(string.Join(",", info.GetParamValues()));
            Log(string.Empty);
        }
    }
}
