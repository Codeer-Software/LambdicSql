using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Vertical text.
    /// </summary>
    public class VText : TextParts
    {
        List<TextParts> _texts = new List<TextParts>();

        /// <summary>
        /// Separator
        /// </summary>
        public string Separator { get; set; }

        /// <summary>
        /// Indent
        /// </summary>
        public int Indent { get; set; }

        /// <summary>
        /// Is single line.
        /// </summary>
        public override bool IsSingleLine => _texts.Count <= 1 && !_texts.Any(e => !e.IsSingleLine);

        /// <summary>
        /// Is empty.
        /// </summary>
        public override bool IsEmpty => !_texts.Any(e => !e.IsEmpty);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Vertical texts.</param>
        public VText(params TextParts[] texts)
        {
            _texts.AddRange(texts.Where(e => !e.IsEmpty));
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public override string ToString(int indent)
            => string.Join(Separator + Environment.NewLine, _texts.Select(e => e.ToString(Indent + indent)).ToArray());

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="indent">Indent.</param>
        public void Add(string text, int indent)
        {
            if (string.IsNullOrEmpty(text.Trim())) return;
            _texts.Add(new SingleText(text, indent));
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void Add(TextParts text)
        {
            if (text.IsEmpty) return;
            _texts.Add(text);
        }

        /// <summary>
        /// Add texts.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <param name="texts">Texts.</param>
        public void AddRange(int indent, IEnumerable<TextParts> texts)
            => _texts.AddRange(texts.Where(e => !e.IsEmpty).Select(e => new HText(e) { Indent = 1 }).Cast<TextParts>());

        /// <summary>
        /// Add texts.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <param name="texts">Texts.</param>
        public void AddRange(int indent, params TextParts[] texts)
            => _texts.AddRange(texts.Where(e => !e.IsEmpty).Select(e => new HText(e) { Indent = 1 }).Cast<TextParts>());

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override TextParts ConcatAround(string front, string back)
        {
            if (_texts.Count == 0) return CopyProperty(front + back);

            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return CopyProperty(dst);
        }

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override TextParts ConcatToFront(string front)
        {
            if (_texts.Count == 0) return CopyProperty(front);

            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            return CopyProperty(dst);
        }

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns>Text.</returns>
        public override TextParts ConcatToBack(string back)
        {
            if (_texts.Count == 0) return CopyProperty(back);

            var dst = _texts.ToArray();
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return CopyProperty(dst);
        }

        VText CopyProperty(params TextParts[] texts)
             => new VText(texts) { Indent = Indent, Separator = Separator };
    }
}
