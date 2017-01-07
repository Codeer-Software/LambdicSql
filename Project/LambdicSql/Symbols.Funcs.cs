using LambdicSql.ConverterServices.Inside;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.SymbolConverters.Inside;
using System;

namespace LambdicSql
{
    /// <summary>
    /// SQL Symbols.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// Use[using static LambdicSql.Keywords;], you can use to write natural SQL.
    /// </summary>
    public static partial class Symbols
    {
        /// <summary>
        /// SUM function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Total.</returns>
        [FuncConverter(Separator =" ")]
        public static T Sum<T>(T column) => InvalitContext.Throw<T>(nameof(Sum));

        /// <summary>
        /// SUM function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Total.</returns>
        [FuncConverter(Separator = " ")]
        public static T Sum<T>(IAggregatePredicate aggregatePredicate, T column) => InvalitContext.Throw<T>(nameof(Sum));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Count.</returns>
        [FuncConverter(Separator = " ")]
        public static int Count(object column) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="asterisk">*</param>
        /// <returns>Count.</returns>
        [FuncConverter(Separator = " ")]
        public static int Count(IAsterisk asterisk) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Count.</returns>
        [FuncConverter(Separator = " ")]
        public static int Count(IAggregatePredicate aggregatePredicate, object column) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Count.</returns>
        [FuncConverter(Separator = " ")]
        public static int Count(IAggregatePredicate aggregatePredicate, IAsterisk asterisk) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// AVG function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Average.</returns>
        [FuncConverter]
        public static double Avg(object column) => InvalitContext.Throw<double>(nameof(Avg));

        /// <summary>
        /// MIN function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Minimum.</returns>
        [FuncConverter]
        public static T Min<T>(T column) => InvalitContext.Throw<T>(nameof(Min));

        /// <summary>
        /// MAX function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Maximum.</returns>
        [FuncConverter]
        public static T Max<T>(T column) => InvalitContext.Throw<T>(nameof(Max));

        /// <summary>
        /// ABS function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Absolute value.</returns>
        [FuncConverter]
        public static T Abs<T>(T column) => InvalitContext.Throw<T>(nameof(Abs));

        /// <summary>
        /// MOD function.
        /// </summary>
        /// <typeparam name="T">Type represented by target</typeparam>
        /// <param name="target">Numeric expression to divide.</param>
        /// <param name="div">A numeric expression that divides the dividend.</param>
        /// <returns>Surplus.</returns>
        [FuncConverter]
        public static T Mod<T>(T target, object div) => InvalitContext.Throw<T>(nameof(Mod));

        /// <summary>
        /// ROUND function.
        /// </summary>
        /// <typeparam name="T">Type represented by target.</typeparam>
        /// <param name="target">Numeric expression to round.</param>
        /// <param name="digit">Is the precision to which it is to be rounded.</param>
        /// <returns>Rounded result.</returns>
        [FuncConverter]
        public static T Round<T>(T target, object digit) => InvalitContext.Throw<T>(nameof(Round));

        /// <summary>
        /// CONCAT function.
        /// </summary>
        /// <param name="targets">A string value to concatenate to the other values.</param>
        /// <returns>concatenated result.</returns>
        [FuncConverter]
        public static string Concat(params object[] targets) => InvalitContext.Throw<string>(nameof(Concat));

        /// <summary>
        /// LENGTH function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>String length.</returns>
        [FuncConverter]
        public static int Length(object target) => InvalitContext.Throw<int>(nameof(Length));

        /// <summary>
        /// LEN function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>String length.</returns>
        [FuncConverter]
        public static int Len(object target) => InvalitContext.Throw<int>(nameof(Len));

        /// <summary>
        /// LOWER function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>Changed string.</returns>
        [FuncConverter]
        public static string Lower(object target) => InvalitContext.Throw<string>(nameof(Lower));

        /// <summary>
        /// UPPER function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>Changed string.</returns>
        [FuncConverter]
        public static string Upper(object target) => InvalitContext.Throw<string>(nameof(Upper));

        /// <summary>
        /// REPLACE function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <param name="src">source.</param>
        /// <param name="dst">destination.</param>
        /// <returns>Changed string.</returns>
        [FuncConverter]
        public static string Replace(object target, object src, object dst) => InvalitContext.Throw<string>(nameof(Replace));

        /// <summary>
        /// SUBSTRING function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <param name="startIndex">Specify the starting position of the character string to be acquired.</param>
        /// <param name="length">Specify the length of the string to be retrieved.</param>
        /// <returns>Part of a text.</returns>
        [FuncConverter]
        public static string Substring(object target, object startIndex, object length) => InvalitContext.Throw<string>(nameof(Substring));

        /// <summary>
        /// EXTRACT function.
        /// </summary>
        /// <param name="element">Part type.</param>
        /// <param name="src">The date data.</param>
        /// <returns>A part from the date data.</returns>
        [ExtractConverter]
        public static double Extract(DateTimeElement element, DateTime src) => InvalitContext.Throw<double>(nameof(Extract));

        /// <summary>
        /// DATEPART function.
        /// </summary>
        /// <param name="element">Part type.</param>
        /// <param name="src">The date data.</param>
        /// <returns>A part from the date data.</returns>
        [FuncConverter]
        public static int DatePart(DateTimeElement element, DateTime src) => InvalitContext.Throw<int>(nameof(Extract));

        //TODO stringをやめる
        /// <summary>
        /// CAST function.
        /// </summary>
        /// <typeparam name="TDst">Type of destination.</typeparam>
        /// <param name="target"></param>
        /// <param name="destinationType">Type of destination.</param>
        /// <returns>Converted data.</returns>
        [CastConverter]
        public static TDst Cast<TDst>(object target, IDataType destinationType) => InvalitContext.Throw<TDst>(nameof(Cast));

