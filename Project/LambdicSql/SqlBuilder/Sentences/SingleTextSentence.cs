using System.Linq;

namespace LambdicSql.SqlBuilder.Sentences
{
    /// <summary>
    /// Single text.
    /// </summary>
    public class SingleTextSentence : Sentence
    {
        string _text;
        int _indent;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Text.</param>
        public SingleTextSentence(string text)
        {
            _text = text;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="indent">Indent.</param>
        public SingleTextSentence(string text, int indent)
        {
            _text = text;
            _indent = indent;
        }

        /// <summary>
        /// Is single line.
        /// </summary>
        public override bool IsSingleLine(SqlBuildingContext context) => true;

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
        public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
            => string.Join(string.Empty, Enumerable.Range(0, _indent + indent).Select(e => "\t").ToArray()) + _text;

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override Sentence ConcatAround(string front, string back) => new SingleTextSentence(front + _text + back, _indent);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override Sentence ConcatToFront(string front) => new SingleTextSentence(front + _text, _indent);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override Sentence ConcatToBack(string back) => new SingleTextSentence(_text + back, _indent);

        /// <summary>
        /// Customize.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        public override Sentence Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
    }
}
