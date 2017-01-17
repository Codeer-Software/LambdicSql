namespace LambdicSql.BuilderServices.CodeParts
{
    /// <summary>
    /// Customizer.
    /// </summary>
    public interface ICodeCustomizer
    {
        /// <summary>
        /// Visit and customize.
        /// </summary>
        /// <param name="src">Source.</param>
        /// <returns>Destination.</returns>
        ICode Visit(ICode src);
    }
}
