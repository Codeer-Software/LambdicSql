namespace LambdicSql.BuilderServices.CodeParts
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
        Parts Custom(Parts src);
    }
}
