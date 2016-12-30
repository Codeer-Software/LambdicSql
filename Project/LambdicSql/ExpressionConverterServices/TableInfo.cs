namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Table info.
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// Full name in lambda.
        /// </summary>
        public string LambdaFullName { get; }

        /// <summary>
        /// Full name at Sql.
        /// </summary>
        public string SqlFullName { get; }

        internal TableInfo(string lambdaFullName, string sqlFullName)
        {
            LambdaFullName = lambdaFullName;
            SqlFullName = sqlFullName;
        }
    }
}
