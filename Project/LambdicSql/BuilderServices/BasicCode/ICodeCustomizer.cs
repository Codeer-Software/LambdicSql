namespace LambdicSql.BuilderServices.BasicCode
{
    /// <summary>
    /// Customizer.
    /// </summary>
    public interface ICodeCustomizer
    {
        /// <summary>
        /// Coustom.
        /// </summary>
        /// <param name="src">Source.</param>
        /// <returns>Result.</returns>
        Code Custom(Code src);
    }
}
