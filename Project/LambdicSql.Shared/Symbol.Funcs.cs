using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using System;

namespace LambdicSql
{
    /// <summary>
    /// SQL Symbols.
    /// It can only be used within methods of the LambdicSql.Db class.
    /// Use[using static LambdicSql.Keywords;], you can use to write natural SQL.
    /// </summary>
    public static partial class Symbol
    {
        /// <summary>
        /// SUM function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Total.</returns>
        [FuncStyleConverter]
        public static T Sum<T>(T column) { throw new InvalitContextException(nameof(Sum)); }

        /// <summary>
        /// SUM function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Total.</returns>
        [MethodFormatConverter(Format = "SUM(|[0] [1])")]
        public static T Sum<T>(AggregatePredicateElement aggregatePredicate, T column) { throw new InvalitContextException(nameof(Sum)); }

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Count.</returns>
        [FuncStyleConverter]
        public static int Count(object column) { throw new InvalitContextException(nameof(Count)); }

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="asterisk">*</param>
        /// <returns>Count.</returns>
        [FuncStyleConverter]
        public static int Count(AsteriskElement asterisk) { throw new InvalitContextException(nameof(Count)); }

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Count.</returns>
        [MethodFormatConverter(Format = "COUNT(|[0] [1])")]
        public static int Count(AggregatePredicateElement aggregatePredicate, object column) { throw new InvalitContextException(nameof(Count)); }

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="asterisk">*</param>
        /// <returns>Count.</returns>
        [MethodFormatConverter(Format = "COUNT(|[0] [1])")]
        public static int Count(AggregatePredicateElement aggregatePredicate, AsteriskElement asterisk) { throw new InvalitContextException(nameof(Count)); }

        /// <summary>
        /// AVG function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Average.</returns>
        [FuncStyleConverter]
        public static double Avg(object column) { throw new InvalitContextException(nameof(Avg)); }

        /// <summary>
        /// MIN function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Minimum.</returns>
        [FuncStyleConverter]
        public static T Min<T>(T column) { throw new InvalitContextException(nameof(Min)); }

        /// <summary>
        /// MAX function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Maximum.</returns>
        [FuncStyleConverter]
        public static T Max<T>(T column) { throw new InvalitContextException(nameof(Max)); }

        /// <summary>
        /// ABS function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Absolute value.</returns>
        [FuncStyleConverter]
        public static T Abs<T>(T column) { throw new InvalitContextException(nameof(Abs)); }

        /// <summary>
        /// MOD function.
        /// </summary>
        /// <typeparam name="T1">Type represented by target</typeparam>
        /// <typeparam name="T2">Type represented by div</typeparam>
        /// <param name="target">Numeric expression to divide.</param>
        /// <param name="div">A numeric expression that divides the dividend.</param>
        /// <returns>Surplus.</returns>
        [FuncStyleConverter]
        public static T1 Mod<T1, T2>(T1 target, T2 div) { throw new InvalitContextException(nameof(Mod)); }

        /// <summary>
        /// ROUND function.
        /// </summary>
        /// <typeparam name="T1">Type represented by target.</typeparam>
        /// <typeparam name="T2">Type represented by digit</typeparam>
        /// <param name="target">Numeric expression to round.</param>
        /// <param name="digit">Is the precision to which it is to be rounded.</param>
        /// <returns>Rounded result.</returns>
        [FuncStyleConverter]
        public static T1 Round<T1, T2>(T1 target, T2 digit) { throw new InvalitContextException(nameof(Round)); }

        /// <summary>
        /// CONCAT function.
        /// </summary>
        /// <param name="targets">A string value to concatenate to the other values.</param>
        /// <returns>concatenated result.</returns>
        [FuncStyleConverter]
        public static string Concat(params string[] targets) { throw new InvalitContextException(nameof(Concat)); }

        /// <summary>
        /// LENGTH function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>String length.</returns>
        [FuncStyleConverter]
        public static int Length(object target) { throw new InvalitContextException(nameof(Length)); }

