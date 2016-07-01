using System.Data.Common;

namespace LambdicSql.QueryBase
{
    public interface IDbAdapter
    {
        QueryDecoder CreateParser();
        DbCommand CreateCommand();
        DbConnection CreateConnection();
    }
}
