using LambdicSql.BuilderServices.Inside;

namespace LambdicSql.BuilderServices.BasicCode
{
    /// <summary>
    /// Single text.
    /// </summary>
    public class SingleTextCode : Code
    {
        string _text;
        int _indent;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Text.</param>
        public SingleTextCode(string text)
        {
            _text = text;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="indent">Indent.</param>
        public SingleTextCode(string text, int indent)
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
        public override bool IsEmpty => string.IsNullOrEmpty(_text);

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="isTopLevel">Is top level.</param>
        /// <param name="indent">Indent.</param>
        /// <param name="context">Context.</param>
        /// <returns>Text.</returns>
        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => PartsUtils.GetIndent(_indent + indent) + _text;

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override Code ConcatAround(string front, string back) => new SingleTextCode(front + _text + back, _indent);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override Code ConcatToFront(string front) => new SingleTextCode(front + _text, _indent);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override Code ConcatToBack(string back) => new SingleTextCode(_text + back, _indent);

        /// <summary>
        /// Customize.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        public override Code Customize(ICodeCustomizer customizer) => customizer.Custom(this);
    }
}
