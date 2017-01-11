namespace LambdicSql.BuilderServices.Parts
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
        CodeParts Custom(CodeParts src);
    }
}
