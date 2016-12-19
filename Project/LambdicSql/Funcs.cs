using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql
{
    /// <summary>
    /// SQL Functions.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// Use[using static LambdicSql.Funcs;], you can use to write natural SQL.
    /// </summary>
    [SqlSyntax]
    public static class Funcs
    {
        /// <summary>
        /// SUM function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Total.</returns>
        public static T Sum<T>(T column) => InvalitContext.Throw<T>(nameof(Sum));

        /// <summary>
        /// SUM function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Total.</returns>
        public static T Sum<T>(AggregatePredicate aggregatePredicate, T column) => InvalitContext.Throw<T>(nameof(Sum));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Count.</returns>
        public static int Count(object column) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="asterisk">*</param>
        /// <returns>Count.</returns>
        public static int Count(IAsterisk asterisk) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Count.</returns>
        public static int Count(AggregatePredicate aggregatePredicate, object column) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Count.</returns>
        public static int Count(AggregatePredicate aggregatePredicate, IAsterisk asterisk) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// AVG function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Average.</returns>
        public static double Avg(object column) => InvalitContext.Throw<double>(nameof(Avg));

        /// <summary>
        /// MIN function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Minimum.</returns>
        public static T Min<T>(T column) => InvalitContext.Throw<T>(nameof(Min));

        /// <summary>
        /// MAX function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Maximum.</returns>
        public static T Max<T>(T column) => InvalitContext.Throw<T>(nameof(Max));

        /// <summary>
        /// ABS function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Absolute value.</returns>
        public static T Abs<T>(T column) => InvalitContext.Throw<T>(nameof(Abs));

        /// <summary>
        /// MOD function.
        /// </summary>
        /// <typeparam name="T">Type represented by target</typeparam>
        /// <param name="target">Numeric expression to divide.</param>
        /// <param name="div">A numeric expression that divides the dividend.</param>
        /// <returns>Surplus.</returns>
        public static T Mod<T>(T target, object div) => InvalitContext.Throw<T>(nameof(Mod));

        /// <summary>
        /// ROUND function.
        /// </summary>
        /// <typeparam name="T">Type represented by target.</typeparam>
        /// <param name="target">Numeric expression to round.</param>
        /// <param name="digit">Is the precision to which it is to be rounded.</param>
        /// <returns>Rounded result.</returns>
        public static T Round<T>(T target, object digit) => InvalitContext.Throw<T>(nameof(Round));

        /// <summary>
        /// CONCAT function.
        /// </summary>
        /// <param name="targets">A string value to concatenate to the other values.</param>
        /// <returns>concatenated result.</returns>
        public static string Concat(params object[] targets) => InvalitContext.Throw<string>(nameof(Concat));

        /// <summary>
        /// LENGTH function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>String length.</returns>
        public static int Length(object target) => InvalitContext.Throw<int>(nameof(Length));

        /// <summary>
        /// LEN function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>String length.</returns>
        public static int Len(object target) => InvalitContext.Throw<int>(nameof(Len));

        /// <summary>
        /// LOWER function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>Changed string.</returns>
        public static string Lower(object target) => InvalitContext.Throw<string>(nameof(Lower));

        /// <summary>
        /// UPPER function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>Changed string.</returns>
        public static string Upper(object target) => InvalitContext.Throw<string>(nameof(Upper));

        /// <summary>
        /// REPLACE function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <param name="src">source.</param>
        /// <param name="dst">destination.</param>
        /// <returns>Changed string.</returns>
        public static string Replace(object target, object src, object dst) => InvalitContext.Throw<string>(nameof(Replace));

        /// <summary>
        /// SUBSTRING function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <param name="startIndex">Specify the starting position of the character string to be acquired.</param>
        /// <param name="length">Specify the length of the string to be retrieved.</param>
        /// <returns>Part of a text.</returns>
        public static string Substring(object target, object startIndex, object length) => InvalitContext.Throw<string>(nameof(Substring));

        /// <summary>
        /// EXTRACT function.
        /// </summary>
        /// <param name="element">Part type.</param>
        /// <param name="src">The date data.</param>
        /// <returns>A part from the date data.</returns>
        public static double Extract(DateTimeElement element, DateTime src) => InvalitContext.Throw<double>(nameof(Extract));

        /// <summary>
        /// DATEPART function.
        /// </summary>
        /// <param name="element">Part type.</param>
        /// <param name="src">The date data.</param>
        /// <returns>A part from the date data.</returns>
        public static int DatePart(DateTimeElement element, DateTime src) => InvalitContext.Throw<int>(nameof(Extract));

        /// <summary>
        /// CAST function.
        /// </summary>
        /// <typeparam name="TDst">Type of destination.</typeparam>
        /// <param name="target"></param>
        /// <param name="destinationType">Type of destination.</param>
        /// <returns>Converted data.</returns>
        public static TDst Cast<TDst>(object target, string destinationType) => InvalitContext.Throw<TDst>(nameof(Cast));

        /// <summary>
        /// COALESCE function.
        /// </summary>
        /// <typeparam name="T">Type of parameter</typeparam>
        /// <param name="parameter">Parameter.</param>
        /// <returns>The first non-null value in the parameter.</returns>
        public static T Coalesce<T>(params T[] parameter) => InvalitContext.Throw<T>(nameof(Coalesce));

        /// <summary>
        /// NVL function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="expression1">expression.</param>
        /// <param name="expression2">expression.</param>
        /// <returns>expression1 or expression2.</returns>
        public static T NVL<T>(T expression1, T expression2) => InvalitContext.Throw<T>(nameof(NVL));

        internal static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            switch (method.Method.Name)
            {
                case nameof(Sum):
                case nameof(Count):
                    if (method.Arguments.Count == 2) return FuncSpace(method.Method.Name.ToUpper(), args);
                    break;
                case nameof(Extract):
                    return FuncSpace(method.Method.Name.ToUpper(), args[0], "FROM", args[1]);
                case nameof(Cast):
                    return FuncSpace("CAST", args[0], "AS", args[1].Customize(new CustomizeParameterToObject()));
                default:
                    break;
            }
            return Func(method.Method.Name.ToUpper(), args);
        }
    }
}
