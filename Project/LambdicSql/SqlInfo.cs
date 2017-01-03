using LambdicSql.ConverterService;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql
{
    /// <summary>
    /// SQL information.
    /// </summary>
    public class SqlInfo
    {
        Dictionary<string, DbParam> _dbParams;

        /// <summary>
        /// Sql text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Parameters.
        /// </summary>
        public Dictionary<string, DbParam> Params => _dbParams.ToDictionary(e => e.Key, e => e.Value);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sqlText">Sql text.</param>
        /// <param name="dbParams">Parameters.</param>
        public SqlInfo(string sqlText, Dictionary<string, DbParam> dbParams)
        {
            Text = sqlText;
            _dbParams = dbParams;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="src">Source.</param>
        public SqlInfo(SqlInfo src)
        {
            Text = src.Text;
            _dbParams = src._dbParams;
        }
    }

    /// <summary>
    /// SQL information.
    /// </summary>
    /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
    public class SqlInfo<TSelected> : SqlInfo
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sqlText">Sql text.</param>
        /// <param name="dbParams">Parameters.</param>
        public SqlInfo(string sqlText, Dictionary<string, DbParam> dbParams)
            : base(sqlText, dbParams) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">Source.</param>
        public SqlInfo(SqlInfo src) : base(src) { }
    }
}
