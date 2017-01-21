using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql
{
    /// <summary>
    /// SQL text and parameters.
    /// </summary>
    public class BuildedSql
    {
        Dictionary<string, IDbParam> _dbParams;

        /// <summary>
        /// Sql text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Get parameters.
        /// </summary>
        /// <typeparam name="T">Converted type.</typeparam>
        /// <param name="converter">Converter.</param>
        /// <returns>Parameters.</returns>
        public Dictionary<string, T> GetParams<T>(Func<IDbParam, T> converter) => _dbParams.ToDictionary(e => e.Key, e => converter(e.Value));

        /// <summary>
        /// Get parameters.
        /// </summary>
        /// <returns>Parameters.</returns>
        public Dictionary<string, object> GetParamValues() => _dbParams.ToDictionary(e => e.Key, e => e.Value.Value);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sqlText">Sql text.</param>
        /// <param name="dbParams">Parameters.</param>
        internal BuildedSql(string sqlText, Dictionary<string, IDbParam> dbParams)
        {
            Text = sqlText;
            _dbParams = dbParams;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="src">Source.</param>
        internal BuildedSql(BuildedSql src)
        {
            Text = src.Text;
            _dbParams = src._dbParams;
        }

        /// <summary>
        /// 
        /// </summary>
        public BuildedSql() { }
    }

    /// <summary>
    /// SQL text and parameters.
    /// </summary>
    /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
    public class BuildedSql<TSelected> : BuildedSql
    {
        /// <summary>
        /// 
        /// </summary>
        public BuildedSql() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sqlText">Sql text.</param>
        /// <param name="dbParams">Parameters.</param>
        internal BuildedSql(string sqlText, Dictionary<string, IDbParam> dbParams)
            : base(sqlText, dbParams) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">Source.</param>
        internal BuildedSql(BuildedSql src) : base(src) { }
    }
}
