using LambdicSql.SqlBase;
using System.Collections.Generic;

namespace LambdicSql
{
    public class SqlInfo
    {
        public ObjectCreateInfo SelectClauseInfo { get; }
        public DbInfo DbInfo { get; }
        public string SqlText { get; }
        public Dictionary<string, object> Parameters { get; }
        public SqlInfo(DbInfo dbInfo, string sqlText, ObjectCreateInfo selectClauseInfo, Dictionary<string, object> parameters)
        {
            DbInfo = dbInfo;
            SqlText = sqlText;
            SelectClauseInfo = selectClauseInfo;
            Parameters = parameters;
        }
        public SqlInfo(SqlInfo src)
        {
            DbInfo = src.DbInfo;
            SqlText = src.SqlText;
            SelectClauseInfo = src.SelectClauseInfo;
            Parameters = src.Parameters;
        }
    }

    public class SqlInfo<TSelected> : SqlInfo
    {
        public SqlInfo(DbInfo dbInfo, string sqlText, ObjectCreateInfo selectClauseInfo, Dictionary<string, object> parameters)
            : base(dbInfo, sqlText, selectClauseInfo, parameters) { }
        public SqlInfo(SqlInfo src) : base(src) { }
    }
}
