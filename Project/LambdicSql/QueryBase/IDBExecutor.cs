using System.Collections.Generic;

namespace LambdicSql.QueryBase
{
    public interface IDbExecutor<TSelect>
    {
        string CommandText { get; }
        IEnumerable<TSelect> Read();
    }
}
