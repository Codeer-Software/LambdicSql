namespace LambdicSql.BuilderServices.Parts
{
    /// <summary>
    /// Sql code parts.
    /// </summary>
    public abstract class CodeParts
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
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public abstract CodeParts ConcatAround(string front, string back);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public abstract CodeParts ConcatToFront(string front);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public abstract CodeParts ConcatToBack(string back);

        /// <summary>
        /// Customize.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        public abstract CodeParts Customize(IPartsCustomizer customizer);

        /// <summary>
        /// Convert string to IText.
        /// </summary>
        /// <param name="text">string.</param>
        public static implicit operator CodeParts(string text) => new SingleTextParts(text);
    }
}
