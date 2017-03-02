using LambdicSql;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using System;

namespace Test
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
        /// Constructor.
        /// </summary>
        /// <param name="preceding">Preceding row count.</param>
        [MethodFormatConverter(Format = "ROWS [$0] PRECEDING")]
        public static OverElement Rows(long preceding) { throw new InvalitContextException(nameof(Rows)); }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="preceding">Preceding row count.</param>
        /// <param name="following">Following row count.</param>
        [MethodFormatConverter(Format = "ROWS BETWEEN [$0] PRECEDING AND [$1] FOLLOWING")]
        public static OverElement Rows(long preceding, long following) { throw new InvalitContextException(nameof(Rows)); }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="columns">Specify column or expression.</param>
        [MethodFormatConverter(Format = "PARTITION BY|[<,>0]", FormatDirection = FormatDirection.Vertical)]
        public static OverElement PartitionBy(params object[] columns) { throw new InvalitContextException(nameof(PartitionBy)); }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="before"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        [MethodFormatConverter(Format = "OVER(|[< >1])", FormatDirection = FormatDirection.Vertical)]
        public static T Over<T>(this T before, params OverElement[] args) { throw new InvalitContextException(nameof(Over)); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [MethodFormatConverter(Format = "OVER(|[< >0])", FormatDirection = FormatDirection.Vertical)]
        public static OverReturnValue Over(params OverElement[] args) { throw new InvalitContextException(nameof(Over)); }
    }
}
