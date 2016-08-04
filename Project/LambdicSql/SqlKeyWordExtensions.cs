using LambdicSql.QueryBase;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class SqlKeyWordExtensions
    {
        public static ISqlFunc Func<T>(this ISqlKeyWord<T> words) => null;
        public static ISqlUtility Util<T>(this ISqlKeyWord<T> words) => null;
        public static IWindowFuncs Window<T>(this ISqlKeyWord<T> words) => null;
        public static T Cast<T>(this ISqlKeyWord words) => default(T);

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            return string.Empty;
        }
    }
}
