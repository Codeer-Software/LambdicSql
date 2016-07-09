using System.Collections.Generic;

namespace LambdicSql.QueryBase
{
    public interface ISqlExecutor<TSelect>
    {
        IEnumerable<TSelect> Read();
        int Write();
    }
}
