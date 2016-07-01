using LambdicSql.QueryBase;
using System;

namespace LambdicSql.Inside
{
    internal static class QueryInternalExtensions
    {
        internal static Query<TDB, TSelect> CustomClone<TDB, TSelect>(this IQuery<TDB, TSelect> query, Action<Query<TDB, TSelect>> custom)
            where TDB : class
            where TSelect : class
        {
            var src = query as Query<TDB, TSelect>;
            var dst = src.Clone();
            custom(dst);
            return dst;
        }
    }
}
