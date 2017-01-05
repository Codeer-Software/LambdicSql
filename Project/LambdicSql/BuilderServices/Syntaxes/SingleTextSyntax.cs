using LambdicSql.BuilderServices.Syntaxes.Inside;

namespace LambdicSql.BuilderServices.Syntaxes
{
    /// <summary>
    /// Single text.
    /// </summary>
    public class SingleTextSyntax : Syntax
    {
        string _text;
        int _indent;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Text.</param>
        public SingleTextSyntax(string text)
        {
            _text = text;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="indent">Indent.</param>
        public SingleTextSyntax(string text, int indent)
        {
            _text = text;
            _indent = indent;
        }

        /// <summary>
        /// Is single line.
        /// </summary>
        public override bool IsSingleLine(BuildingContext context) => true;

        /// <summary>
        /// Is empty.
        /// </summary>
        public override bool IsEmpty => string.IsNullOrEmpty(_text.Trim());

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="isTopLevel">Is top level.</param>
        /// <param name="indent">Indent.</param>
        /// <param name="context">Context.</param>
        /// <returns>Text.</returns>
        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => SyntaxUtils.GetIndent(_indent + indent) + _text;

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override Syntax ConcatAround(string front, string back) => new SingleTextSyntax(front + _text + back, _indent);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override Syntax ConcatToFront(string front) => new SingleTextSyntax(front + _text, _indent);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override Syntax ConcatToBack(string back) => new SingleTextSyntax(_text + back, _indent);

        /// <summary>
        /// Customize.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        public override Syntax Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
    }
}
