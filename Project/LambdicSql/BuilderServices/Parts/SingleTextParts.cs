﻿using LambdicSql.BuilderServices.Inside;

namespace LambdicSql.BuilderServices.Parts
{
    /// <summary>
    /// Single text.
    /// </summary>
    public class SingleTextParts : CodeParts
    {
        string _text;
        int _indent;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Text.</param>
        public SingleTextParts(string text)
        {
            _text = text;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="indent">Indent.</param>
        public SingleTextParts(string text, int indent)
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
        public override CodeParts ConcatAround(string front, string back) => new SingleTextParts(front + _text + back, _indent);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override CodeParts ConcatToFront(string front) => new SingleTextParts(front + _text, _indent);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override CodeParts ConcatToBack(string back) => new SingleTextParts(_text + back, _indent);

        /// <summary>
        /// Customize.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        public override CodeParts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}