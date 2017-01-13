using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.SymbolConverters;
using System;

namespace LambdicSql
{
    /// <summary>
    /// SQL Symbols.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// Use[using static LambdicSql.Keywords;], you can use to write natural SQL.
    /// </summary>
    public static partial class Symbol
    {
        /// <summary>
        /// It's *.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="selected">The type you want to obtain with the SELECT clause. Usually you specify a table element.</param>
        /// <returns>*</returns>
        [ClauseStyleConverter(Name = "*")]
        public static IAsterisk<TSelected> Asterisk<TSelected>(TSelected selected) => InvalitContext.Throw<IAsterisk<TSelected>>(nameof(Asterisk));

        /// <summary>
        /// It's *.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <returns>*</returns>
        [ClauseStyleConverter(Name = "*")]
        public static IAsterisk<TSelected> Asterisk<TSelected>() => InvalitContext.Throw<IAsterisk<TSelected>>(nameof(Asterisk));

        /// <summary>
        /// It's *.
        /// </summary>
        /// <returns>*</returns>
        [ClauseStyleConverter(Name = "*")]
        public static IAsterisk Asterisk() => InvalitContext.Throw<IAsterisk>(nameof(Asterisk));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="target">target column.</param>
        [MethodFormatConverter(Format = "[0] ASC")]
        public static ISortedBy Asc(object target) => InvalitContext.Throw<ISortedBy>(nameof(Asc));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="target">target column.</param>
        [MethodFormatConverter(Format = "[0] DESC")]
        public static ISortedBy Desc(object target) => InvalitContext.Throw<ISortedBy>(nameof(Desc));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="count">cout.</param>
        [MethodFormatConverter(Format = "TOP [$0]")]
        public static ITop Top(long count) => InvalitContext.Throw<ITop>(nameof(Top));

        /// <summary>
        /// ROWNUM BETWEEN keyword.
        /// </summary>
        [ClauseStyleConverter]
        public static object RowNum() => InvalitContext.Throw<Non>(nameof(RowNum));

        /// <summary>
        /// CURREN_TDATE function.
        /// </summary>
        /// <returns>Date of executing SQL.</returns>
        [CurrentDateTimeConverter(Name = "DATE")]
        public static DateTime Current_Date() => InvalitContext.Throw<DateTime>(nameof(Current_Date));

        /// <summary>
        /// CURRENT_TIME function.
        /// </summary>
        /// <returns>Date of executing SQL.</returns>
        [CurrentDateTimeConverter(Name = "TIME")]
        public static TimeSpan Current_Time() => InvalitContext.Throw<TimeSpan>(nameof(DateTimeOffset));

        /// <summary>
        /// CURRENT_TIMESTAMP function.
        /// </summary>
        /// <returns>Date and time of executing SQL.</returns>
        [CurrentDateTimeConverter(Name = "TIMESTAMP")]
        public static DateTime Current_TimeStamp() => InvalitContext.Throw<DateTime>(nameof(Current_TimeStamp));

        /// <summary>
        /// DUAL keyword.
        /// </summary>
        [MemberConverter]
        public static object Dual => InvalitContext.Throw<Non>(nameof(Dual));
    }
}
