using System.Collections.Generic;

namespace LambdicSql
{
    public class SqlInfo
    {
        public string SqlText { get; }
        public Dictionary<string, object> Parameters { get; }
        public SqlInfo(string sqlText, Dictionary<string, object> parameters)
        {
            SqlText = sqlText;
            Parameters = parameters;
        }
    }
}
