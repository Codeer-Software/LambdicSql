using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.BuilderServices.CodeParts
{
    /// <summary>
    /// Vertical text.
    /// </summary>
    public class VCode : ICode
    {
        List<ICode> _texts = new List<ICode>();

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
        public bool IsSingleLine(BuildingContext context) => _texts.Count <= 1 && !_texts.Any(e => !e.IsSingleLine(context));

        /// <summary>
        /// Is empty.
        /// </summary>
        public bool IsEmpty => !_texts.Any(e => !e.IsEmpty);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Vertical texts.</param>
        public VCode(params ICode[] texts)
        {
            _texts.AddRange(texts.Where(e => !e.IsEmpty));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Vertical texts.</param>
        public VCode(IEnumerable<ICode> texts)
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
        public string ToString(bool isTopLevel, int indent, BuildingContext context)
            => string.Join(Separator + Environment.NewLine, _texts.Select(e => e.ToString(isTopLevel, Indent + indent, context).TrimEnd()).ToArray());

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="indent">Indent.</param>
        public void Add(string text, int indent)
        {
            if (string.IsNullOrEmpty(text.Trim())) return;
            _texts.Add(new SingleTextCode(text, indent));
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void Add(ICode text)
        {
            if (text.IsEmpty) return;
            _texts.Add(text);
        }

        /// <summary>
        /// Add texts.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <param name="texts">Texts.</param>
        public void AddRange(int indent, IEnumerable<ICode> texts)
            => _texts.AddRange(texts.Where(e => !e.IsEmpty).Select(e => new HCode(e) { Indent = 1 }).Cast<ICode>());

        /// <summary>
        /// Add texts.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <param name="texts">Texts.</param>
        public void AddRange(int indent, params ICode[] texts)
            => _texts.AddRange(texts.Where(e => !e.IsEmpty).Select(e => new HCode(e) { Indent = 1 }).Cast<ICode>());

        /// <summary>
        /// Customize.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        public ICode Customize(ICodeCustomizer customizer)
        {
            var dst = _texts.Select(e => e.Customize(customizer));
            return CopyProperty(dst.ToArray());
        }

        VCode CopyProperty(params ICode[] texts)
             => new VCode(texts) { Indent = Indent, Separator = Separator };
    }
}
