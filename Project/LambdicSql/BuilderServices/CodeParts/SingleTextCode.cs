using LambdicSql.BuilderServices.Inside;

namespace LambdicSql.BuilderServices.CodeParts
{
    /// <summary>
    /// Single text.
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
        /// Is single line.
        /// </summary>
        public bool IsSingleLine(BuildingContext context) => true;

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
        /// Customize.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        public ICode Customize(ICodeCustomizer customizer) => customizer.Custom(this);
    }
}
