namespace LambdicSql.ConverterServices.Inside
{
    class TableInfo
    {
        internal string LambdaFullName { get; }
        internal string SqlFullName { get; }

        internal TableInfo(string lambdaFullName, string sqlFullName)
        {
            LambdaFullName = lambdaFullName;
            SqlFullName = sqlFullName;
        }
    }
}
