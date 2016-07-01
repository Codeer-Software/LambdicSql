using LambdicSql.Clause.Having;

namespace LambdicSql
{
    public static class HavingFuncsExtensions
    {
        public static T Sum<T>(this IHavingFuncs func, T item) { return default(T); }
        public static T Avg<T>(this IHavingFuncs func, T item) { return default(T); }
        public static T Count<T>(this IHavingFuncs func, T item) { return default(T); }
        public static T Min<T>(this IHavingFuncs func, T item) { return default(T); }
        public static T Max<T>(this IHavingFuncs func, T item) { return default(T); }
    }
}
