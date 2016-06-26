using System;

namespace LambdicSql.QueryInfo
{
    public class ColumnInfo
    {
        public string LambdaFullName { get; }
        public string SqlFullName { get; }

        public ColumnInfo(string lambdaFullName, string sqlFullName)
        {
            LambdaFullName = lambdaFullName;
            SqlFullName = sqlFullName;
        }
    }
}
