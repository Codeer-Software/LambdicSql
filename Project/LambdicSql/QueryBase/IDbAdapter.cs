using System.Data.Common;

namespace LambdicSql.QueryBase
{
    public interface IDbAdapter
    {
        IQueryCustomizer CreateQueryCustomizer();
        DbCommand CreateCommand();
        DbConnection CreateConnection();
        DbParameter CreateParameter(string parameterName, object value);
    }
}
