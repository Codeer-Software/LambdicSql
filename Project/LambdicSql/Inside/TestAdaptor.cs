using LambdicSql.QueryBase;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    //for unit testing.
    public static class TestAdaptor
    {
        public static string ToSqlString(DbInfo info, Expression exp)
            => new SqlStringConverter(info, null).ToString(exp);
        public static string ToSqlString(IQuery query)
            => SqlStringConverter.ToString(query, null);
    }
}
