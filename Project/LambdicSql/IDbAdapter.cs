using LambdicSql.QueryInfo;
using System.Data.Common;

namespace LambdicSql
{
    public interface IDbAdapter
    {
        QueryDecoder CreateParser();
        DbCommand CreateCommand();
        DbConnection CreateConnection();
    }
}
