using LambdicSql.SqlBase;
using System.Collections.Generic;

namespace LambdicSql
{
    public class SqlInfo
    {
        PrepareParameters _parameters;

        public ObjectCreateInfo SelectClauseInfo { get; }
        public DbInfo DbInfo { get; }
        public string SqlText { get; }
        public Dictionary<string, object> Params => _parameters.GetParams();
        public Dictionary<string, DbParam> DbParams => _parameters.GetDbParams();

        public SqlInfo(DbInfo dbInfo, string sqlText, ObjectCreateInfo selectClauseInfo, PrepareParameters parameters)
        {
            DbInfo = dbInfo;
            SqlText = sqlText;
            SelectClauseInfo = selectClauseInfo;
            _parameters = parameters;
        }
        public SqlInfo(SqlInfo src)
        {
            DbInfo = src.DbInfo;
            SqlText = src.SqlText;
            SelectClauseInfo = src.SelectClauseInfo;
            _parameters = src._parameters;
        }
    }

    public class SqlInfo<TSelected> : SqlInfo
    {
        public SqlInfo(DbInfo dbInfo, string sqlText, ObjectCreateInfo selectClauseInfo, PrepareParameters parameters)
            : base(dbInfo, sqlText, selectClauseInfo, parameters) { }
        public SqlInfo(SqlInfo src) : base(src) { }
    }
}
