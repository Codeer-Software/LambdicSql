using LambdicSql.ConverterServices;
using LambdicSql.MultiplatformCompatibe;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql
{
    /// <summary>
    /// SQL text and parameters.
    /// </summary>
    public class BuildedSql
    {
        Dictionary<string, DbParam> _dbParams;

        /// <summary>
        /// Sql text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Get parameters.
        /// </summary>
        /// <returns>Parameters.</returns>
        public Dictionary<string, DbParam> GetParams() => _dbParams.ToDictionary(e => e.Key, e => e.Value);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sqlText">Sql text.</param>
        /// <param name="dbParams">Parameters.</param>
        internal BuildedSql(string sqlText, Dictionary<string, IDbParam> dbParams)
        {
            Text = sqlText;
            _dbParams = dbParams.Adapt();
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
    }

    /// <summary>
    /// SQL text and parameters.
    /// </summary>
    /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
    public class BuildedSql<TSelected> : BuildedSql
    {
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
