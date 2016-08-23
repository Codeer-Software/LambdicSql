using System;

namespace LambdicSql.SqlBase
{
    public class ColumnInfo
    {
        public Type Type { get; }
        public string LambdaFullName { get; }
        public string SqlFullName { get; }
        public string SqlColumnName { get; }

        public ColumnInfo(Type type, string lambdaFullName, string sqlFullName, string sqlColumnName)
        {
            LambdaFullName = lambdaFullName;
            SqlFullName = sqlFullName;
            SqlColumnName = sqlColumnName;
        }
    }
}
