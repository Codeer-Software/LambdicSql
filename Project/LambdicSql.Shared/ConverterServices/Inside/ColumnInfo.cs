using System;

namespace LambdicSql.ConverterServices.Inside
{
    class ColumnInfo
    {
        internal Type Type { get; }
        internal string LambdaFullName { get; }
        internal string SqlFullName { get; }
        internal string SqlColumnName { get; }

        internal ColumnInfo(Type type, string lambdaFullName, string sqlFullName, string sqlColumnName)
        {
            Type = type;
            LambdaFullName = lambdaFullName;
            SqlFullName = sqlFullName;
            SqlColumnName = sqlColumnName;
        }
    }
}
