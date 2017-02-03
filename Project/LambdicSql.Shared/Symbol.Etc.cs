using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Specialized.SymbolConverters;
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
        /// It's *.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="selected">The type you want to obtain with the SELECT clause. Usually you specify a table element.</param>
        /// <returns>*</returns>
        [ClauseStyleConverter(Name = "*")]
        public static AsteriskElement<TSelected> Asterisk<TSelected>(TSelected selected) { throw new InvalitContextException(nameof(Asterisk)); }

        /// <summary>
        /// It's *.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <returns>*</returns>
        [ClauseStyleConverter(Name = "*")]
        public static AsteriskElement<TSelected> Asterisk<TSelected>() { throw new InvalitContextException(nameof(Asterisk)); }

        /// <summary>
        /// It's *.
        /// </summary>
        /// <returns>*</returns>
        [ClauseStyleConverter(Name = "*")]
        public static AsteriskElement Asterisk() { throw new InvalitContextException(nameof(Asterisk)); }

        /// <summary>
        /// ASC Keyword.
        /// </summary>
        /// <param name="target">target column.</param>
        /// <returns>ASC.</returns>
        [MethodFormatConverter(Format = "[0] ASC")]
        public static OrderByElement Asc(object target) { throw new InvalitContextException(nameof(Asc)); }

        /// <summary>
        /// DESC Keyword.
        /// </summary>
        /// <param name="target">target column.</param>
        /// <returns>DESC.</returns>
        [MethodFormatConverter(Format = "[0] DESC")]
        public static OrderByElement Desc(object target) { throw new InvalitContextException(nameof(Desc)); }

        /// <summary>
        /// TOP Keyword.
        /// </summary>
        /// <param name="count">Count.</param>
        /// <returns>TOP.</returns>
        [MethodFormatConverter(Format = "TOP [$0]")]
        public static TopElement Top(long count) { throw new InvalitContextException(nameof(Top)); }

        /// <summary>
        /// ALL Keyword
        /// </summary>
        /// <returns>ALL.</returns>
        [ClauseStyleConverter]
        public static AggregatePredicateAllElement All() { throw new InvalitContextException(nameof(All)); }

        /// <summary>
        /// DISTINCT Keyword.
        /// </summary>
        /// <returns>DISTINCT.</returns>
        [ClauseStyleConverter]
        public static AggregatePredicateElement Distinct() { throw new InvalitContextException(nameof(All)); }

        /// <summary>
        /// CURREN_TDATE Keyword.
        /// </summary>
        /// <returns>Date of executing SQL.</returns>
        [CurrentDateTimeConverter(Name = "DATE")]
        public static DateTime Current_Date() { throw new InvalitContextException(nameof(Current_Date)); }

        /// <summary>
        /// CURRENT_TIME Keyword.
        /// </summary>
        /// <returns>Date of executing SQL.</returns>
        [CurrentDateTimeConverter(Name = "TIME")]
        public static TimeSpan Current_Time() { throw new InvalitContextException(nameof(DateTimeOffset)); }

        /// <summary>
        /// CURRENT_TIMESTAMP Keyword.
        /// </summary>
        /// <returns>Date and time of executing SQL.</returns>
        [CurrentDateTimeConverter(Name = "TIMESTAMP")]
        public static DateTime Current_TimeStamp() { throw new InvalitContextException(nameof(Current_TimeStamp)); }

        /// <summary>
        /// DUAL keyword.
        /// </summary>
        [MemberConverter]
        public static object Dual { get { throw new InvalitContextException(nameof(Dual)); } }

        /// <summary>
        /// ROWNUM BETWEEN keyword.
        /// </summary>
        [ClauseStyleConverter]
        public static object RowNum() { throw new InvalitContextException(nameof(RowNum)); }
    }
}
