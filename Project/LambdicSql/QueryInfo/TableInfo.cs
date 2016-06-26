namespace LambdicSql.QueryInfo
{
    public class TableInfo
    {
        public string LambdaFullName { get; }
        public string SqlFullName { get; }

        public TableInfo(string lambdaFullName, string sqlFullName)
        {
            LambdaFullName = lambdaFullName;
            SqlFullName = sqlFullName;
        }
    }
}
