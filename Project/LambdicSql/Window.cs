using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    [SqlSyntax]
    public static class Window
    {
        public interface IFuncAfter : IMethodChain { }

        //一応型を伝えてあげることができる
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Avg(object expression) => InvalitContext.Throw<IFuncAfter>(nameof(Avg));
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Sum(object expression) => InvalitContext.Throw<IFuncAfter>(nameof(Sum));
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Count(object expression) => InvalitContext.Throw<IFuncAfter>(nameof(Count));
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Max(object expression) => InvalitContext.Throw<IFuncAfter>(nameof(Max));
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Min(object expression) => InvalitContext.Throw<IFuncAfter>(nameof(Min));

        //その型が帰る
        [MethodGroup(nameof(Window))]
        public static IFuncAfter First_Value(object expression) => InvalitContext.Throw<IFuncAfter>(nameof(Min));
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Last_Value(object expression) => InvalitContext.Throw<IFuncAfter>(nameof(Min));

        //多分longでいいんやけどね・・・
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Rank() => InvalitContext.Throw<IFuncAfter>(nameof(Min));
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Dense_Rank() => InvalitContext.Throw<IFuncAfter>(nameof(Min));
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Percent_Rank() => InvalitContext.Throw<IFuncAfter>(nameof(Min));

        //double
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Cume_Dist() => InvalitContext.Throw<IFuncAfter>(nameof(Min));

        //longが帰る
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Ntile(object groupCount) => InvalitContext.Throw<IFuncAfter>(nameof(Min));
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Nth_Value(object expression, object offset) => InvalitContext.Throw<IFuncAfter>(nameof(Min));

        //その型がかえるんやっけ？
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Lag(object expression) => InvalitContext.Throw<IFuncAfter>(nameof(Lag));
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Lag(object expression, object offset) => InvalitContext.Throw<IFuncAfter>(nameof(Lag));
        [MethodGroup(nameof(Window))]
        public static IFuncAfter Lag(object expression, object offset, object @default) => InvalitContext.Throw<IFuncAfter>(nameof(Lag));


        [MethodGroup(nameof(Window))]
        public static T Over<T>(this IFuncAfter words, OrderBy orderBy) => InvalitContext.Throw<T>(nameof(Over));

        [MethodGroup(nameof(Window))]
        public static T Over<T>(this IFuncAfter words, OrderBy orderBy, Rows rows) => InvalitContext.Throw<T>(nameof(Over));

        [MethodGroup(nameof(Window))]
        public static T Over<T>(this IFuncAfter words, PartitionBy partitionBy, OrderBy orderBy, Rows rows) => InvalitContext.Throw<T>(nameof(Over));


        static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var func = converter.MakeNormalSqlFunctionString(methods[0]);

            var overMethod = methods[1];
            var over = overMethod.Method.Name.ToUpper() + "(" +
                string.Join(" ", overMethod.Arguments.Skip(1).Where(e=>!(e is ConstantExpression)).Select(e => converter.ToString(e)).ToArray()) + ")";

            return func + over;
        }
    }
}