        /// <summary>
        /// LEN function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>String length.</returns>
        [FuncStyleConverter]
        public static int Len(object target) { throw new InvalitContextException(nameof(Len)); }

        /// <summary>
        /// LOWER function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>Changed string.</returns>
        [FuncStyleConverter]
        public static string Lower(object target) { throw new InvalitContextException(nameof(Lower)); }

        /// <summary>
        /// UPPER function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>Changed string.</returns>
        [FuncStyleConverter]
        public static string Upper(object target) { throw new InvalitContextException(nameof(Upper)); }

        /// <summary>
        /// REPLACE function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <param name="src">source.</param>
        /// <param name="dst">destination.</param>
        /// <returns>Changed string.</returns>
        [FuncStyleConverter]
        public static string Replace(object target, object src, object dst) { throw new InvalitContextException(nameof(Replace)); }

        /// <summary>
        /// SUBSTRING function.
        /// </summary>
        /// <param name="target">target.</param>
        /// <param name="startIndex">Specify the starting position of the character string to be acquired.</param>
        /// <param name="length">Specify the length of the string to be retrieved.</param>
        /// <returns>Part of a text.</returns>
        [FuncStyleConverter]
        public static string Substring(object target, object startIndex, object length) { throw new InvalitContextException(nameof(Substring)); }

        /// <summary>
        /// EXTRACT function.
        /// </summary>
        /// <param name="element">Part type.</param>
        /// <param name="src">The date data.</param>
        /// <returns>A part from the date data.</returns>
        [MethodFormatConverter(Format = "EXTRACT(|[0] FROM [1])")]
        public static double Extract(DateTimeElement element, DateTime src) { throw new InvalitContextException(nameof(Extract)); }

        /// <summary>
        /// DATEPART function.
        /// </summary>
        /// <param name="element">Part type.</param>
        /// <param name="src">The date data.</param>
        /// <returns>A part from the date data.</returns>
        [FuncStyleConverter]
        public static int DatePart(DateTimeElement element, DateTime src) { throw new InvalitContextException(nameof(Extract)); }

        /// <summary>
        /// CAST function.
        /// </summary>
        /// <typeparam name="TDst">Type of destination.</typeparam>
        /// <param name="target"></param>
        /// <param name="destinationType">Type of destination.</param>
        /// <returns>Converted data.</returns>
        [MethodFormatConverter(Format = "CAST(|[0] AS [1])")]
        public static TDst Cast<TDst>(object target, DataTypeElement destinationType) { throw new InvalitContextException(nameof(Cast)); }

        /// <summary>
        /// COALESCE function.
        /// </summary>
        /// <typeparam name="T">Type of parameter</typeparam>
        /// <param name="parameter">Parameter.</param>
        /// <returns>The first non-null value in the parameter.</returns>
        [MethodFormatConverter(Format = "COALESCE(|[<, >0])")]
        public static T Coalesce<T>(params T[] parameter) { throw new InvalitContextException(nameof(Coalesce)); }

        /// <summary>
        /// NVL function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="expression1">expression.</param>
        /// <param name="expression2">expression.</param>
        /// <returns>expression1 or expression2.</returns>
        [FuncStyleConverter]
        public static T NVL<T>(T expression1, T expression2) { throw new InvalitContextException(nameof(NVL)); }

        /// <summary>
        /// FIRST_VALUE function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>For each group, the value of the first row of the sorted record.</returns>
        [FuncStyleConverter]
        public static T First_Value<T>(T column) { throw new InvalitContextException(nameof(First_Value)); }

        /// <summary>
        /// LAST_VALUE function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>For each group, the value at the end of the sorted record.</returns>
        [FuncStyleConverter]
        public static T Last_Value<T>(T column) { throw new InvalitContextException(nameof(Last_Value)); }

        /// <summary>
        /// RANK function.
        /// </summary>
        /// <returns>Ranked value.</returns>
        [FuncStyleConverter]
        public static int Rank() { throw new InvalitContextException(nameof(Rank)); }

