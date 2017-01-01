using LambdicSql.SqlBuilder.ExpressionElements;

namespace LambdicSql.SqlBuilder
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
        ExpressionElement Custom(ExpressionElement src);
    }
}
