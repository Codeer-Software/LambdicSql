using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.BuilderServices.CodeParts
{
    /// <summary>
    /// Arrange the code vertically.
    /// </summary>
    public class VCode : ICode
    {
        List<ICode> _core = new List<ICode>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Vertical texts.</param>
        public VCode(params ICode[] texts)
        {
            _core.AddRange(texts.Where(e => !e.IsEmpty));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Vertical texts.</param>
        public VCode(IEnumerable<ICode> texts)
        {
            _core.AddRange(texts.Where(e => !e.IsEmpty));
        }

        /// <summary>
        /// Separator
        /// </summary>
        public string Separator { get; set; }

        /// <summary>
        /// Indent
        /// </summary>
        public int Indent { get; set; }

        /// <summary>
        /// Is empty.
        /// </summary>
        public bool IsEmpty => !_core.Any(e => !e.IsEmpty);

        /// <summary>
        /// Is single line.
        /// </summary>
        public bool IsSingleLine(BuildingContext context) => _core.Count <= 1 && !_core.Any(e => !e.IsSingleLine(context));

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Text.</returns>
        public string ToString(BuildingContext context)
        {
            var next = context.ChangeIndent(context.Indent + Indent);
            return string.Join(Separator + Environment.NewLine, _core.Select(e => e.ToString(next).TrimEnd()).ToArray());
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="indent">Indent.</param>
        public void Add(string text, int indent)
        {
            if (string.IsNullOrEmpty(text.Trim())) return;
            _core.Add(new SingleTextCode(text, indent));
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void Add(ICode text)
        {
            if (text.IsEmpty) return;
            _core.Add(text);
        }

        /// <summary>
        /// Add texts.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <param name="texts">Texts.</param>
        public void AddRange(int indent, IEnumerable<ICode> texts)
            => _core.AddRange(texts.Where(e => !e.IsEmpty).Select(e => new HCode(e) { Indent = 1 }).Cast<ICode>());

        /// <summary>
        /// Add texts.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <param name="texts">Texts.</param>
        public void AddRange(int indent, params ICode[] texts)
            => _core.AddRange(texts.Where(e => !e.IsEmpty).Select(e => new HCode(e) { Indent = 1 }).Cast<ICode>());

        /// <summary>
        /// Accept customizer.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;

            var dstCore = _core.Select(e => e.Accept(customizer));
            return CopyProperty(dstCore.ToArray());
        }

        VCode CopyProperty(params ICode[] texts)
             => new VCode(texts) { Indent = Indent, Separator = Separator };
    }
}
