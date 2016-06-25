using System.Collections.Generic;

namespace LambdicSql
{
    public interface ISqlExecutor<TSelect>
    {
        IEnumerable<TSelect> Read();
    }
}
