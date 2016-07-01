using System.Data.Common;

namespace LambdicSql.QueryBase
{
    public interface IDbAdapter
    {
        QueryDecoder CreateParser();//TODO change type.
        DbCommand CreateCommand();
        DbConnection CreateConnection();
    }
}
