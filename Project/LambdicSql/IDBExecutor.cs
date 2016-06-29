using System.Collections.Generic;

namespace LambdicSql
{
    public interface IDBExecutor<TSelect>
    {
        string CommandText { get; }
        IEnumerable<TSelect> Read();
    }
}
