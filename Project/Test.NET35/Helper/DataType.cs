using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;

namespace LambdicSql
{
    /// <summary>
    /// Data type.
    /// It can only be used within lambda of the LambdicSql.
    /// </summary>
    public static class DataType
    {
        /// <summary>
        /// INT
        /// </summary>
        /// <returns>INT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Int() { throw new InvalitContextException(nameof(Int)); }

        /// <summary>
        /// CHAR
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>CHAR</returns>
        [FuncStyleConverter]
        public static DataTypeElement Char(int n) { throw new InvalitContextException(nameof(Char)); }
    }
}
