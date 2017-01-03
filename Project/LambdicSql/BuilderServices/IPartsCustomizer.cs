using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.BuilderServices
{
    /// <summary>
    /// Customizer.
    /// </summary>
    public interface IPartsCustomizer
    {
        /// <summary>
        /// Coustom.
        /// </summary>
        /// <param name="src">Source.</param>
        /// <returns>Result.</returns>
        BuildingParts Custom(BuildingParts src);
    }
}
