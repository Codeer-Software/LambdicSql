using System.Data.Common;

namespace LambdicSql.QueryBase
{
    public interface IDbAdapter
    {
        IQueryCustomizer CreateQueryCustomizer();
        DbCommand CreateCommand();
        DbConnection CreateConnection();
    }
}
