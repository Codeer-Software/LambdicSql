using LambdicSql.SqlBuilder.Sentences;

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
        Sentence Custom(Sentence src);
    }
}
