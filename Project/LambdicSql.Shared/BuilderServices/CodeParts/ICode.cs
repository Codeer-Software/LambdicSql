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
        /// <param name="context">Context.</param>
        /// <returns>Is single line.</returns>
        bool IsSingleLine(BuildingContext context);

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Text.</returns>
        string ToString(BuildingContext context);

        /// <summary>
        /// Accept customizer.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        ICode Accept(ICodeCustomizer customizer);
    }
}
