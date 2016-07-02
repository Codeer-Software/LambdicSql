using System.Linq.Expressions;

namespace LambdicSql.QueryBase
{
    public class TableInfo
    {
        public string LambdaFullName { get; }
        public string SqlFullName { get; }
        public Expression SubQuery { get; }

        public TableInfo(string lambdaFullName, string sqlFullName, Expression subQuery)
        {
            LambdaFullName = lambdaFullName;
            SqlFullName = sqlFullName;
            SubQuery = subQuery;
        }
    }
}
