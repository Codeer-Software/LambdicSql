using LambdicSql.QueryInfo;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    //for unit testing.
    public static class TestAdaptor
    {
        public static string ToSqlString(DbInfo info, Expression exp) 
            => ExpressionAnalyzer.ToString(info, exp);
    }
}
