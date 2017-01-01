using LambdicSql.ExpressionConverterService;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql
{
    /// <summary>
    /// SQL information.
    /// </summary>
    public class BuildedSql
    {
        Dictionary<string, DbParam> _dbParams;

        /// <summary>
        /// Object create information.
        /// </summary>
        public ObjectCreateInfo SelectClauseInfo { get; }

        /// <summary>
        /// DataBase information.
        /// </summary>
        public DbInfo DbInfo { get; }

        //単なるTextでいいか
        /// <summary>
        /// Sql text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Parameters.
        /// </summary>
        public Dictionary<string, DbParam> DbParams => _dbParams.ToDictionary(e => e.Key, e => e.Value);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbInfo">DataBase information.</param>
        /// <param name="sqlText">Sql text.</param>
        /// <param name="selectClauseInfo">Object create information.</param>
        /// <param name="dbParams">Parameters.</param>
        public BuildedSql(DbInfo dbInfo, string sqlText, ObjectCreateInfo selectClauseInfo, Dictionary<string, DbParam> dbParams)
        {
            DbInfo = dbInfo;
            Text = sqlText;
            SelectClauseInfo = selectClauseInfo;
            _dbParams = dbParams;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="src">Source.</param>
        public BuildedSql(BuildedSql src)
        {
            DbInfo = src.DbInfo;
            Text = src.Text;
            SelectClauseInfo = src.SelectClauseInfo;
            _dbParams = src._dbParams;
        }
    }

    /// <summary>
    /// SQL information.
    /// </summary>
    /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
    public class BuildedSql<TSelected> : BuildedSql
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbInfo">DataBase information.</param>
        /// <param name="sqlText">Sql text.</param>
        /// <param name="selectClauseInfo">Object create information.</param>
        /// <param name="dbParams">Parameters.</param>
        public BuildedSql(DbInfo dbInfo, string sqlText, ObjectCreateInfo selectClauseInfo, Dictionary<string, DbParam> dbParams)
            : base(dbInfo, sqlText, selectClauseInfo, dbParams) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">Source.</param>
        public BuildedSql(BuildedSql src) : base(src) { }
    }
}
