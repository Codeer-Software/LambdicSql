using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    [SqlSyntax]
    public static class Funcs
    {
        public static T Sum<T>(T item) => InvalitContext.Throw<T>(nameof(Sum));
        public static T Sum<T>(AggregatePredicate aggregatePredicate, T item) => InvalitContext.Throw<T>(nameof(Sum));
        public static T Count<T>(T item) => InvalitContext.Throw<T>(nameof(Count));
        public static T Count<T>(AggregatePredicate aggregatePredicate, T item) => InvalitContext.Throw<T>(nameof(Count));
        public static T Avg<T>(T item) => InvalitContext.Throw<T>(nameof(Avg));
        public static T Min<T>(T item) => InvalitContext.Throw<T>(nameof(Min));
        public static T Max<T>(T item) => InvalitContext.Throw<T>(nameof(Max));

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var args = method.Arguments.Select(e => converter.ToString(e)).ToArray();
            if (method.Arguments.Count != 2) return converter.MakeNormalSqlFunctionString(method);
            return method.Method.Name.ToUpper() + "(" + args[0].ToUpper() + " " + args[1] + ")";
        }
    }
}
