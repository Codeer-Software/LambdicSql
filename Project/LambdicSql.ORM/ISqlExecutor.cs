using System.Collections.Generic;

namespace LambdicSql.SqlBase
{
    public interface ISqlExecutor<TSelect>
    {
        IEnumerable<TSelect> Read();
        int Write();
    }
}
