namespace LambdicSql.BuilderServices.Syntaxes
{
    /// <summary>
    /// Text.
    /// </summary>
    public abstract class TextPartsBase
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
        public abstract TextPartsBase ConcatAround(string front, string back);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public abstract TextPartsBase ConcatToFront(string front);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public abstract TextPartsBase ConcatToBack(string back);

        /// <summary>
        /// Customize.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        public abstract TextPartsBase Customize(ISyntaxCustomizer customizer);

        /// <summary>
        /// Convert string to IText.
        /// </summary>
        /// <param name="text">string.</param>
        public static implicit operator TextPartsBase(string text) => new SingleTextSyntax(text);
    }
}
