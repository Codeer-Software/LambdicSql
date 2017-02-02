using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Specialized.SymbolConverters;

namespace LambdicSql
{
    /// <summary>
    /// Utility.
    /// It can only be used within methods of the LambdicSql.Db class.
    /// </summary>
    public static class UtilitySymbolExtensions
    {
        /// <summary>
        /// Put the text in the expression of LamblicSql.
        /// </summary>
        /// <param name="text">Text.You can use the same format as System.String's Format method.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>LamblicSql's expression.</returns>
        [ToSqlConverter]
        public static object ToSql(this string text, params object[] args) { throw new InvalitContextException(nameof(ToSql)); }

        /// <summary>
        /// Put the text in the expression of LamblicSql.
        /// You can use TwoWaySql text format.
        /// It's ...
        /// TwoWaySql(@"SELECT money + /*0*/1000/**/ FROM tbl_remuneration WHERE /*1*/tbl_remuneration.money = 100/**/", 1000, tbl_remuneration.staff_id == 10)
        /// Replace /*no*/.../**/ by arguments.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>LamblicSql's expression.</returns>
        [TwoWaySqlConverter]
        public static object TwoWaySql(this string text, params object[] args) { throw new InvalitContextException(nameof(ToSql)); }

        /// <summary>
        /// Embed values directly into SQL without parameterization.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="value">Value.</param>
        /// <returns>Direct value.</returns>
        [MethodFormatConverter(Format = "[$0]")]
        public static T DirectValue<T>(this T value) { throw new InvalitContextException(nameof(DirectValue)); }

        /// <summary>
        /// Schema and table names are omitted and only columns are used.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="column">Column.</param>
        /// <returns>Column only.</returns>
        [MethodFormatConverter(Format = "[#0]")]
        public static T ColumnOnly<T>(this T column) { throw new InvalitContextException(nameof(ColumnOnly)); }
    }
}
