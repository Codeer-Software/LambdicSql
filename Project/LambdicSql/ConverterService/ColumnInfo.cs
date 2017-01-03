using System;

namespace LambdicSql.ConverterService
{
    /// <summary>
    /// Column info.
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// Column type.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Full name in lambda.
        /// </summary>
        public string LambdaFullName { get; }

        /// <summary>
        /// Full name at Sql.
        /// </summary>
        public string SqlFullName { get; }

        /// <summary>
        /// Column name at Sql.
        /// </summary>
        public string SqlColumnName { get; }

        internal ColumnInfo(Type type, string lambdaFullName, string sqlFullName, string sqlColumnName)
        {
            Type = type;
            LambdaFullName = lambdaFullName;
            SqlFullName = sqlFullName;
            SqlColumnName = sqlColumnName;
        }
    }
}
