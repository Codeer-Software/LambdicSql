using LambdicSql.QueryBase;
using System.Collections.Generic;

namespace LambdicSql
{
    public class SqlInfo<TSelected>
    {
        public SelectClauseInfo SelectClauseInfo { get; }
        public DbInfo DbInfo { get; }
        public string SqlText { get; }
        public Dictionary<string, object> Parameters { get; }
        public SqlInfo(DbInfo dbInfo, string sqlText, SelectClauseInfo selectClauseInfo, Dictionary<string, object> parameters)
        {
            DbInfo = dbInfo;
            SqlText = sqlText;
            SelectClauseInfo = selectClauseInfo;
            Parameters = parameters;
        }
    }
}
