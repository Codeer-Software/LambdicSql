using System.Collections.Generic;

namespace LambdicSql.QueryBase
{
    public interface IDBExecutor<TSelect>
    {
        string CommandText { get; }
        IEnumerable<TSelect> Read();
    }
}
