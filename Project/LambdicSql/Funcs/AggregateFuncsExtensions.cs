using LambdicSql.QueryBase;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class AggregateFuncsExtensions
    {
        public static T Sum<T>(this ISqlFuncs func, T item) => default(T);
        public static T Sum<T>(this ISqlFuncs func, AggregatePredicate aggregatePredicate, T item) => default(T);
        public static T Count<T>(this ISqlFuncs func, T item) => default(T);
        public static T Count<T>(this ISqlFuncs func, AggregatePredicate aggregatePredicate, T item) => default(T);
        public static T Avg<T>(this ISqlFuncs func, T item) => default(T);
        public static T Min<T>(this ISqlFuncs func, T item) => default(T);
        public static T Max<T>(this ISqlFuncs func, T item) => default(T);

        public static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Skip(1).Select(e => converter.ToString(e)).ToArray();
            if (method.Arguments.Count != 3) return null;
            return method.Method.Name.ToUpper() + "(" + args[0].ToUpper() + " " + args[1] + ")";
        }
    }
}
