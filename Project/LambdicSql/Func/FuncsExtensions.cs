using LambdicSql.QueryBase;

namespace LambdicSql
{
    public static class FuncsExtensions
    {
        public static T Sum<T>(this ISqlFunc func, T item) { return default(T); }
        public static T Avg<T>(this ISqlFunc func, T item) { return default(T); }
        public static T Count<T>(this ISqlFunc func, T item) { return default(T); }
        public static T Min<T>(this ISqlFunc func, T item) { return default(T); }
        public static T Max<T>(this ISqlFunc func, T item) { return default(T); }
    }
}