        /// <summary>
        /// COALESCE function.
        /// </summary>
        /// <typeparam name="T">Type of parameter</typeparam>
        /// <param name="parameter">Parameter.</param>
        /// <returns>The first non-null value in the parameter.</returns>
        [FuncConverter]
        public static T Coalesce<T>(params T[] parameter) => InvalitContext.Throw<T>(nameof(Coalesce));

        /// <summary>
        /// NVL function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="expression1">expression.</param>
        /// <param name="expression2">expression.</param>
        /// <returns>expression1 or expression2.</returns>
        [FuncConverter]
        public static T NVL<T>(T expression1, T expression2) => InvalitContext.Throw<T>(nameof(NVL));

        /// <summary>
        /// FIRST_VALUE function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>For each group, the value of the first row of the sorted record.</returns>
        [FuncConverter]
        public static T First_Value<T>(T column) => InvalitContext.Throw<T>(nameof(First_Value));

        /// <summary>
        /// LAST_VALUE function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>For each group, the value at the end of the sorted record.</returns>
        [FuncConverter]
        public static T Last_Value<T>(T column) => InvalitContext.Throw<T>(nameof(Last_Value));

        /// <summary>
        /// RANK function.
        /// </summary>
        /// <returns>Ranked value.</returns>
        [FuncConverter]
        public static int Rank() => InvalitContext.Throw<int>(nameof(Rank));

        /// <summary>
        /// DENSE_RANK function.
        /// </summary>
        /// <returns>Ranked value.</returns>
        [FuncConverter]
        public static int Dense_Rank() => InvalitContext.Throw<int>(nameof(Dense_Rank));

        /// <summary>
        /// PERCENT_RANK function.
        /// </summary>
        /// <returns>Ranked value.</returns>
        [FuncConverter]
        public static double Percent_Rank() => InvalitContext.Throw<double>(nameof(Percent_Rank));

        /// <summary>
        /// CUME_DIST function.
        /// </summary>
        /// <returns>Cumulative distribution of values in group.</returns>
        [FuncConverter]
        public static double Cume_Dist() => InvalitContext.Throw<double>(nameof(Cume_Dist));

        /// <summary>
        /// NTILE function.
        /// </summary>
        /// <param name="groupCount">The number of ranking groups.</param>
        /// <returns>For each row, NTILE returns the number of the group to which the row belongs.</returns>
        [FuncConverter]
        public static int Ntile(object groupCount) => InvalitContext.Throw<int>(nameof(Ntile));

        /// <summary>
        /// NTH_VALUE function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">Specify the number of lines associated with the first line of the window that returns the expression.</param>
        /// <returns>Returns the value of the expression in the specified row of the window frame associated with the first line of the window.</returns>
        [FuncConverter]
        public static int Nth_Value(object column, object offset) => InvalitContext.Throw<int>(nameof(Nth_Value));

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Returns the value of the row at the specified offset above (before) the current row of the partition.</returns>
        [FuncConverter]
        public static T Lag<T>(T column) => InvalitContext.Throw<T>(nameof(Lag));

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">An optional parameter that specifies the number of lines before the current line that returns a value.</param>
        /// <returns>Returns the value of the row at the specified offset above (before) the current row of the partition.</returns>
        [FuncConverter]
        public static T Lag<T>(T column, object offset) => InvalitContext.Throw<T>(nameof(Lag));

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">An optional parameter that specifies the number of lines before the current line that returns a value.</param>
        /// <param name="default">The value returned if the value specified by offset is NULL.</param>
        /// <returns>Returns the value of the row at the specified offset above (before) the current row of the partition.</returns>
        [FuncConverter]
        public static T Lag<T>(T column, object offset, T @default) => InvalitContext.Throw<T>(nameof(Lag));

        /// <summary>
        /// ROW_NUMBER function.
        /// </summary>
        /// <returns>Row number.</returns>
        [FuncConverter]
        public static int Row_Number() => InvalitContext.Throw<int>(nameof(Row_Number));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="preceding">Preceding row count.</param>
        [RowsConverter]
        public static IRows Rows(long preceding) => InvalitContext.Throw<IRows>(nameof(Rows));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="preceding">Preceding row count.</param>
        /// <param name="following">Following row count.</param>
        [RowsConverter]
        public static IRows Rows(long preceding, long following) => InvalitContext.Throw<IRows>(nameof(Rows));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="columns">Specify column or expression.</param>
        [PartitionByConverter]
        public static IPartitionBy PartitionBy(params object[] columns) => InvalitContext.Throw<IPartitionBy>(nameof(PartitionBy));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [OverConverter]
        public static T Over<T>(this T before, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [OverConverter]
        public static T Over<T>(this T before, IPartitionBy partitionBy, IOrderBy orderBy) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [OverConverter]
        public static T Over<T>(this T before, IPartitionBy partitionBy, IRows rows) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">Getting row order.</param>
        /// <returns>It is the result of Window function.</returns>
        [OverConverter]
        public static T Over<T>(this T before, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [OverConverter]
        public static T Over<T>(this T before, IPartitionBy partitionBy) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <returns>It is the result of Window function.</returns>
        [OverConverter]
        public static T Over<T>(this T before, IOrderBy orderBy) => InvalitContext.Throw<T>(nameof(Over));

        /// <summary>
        /// OVER function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="before">It is an before expression in the Windwo function.</param>
        /// <param name="rows">Getting row order.</param>
        /// <returns>It is the result of Window function.</returns>
        [OverConverter]
        public static T Over<T>(this T before, IRows rows) => InvalitContext.Throw<T>(nameof(Over));
    }
}
