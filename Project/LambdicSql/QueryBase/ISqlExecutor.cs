using System.Collections.Generic;

namespace LambdicSql.QueryBase
{
    public interface ISqlExecutor<TSelect>
    {
        string CommandText { get; }
        IEnumerable<TSelect> Read();
        int Write();
    }
}
