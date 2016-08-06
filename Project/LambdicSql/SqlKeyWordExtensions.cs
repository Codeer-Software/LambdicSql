using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class SqlKeyWordExtensions
    {
        public static ISqlFunc Func<T>(this ISqlKeyWord<T> words) => InvalitContext.Throw<ISqlFunc>(nameof(Func));
        public static ISqlUtility Util<T>(this ISqlKeyWord<T> words) => InvalitContext.Throw<ISqlUtility>(nameof(Util));
        public static IWindowFuncs Window<T>(this ISqlKeyWord<T> words) => InvalitContext.Throw<IWindowFuncs>(nameof(Window));
        public static T Cast<T>(this ISqlKeyWord words) => default(T);
        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods) => string.Empty;
    }
}
