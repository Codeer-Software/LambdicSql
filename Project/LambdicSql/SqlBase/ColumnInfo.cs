using System;

namespace LambdicSql.SqlBase
{
    public class ColumnInfo
    {
        DbParam _dbParam;

        public Type Type { get; }
        public string LambdaFullName { get; }
        public string SqlFullName { get; }

        public ColumnInfo(Type type, string lambdaFullName, string sqlFullName, DbParam dbParam)
        {
            LambdaFullName = lambdaFullName;
            SqlFullName = sqlFullName;
            _dbParam = dbParam;
        }

        internal DbParam GetDbParamClone() => _dbParam?.Clone();
    }
}
