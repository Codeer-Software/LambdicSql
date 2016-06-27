namespace LambdicSql
{
    public static class SelectFuncsExtensions
    {
        public static T Sum<T>(this ISelectFuncs func, T item) { return default(T); }
        public static T Avg<T>(this ISelectFuncs func, T item) { return default(T); }
        public static T Count<T>(this ISelectFuncs func, T item) { return default(T); }
        public static T Min<T>(this ISelectFuncs func, T item) { return default(T); }
        public static T Max<T>(this ISelectFuncs func, T item) { return default(T); }
    }
}
