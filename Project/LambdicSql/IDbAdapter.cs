using LambdicSql.QueryInfo;
using System.Data.Common;

namespace LambdicSql
{
    public interface IDbAdapter
    {
        QueryParser CreateParser();
        DbCommand CreateCommand();
        DbConnection CreateConnection();
    }
}
