using System;

namespace LambdicSql.Inside
{
    internal static class QueryInternalExtensions
    {
        public static T Custom<T>(this T t, Action<T> custom)
        {
            custom(t);
            return t;
        }
    }
}
