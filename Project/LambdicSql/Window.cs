using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// SQL Window functions.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// </summary>
    [SqlSyntax]
    public static class Window
    {
        public interface IFuncAfter<T> : IMethodChain { }

        /// <summary>
        /// SUM function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<T> Sum<T>(T column) => InvalitContext.Throw<IFuncAfter<T>>(nameof(Sum));

        /// <summary>
        /// SUM function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Total.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<T> Sum<T>(AggregatePredicate aggregatePredicate, T column) => InvalitContext.Throw<IFuncAfter<T>>(nameof(Sum));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<int> Count(object column) => InvalitContext.Throw<IFuncAfter<int>>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="asterisk"></param>
        /// <returns>Count.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<int> Count(Asterisk asterisk) => InvalitContext.Throw<IFuncAfter<int>>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Count.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<int> Count(AggregatePredicate aggregatePredicate, object column) => InvalitContext.Throw<IFuncAfter<int>>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Count.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<int> Count(AggregatePredicate aggregatePredicate, Asterisk asterisk) => InvalitContext.Throw<IFuncAfter<int>>(nameof(Count));

        /// <summary>
        /// AVG function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<double> Avg(object colum) => InvalitContext.Throw<IFuncAfter<double>>(nameof(Avg));

        /// <summary>
        /// MIN function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<T> Min<T>(T column) => InvalitContext.Throw<IFuncAfter<T>>(nameof(Min));

        /// <summary>
        /// MAX function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<T> Max<T>(T column) => InvalitContext.Throw<IFuncAfter<T>>(nameof(Max));

        /// <summary>
        /// FIRST_VALUE function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<T> First_Value<T>(T column) => InvalitContext.Throw<IFuncAfter<T>>(nameof(Min));

        /// <summary>
        /// LAST_VALUE function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<T> Last_Value<T>(T column) => InvalitContext.Throw<IFuncAfter<T>>(nameof(Min));

        /// <summary>
        /// RANK function.
        /// </summary>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<int> Rank() => InvalitContext.Throw<IFuncAfter<int>>(nameof(Min));

        /// <summary>
        /// DENSE_RANK function.
        /// </summary>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<int> Dense_Rank() => InvalitContext.Throw<IFuncAfter<int>>(nameof(Min));

        /// <summary>
        /// PERCENT_RANK function.
        /// </summary>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<double> Percent_Rank() => InvalitContext.Throw<IFuncAfter<double>>(nameof(Min));

        /// <summary>
        /// CUME_DIST function.
        /// </summary>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<double> Cume_Dist() => InvalitContext.Throw<IFuncAfter<double>>(nameof(Min));

        /// <summary>
        /// NTILE function.
        /// </summary>
        /// <param name="groupCount">The number of ranking groups.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<int> Ntile(object groupCount) => InvalitContext.Throw<IFuncAfter<int>>(nameof(Min));

        /// <summary>
        /// NTH_VALUE function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">Specify the number of lines associated with the first line of the window that returns the expression.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<int> Nth_Value(object column, object offset) => InvalitContext.Throw<IFuncAfter<int>>(nameof(Min));

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<T> Lag<T>(T column) => InvalitContext.Throw<IFuncAfter<T>>(nameof(Lag));

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">An optional parameter that specifies the number of lines before the current line that returns a value.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<T> Lag<T>(T column, object offset) => InvalitContext.Throw<IFuncAfter<T>>(nameof(Lag));

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">An optional parameter that specifies the number of lines before the current line that returns a value.</param>
        /// <param name="default">The value returned if the value specified by offset is NULL.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static IFuncAfter<T> Lag<T>(T column, object offset, T @default) => InvalitContext.Throw<IFuncAfter<T>>(nameof(Lag));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [MethodGroup(nameof(Window))]
        public static T Over<T>(this IFuncAfter<T> before, PartitionBy partitionBy) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [MethodGroup(nameof(Window))]
        public static T Over<T>(this IFuncAfter<T> before, PartitionBy partitionBy, OrderBy orderBy) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [MethodGroup(nameof(Window))]
        public static T Over<T>(this IFuncAfter<T> before, PartitionBy partitionBy, Rows rows) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [MethodGroup(nameof(Window))]
        public static T Over<T>(this IFuncAfter<T> before, PartitionBy partitionBy, OrderBy orderBy, Rows rows) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [MethodGroup(nameof(Window))]
        public static T Over<T>(this IFuncAfter<T> before, OrderBy orderBy) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">Getting row order.</param>
        /// <returns>It is the result of Window function.</returns>
        [MethodGroup(nameof(Window))]
        public static T Over<T>(this IFuncAfter<T> before, OrderBy orderBy, Rows rows) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="rows">Getting row order.</param>
        /// <returns>It is the result of Window function.</returns>
        [MethodGroup(nameof(Window))]
        public static T Over<T>(this IFuncAfter<T> before, Rows rows) => InvalitContext.Throw<T>(nameof(Over));

        static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            string func = string.Empty;
            switch (methods[0].Method.Name)
            {
                case nameof(Sum):
                case nameof(Count):
                    func = Funcs.ToString(converter, new[] { methods[0] });
                    break;
                default:
                    func = converter.MakeNormalSqlFunctionString(methods[0]);
                    break;
            }

            var overMethod = methods[1];
            var over = overMethod.Method.Name.ToUpper() + "(" +
                string.Join(" ", overMethod.Arguments.Skip(1).Where(e=>!(e is ConstantExpression)).Select(e => converter.ToString(e)).ToArray()) + ")";

            return func + " " + over;
        }
    }
}
