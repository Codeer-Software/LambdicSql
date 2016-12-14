using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Text.
    /// </summary>
    public abstract class TextParts
    {
        /// <summary>
        /// Is single line.
        /// </summary>
        public abstract bool IsSingleLine { get; }

        /// <summary>
        /// Is empty.
        /// </summary>
        public abstract bool IsEmpty { get; }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public abstract string ToString(int indent);

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public abstract TextParts ConcatAround(string front, string back);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public abstract TextParts ConcatToFront(string front);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public abstract TextParts ConcatToBack(string back);

        /// <summary>
        /// Convert string to IText.
        /// </summary>
        /// <param name="text">string.</param>
        public static implicit operator TextParts (string text) => new SingleText(text);
    }
}
