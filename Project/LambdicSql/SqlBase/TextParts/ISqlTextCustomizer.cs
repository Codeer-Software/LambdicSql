namespace LambdicSql.SqlBase.TextParts
{
    /// <summary>
    /// Customizer.
    /// </summary>
    public interface ISqlTextCustomizer
    {
        /// <summary>
        /// Coustom.
        /// </summary>
        /// <param name="src">Source.</param>
        /// <returns>Result.</returns>
        SqlText Custom(SqlText src);
    }
}