        /// <summary>
        /// DENSE_RANK function.
        /// </summary>
        /// <returns>Ranked value.</returns>
        [FuncStyleConverter]
        public static int Dense_Rank() { throw new InvalitContextException(nameof(Dense_Rank)); }

        /// <summary>
        /// PERCENT_RANK function.
        /// </summary>
        /// <returns>Ranked value.</returns>
        [FuncStyleConverter]
        public static double Percent_Rank() { throw new InvalitContextException(nameof(Percent_Rank)); }

        /// <summary>
        /// CUME_DIST function.
        /// </summary>
        /// <returns>Cumulative distribution of values in group.</returns>
        [FuncStyleConverter]
        public static double Cume_Dist() { throw new InvalitContextException(nameof(Cume_Dist)); }

        /// <summary>
        /// NTILE function.
        /// </summary>
        /// <param name="groupCount">The number of ranking groups.</param>
        /// <returns>For each row, NTILE returns the number of the group to which the row belongs.</returns>
        [FuncStyleConverter]
        public static int Ntile(object groupCount) { throw new InvalitContextException(nameof(Ntile)); }

        /// <summary>
        /// NTH_VALUE function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">Specify the number of lines associated with the first line of the window that returns the expression.</param>
        /// <returns>Returns the value of the expression in the specified row of the window frame associated with the first line of the window.</returns>
        [FuncStyleConverter]
        public static int Nth_Value(object column, object offset) { throw new InvalitContextException(nameof(Nth_Value)); }

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <returns>Returns the value of the row at the specified offset above (before) the current row of the partition.</returns>
        [FuncStyleConverter]
        public static T Lag<T>(T column) { throw new InvalitContextException(nameof(Lag)); }

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">An optional parameter that specifies the number of lines before the current line that returns a value.</param>
        /// <returns>Returns the value of the row at the specified offset above (before) the current row of the partition.</returns>
        [FuncStyleConverter]
        public static T Lag<T>(T column, object offset) { throw new InvalitContextException(nameof(Lag)); }

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">An optional parameter that specifies the number of lines before the current line that returns a value.</param>
        /// <param name="default">The value returned if the value specified by offset is NULL.</param>
        /// <returns>Returns the value of the row at the specified offset above (before) the current row of the partition.</returns>
        [FuncStyleConverter]
        public static T Lag<T>(T column, object offset, T @default) { throw new InvalitContextException(nameof(Lag)); }

        /// <summary>
        /// ROW_NUMBER function.
        /// </summary>
        /// <returns>Row number.</returns>
        [FuncStyleConverter]
        public static int Row_Number() { throw new InvalitContextException(nameof(Row_Number)); }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="preceding">Preceding row count.</param>
        [MethodFormatConverter(Format = "ROWS [$0] PRECEDING")]
        public static OverArgument Rows(long preceding) { throw new InvalitContextException(nameof(Rows)); }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="preceding">Preceding row count.</param>
        /// <param name="following">Following row count.</param>
        [MethodFormatConverter(Format = "ROWS BETWEEN [$0] PRECEDING AND [$1] FOLLOWING")]
        public static OverArgument Rows(long preceding, long following) { throw new InvalitContextException(nameof(Rows)); }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="columns">Specify column or expression.</param>
        [MethodFormatConverter(Format = "PARTITION BY|[<,>0]", FormatDirection = FormatDirection.Vertical)]
        public static OverArgument PartitionBy(params object[] columns) { throw new InvalitContextException(nameof(PartitionBy)); }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="before"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        [MethodFormatConverter(Format = "OVER(|[< >1])", FormatDirection = FormatDirection.Vertical)]
        public static T Over<T>(this T before, params OverArgument[] args) { throw new InvalitContextException(nameof(Over)); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [MethodFormatConverter(Format = "OVER(|[< >0])", FormatDirection = FormatDirection.Vertical)]
        public static OverReturnValue Over(params OverArgument[] args) { throw new InvalitContextException(nameof(Over)); }
    }
}
