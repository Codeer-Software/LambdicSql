using LambdicSql.SqlBuilder.Parts;

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
        BuildingParts Custom(BuildingParts src);
    }
}
