using System;

namespace LambdicSql
{
    public class ColumnInfo
    {
        public Type Type { get; }
        public string LambdaFullName { get; }
        public string SqlFullName { get; }

        public ColumnInfo(Type type, string lambdaFullName, string sqlFullName)
        {
            LambdaFullName = lambdaFullName;
            SqlFullName = sqlFullName;
        }
    }
}
