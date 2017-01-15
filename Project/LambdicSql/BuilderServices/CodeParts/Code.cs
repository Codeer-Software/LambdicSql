namespace LambdicSql.BuilderServices.CodeParts
{
    /// <summary>
    /// Sql code.
    /// </summary>
    public abstract class Code
    {
        /// <summary>
        /// Is empty.
        /// </summary>
        public abstract bool IsEmpty { get; }

        /// <summary>
        /// Is single line.
        /// </summary>
        public abstract bool IsSingleLine(BuildingContext context);

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="isTopLevel">Is top level.</param>
        /// <param name="indent">Indent.</param>
        /// <param name="context">Context.</param>
        /// <returns>Text.</returns>
        public abstract string ToString(bool isTopLevel, int indent, BuildingContext context);

        /// <summary>
        /// Customize.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        public abstract Code Customize(ICodeCustomizer customizer);

        /// <summary>
        /// Convert string to IText.
        /// </summary>
        /// <param name="text">string.</param>
        public static implicit operator Code(string text) => new SingleTextCode(text);
    }
}
