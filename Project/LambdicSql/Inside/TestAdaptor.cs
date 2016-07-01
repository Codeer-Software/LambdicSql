using LambdicSql.QueryBase;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    //for unit testing.
    public static class TestAdaptor
    {
        public static string ToSqlString(DbInfo info, Expression exp)
            => new ExpressionDecoder(info, null).ToStringCore(exp).Text;
        public static string ToSqlString(IQuery query)
            => ExpressionDecoder.ToString(query, null);
    }
}
