using System;

namespace LambdicSql.QueryBase
{
    public class PreviousCondition
    {
        public static implicit operator bool(PreviousCondition src)
        {
            throw new NotSupportedException("do not call cast except in expression.");
        }
    }
}
