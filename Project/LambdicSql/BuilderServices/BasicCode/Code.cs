namespace LambdicSql.BuilderServices.BasicCode
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
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public abstract Code ConcatAround(string front, string back);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public abstract Code ConcatToFront(string front);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public abstract Code ConcatToBack(string back);

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
