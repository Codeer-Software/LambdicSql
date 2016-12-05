using LambdicSql.SqlBase;
using System.Collections.Generic;

namespace LambdicSql
{
    /// <summary>
    /// SQL information.
    /// </summary>
    public class SqlInfo
    {
        PrepareParameters _parameters;

        /// <summary>
        /// Object create information.
        /// </summary>
        public ObjectCreateInfo SelectClauseInfo { get; }

        /// <summary>
        /// DataBase information.
        /// </summary>
        public DbInfo DbInfo { get; }

        /// <summary>
        /// Sql text.
        /// </summary>
        public string SqlText { get; }

        /// <summary>
        /// Parameters.
        /// </summary>
        public Dictionary<string, DbParam> DbParams => _parameters.GetDbParams();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbInfo">DataBase information.</param>
        /// <param name="sqlText">Sql text.</param>
        /// <param name="selectClauseInfo">Object create information.</param>
        /// <param name="parameters">Parameters.</param>
        public SqlInfo(DbInfo dbInfo, string sqlText, ObjectCreateInfo selectClauseInfo, PrepareParameters parameters)
        {
            DbInfo = dbInfo;
            SqlText = sqlText;
            SelectClauseInfo = selectClauseInfo;
            _parameters = parameters;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="src">Source.</param>
        public SqlInfo(SqlInfo src)
        {
            DbInfo = src.DbInfo;
            SqlText = src.SqlText;
            SelectClauseInfo = src.SelectClauseInfo;
            _parameters = src._parameters;
        }
    }

    /// <summary>
    /// SQL information.
    /// </summary>
    /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
    public class SqlInfo<TSelected> : SqlInfo
    {
        public SqlInfo(DbInfo dbInfo, string sqlText, ObjectCreateInfo selectClauseInfo, PrepareParameters parameters)
            : base(dbInfo, sqlText, selectClauseInfo, parameters) { }
        public SqlInfo(SqlInfo src) : base(src) { }
    }
}
