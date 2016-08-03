using LambdicSql.QueryBase;
using System.Collections.Generic;

namespace LambdicSql
{
    public class SqlInfo<TSelected>
    {
        public DbInfo DbInfo { get; }
        public string SqlText { get; }
        public Dictionary<string, object> Parameters { get; }
        public SqlInfo(DbInfo dbInfo, string sqlText, Dictionary<string, object> parameters)
        {
            DbInfo = dbInfo;
            SqlText = sqlText;
            Parameters = parameters;
        }
    }
}
