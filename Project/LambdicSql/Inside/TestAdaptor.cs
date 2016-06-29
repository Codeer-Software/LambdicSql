using LambdicSql.QueryInfo;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    //for unit testing.
    public static class TestAdaptor
    {
        public static string ToSqlString(DbInfo info, Expression exp) 
            => new ExpressionParser(info, new QueryParser()).ToString(exp).Text;
    }
}
