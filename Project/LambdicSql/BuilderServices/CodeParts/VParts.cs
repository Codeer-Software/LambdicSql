using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.BuilderServices.CodeParts
{
    /// <summary>
    /// Vertical text.
    /// </summary>
    public class VParts : Parts
    {
        List<Parts> _texts = new List<Parts>();

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
        public override bool IsSingleLine(BuildingContext context) => _texts.Count <= 1 && !_texts.Any(e => !e.IsSingleLine(context));

        /// <summary>
        /// Is empty.
        /// </summary>
        public override bool IsEmpty => !_texts.Any(e => !e.IsEmpty);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Vertical texts.</param>
        public VParts(params Parts[] texts)
        {
            _texts.AddRange(texts.Where(e => !e.IsEmpty));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Vertical texts.</param>
        public VParts(IEnumerable<Parts> texts)
        {
            _texts.AddRange(texts.Where(e => !e.IsEmpty));
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="isTopLevel">Is top level.</param>
        /// <param name="indent">Indent.</param>
        /// <param name="context">Context.</param>
        /// <returns>Text.</returns>
        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            => string.Join(Separator + Environment.NewLine, _texts.Select(e => e.ToString(isTopLevel, Indent + indent, context)).ToArray());

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="indent">Indent.</param>
        public void Add(string text, int indent)
        {
            if (string.IsNullOrEmpty(text.Trim())) return;
            _texts.Add(new SingleTextParts(text, indent));
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void Add(Parts text)
        {
            if (text.IsEmpty) return;
            _texts.Add(text);
        }

        /// <summary>
        /// Add texts.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <param name="texts">Texts.</param>
        public void AddRange(int indent, IEnumerable<Parts> texts)
            => _texts.AddRange(texts.Where(e => !e.IsEmpty).Select(e => new HParts(e) { Indent = 1 }).Cast<Parts>());

        /// <summary>
        /// Add texts.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <param name="texts">Texts.</param>
        public void AddRange(int indent, params Parts[] texts)
            => _texts.AddRange(texts.Where(e => !e.IsEmpty).Select(e => new HParts(e) { Indent = 1 }).Cast<Parts>());

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override Parts ConcatAround(string front, string back)
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
        public override Parts ConcatToFront(string front)
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
        public override Parts ConcatToBack(string back)
        {
            if (_texts.Count == 0) return CopyProperty(back);

            var dst = _texts.ToArray();
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return CopyProperty(dst);
        }

        /// <summary>
        /// Customize.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        public override Parts Customize(IPartsCustomizer customizer)
        {
            var dst = _texts.Select(e => e.Customize(customizer));
            return CopyProperty(dst.ToArray());
        }

        VParts CopyProperty(params Parts[] texts)
             => new VParts(texts) { Indent = Indent, Separator = Separator };
    }
}
