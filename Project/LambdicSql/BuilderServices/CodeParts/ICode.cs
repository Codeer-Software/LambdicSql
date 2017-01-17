namespace LambdicSql.BuilderServices.CodeParts
{
    /// <summary>
    /// Sql code.
    /// </summary>
    public interface ICode
    {
        /// <summary>
        /// Is empty.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Is single line.
        /// </summary>
        bool IsSingleLine(BuildingContext context);

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Text.</returns>
        string ToString(BuildingContext context);

        /// <summary>
        /// Customize.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        ICode Customize(ICodeCustomizer customizer);
    }
}
