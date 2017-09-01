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
        public static object ToSqlObject(this string text, params object[] args) { throw new InvalitContextException(nameof(ToSqlObject)); }

        /// <summary>
        /// Put the text in the expression of LamblicSql.
        /// You can use TwoWaySql text format.
        /// Replace /*no*/.../**/ by arguments.
        /// It's ...
        /// <para>TwoWaySql(@"</para>
        /// <para>SELECT money + /*0*/1000/**/</para>
        /// <para>FROM tbl_remuneration</para>
        /// <para>WHERE /*1*/id = 1/**/",</para>
        /// <para>1000, tbl_remuneration.staff_id == 10)</para>
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>LamblicSql's expression.</returns>
        [TwoWaySqlConverter]
        public static object TwoWaySql(this string text, params object[] args) { throw new InvalitContextException(nameof(TwoWaySql)); }

        /// <summary>
        /// Embed values directly into SQL without parameterization.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="value">Value.</param>
        /// <returns>Direct value.</returns>
        [MethodFormatConverter(Format = "[$0]")]
        public static T DirectValue<T>(this T value) { throw new InvalitContextException(nameof(DirectValue)); }

        /// <summary>
        /// It use the variable name.
        /// </summary>
        /// <typeparam name="T">Variable type.</typeparam>
        /// <param name="variable">Variable.</param>
        /// <returns>Variable Name.</returns>
        [MethodFormatConverter(Format = "[*0]")]
        public static T VariableName<T>(this T variable)=> throw new InvalitContextException(nameof(VariableName));

        /// <summary>
        /// Specifies that the argument is a special character and is to be converted with intact characters without ''.
        /// </summary>
        /// <param name="specialName">Special name. for example table name, db name.</param>
        /// <returns>Special name.</returns>
        [MethodFormatConverter(Format = "[!0]")]
        public static string SpecialName(this string specialName) => throw new InvalitContextException(nameof(SpecialName));

        /// <summary>
        /// Schema and table names are omitted and only columns are used.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="column">Column.</param>
        /// <returns>Column only.</returns>
        [MethodFormatConverter(Format = "[#0]")]
        public static T ColumnOnly<T>(this T column) { throw new InvalitContextException(nameof(ColumnOnly)); }

        /// <summary>
        /// operator helper.
        /// </summary>
        /// <param name="lhs">lhs.</param>
        /// <param name="rhs">rhs.</param>
        /// <returns>bool.</returns>
        [MethodFormatConverter(Format = "[0] >= [1]")]
        public static bool GreaterThanOrEqual(this object lhs, object rhs) { throw new InvalitContextException("operator >="); }

        /// <summary>
        /// operator helper.
        /// </summary>
        /// <param name="lhs">lhs.</param>
        /// <param name="rhs">rhs.</param>
        /// <returns>bool.</returns>
        [MethodFormatConverter(Format = "[0] <= [1]")]
        public static bool LessThanOrEqual(this object lhs, object rhs) { throw new InvalitContextException("operator <="); }

        /// <summary>
        /// operator helper.
        /// </summary>
        /// <param name="lhs">lhs.</param>
        /// <param name="rhs">rhs.</param>
        /// <returns>bool.</returns>
        [MethodFormatConverter(Format = "[0] > [1]")]
        public static bool GreaterThan(this object lhs, object rhs) { throw new InvalitContextException("operator >"); }

        /// <summary>
        /// operator helper.
        /// </summary>
        /// <param name="lhs">lhs.</param>
        /// <param name="rhs">rhs.</param>
        /// <returns>bool.</returns>
        [MethodFormatConverter(Format = "[0] < [1]")]
        public static bool LessThan(this object lhs, object rhs) { throw new InvalitContextException("operator <"); }
    }
}
