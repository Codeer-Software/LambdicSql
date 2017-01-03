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

        //TODO これを排除  ORMは一旦削除の方向で
        /// <summary>
        /// Object create information.
        /// </summary>
        public ObjectCreateInfo SelectClauseInfo { get; }

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
        /// <param name="selectClauseInfo">Object create information.</param>
        /// <param name="dbParams">Parameters.</param>
        public SqlInfo(string sqlText, ObjectCreateInfo selectClauseInfo, Dictionary<string, DbParam> dbParams)
        {
            Text = sqlText;
            SelectClauseInfo = selectClauseInfo;
            _dbParams = dbParams;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="src">Source.</param>
        public SqlInfo(SqlInfo src)
        {
            Text = src.Text;
            SelectClauseInfo = src.SelectClauseInfo;
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
        /// <param name="selectClauseInfo">Object create information.</param>
        /// <param name="dbParams">Parameters.</param>
        public SqlInfo(string sqlText, ObjectCreateInfo selectClauseInfo, Dictionary<string, DbParam> dbParams)
            : base(sqlText, selectClauseInfo, dbParams) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">Source.</param>
        public SqlInfo(SqlInfo src) : base(src) { }
    }
}
