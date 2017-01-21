using LambdicSql.BuilderServices.Inside;

namespace LambdicSql.BuilderServices.CodeParts
{
    /// <summary>
    /// Single text code.
    /// </summary>
    public class SingleTextCode : ICode
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
        /// Is empty.
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(_text);

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Text.</returns>
        public string ToString(BuildingContext context) => PartsUtils.GetIndent(_indent + context.Indent) + _text;

        /// <summary>
        /// Accept customizer.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Destination.</returns>
        public ICode Accept(ICodeCustomizer customizer) => customizer.Visit(this);

        /// <summary>
        /// Is single line.
        /// </summary>
        public bool IsSingleLine(BuildingContext context) => true;
    }
}
