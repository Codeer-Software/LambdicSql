using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.BuilderServices.CodeParts
{
    /// <summary>
    /// Horizontal text.
    /// </summary>
    public class HCode : ICode
    {
        List<ICode> _texts = new List<ICode>();

        /// <summary>
        /// Separator.
        /// </summary>
        public string Separator { get; set; } = string.Empty;

        /// <summary>
        /// Indent
        /// </summary>
        public int Indent { get; set; }

        //TODO これがfalseなケースってなに？
        /// <summary>
        /// Is functional.
        /// </summary>
        public bool IsFunctional { get; set; }

        /// <summary>
        /// Enable change line.
        /// </summary>
        public bool EnableChangeLine { get; set; } = true;

        /// <summary>
        /// Is single line.
        /// </summary>
        public bool IsSingleLine(BuildingContext context) => !_texts.Any(e => !e.IsSingleLine(context));

        /// <summary>
        /// Is empty.
        /// </summary>
        public bool IsEmpty => !_texts.Any(e => !e.IsEmpty);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Horizontal texts.</param>
        public HCode(params ICode[] texts)
        {
            _texts.AddRange(texts.Where(e => !e.IsEmpty));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Horizontal texts.</param>
        public HCode(IEnumerable<ICode> texts)
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
        {
            indent += Indent;
            if (_texts.Count == 0) return string.Empty;
            if (_texts.Count == 1) return _texts[0].ToString(isTopLevel, indent, context);

            if (IsSingleLine(context) || !EnableChangeLine)
            {
                return _texts[0].ToString(isTopLevel, indent, context) + Separator
                    + string.Join(Separator, _texts.Skip(1).Select(e => e.ToString(isTopLevel, 0, context)).ToArray());
            }

            //if IsFunctional is true, add Indent other than the first line.
            var addIndentCount = IsFunctional ? 1 : 0;
            var sep = Separator.TrimEnd();
            return _texts[0].ToString(isTopLevel, indent, context) + sep + Environment.NewLine +
                string.Join(sep + Environment.NewLine, _texts.Skip(1).Select(e => e.ToString(isTopLevel, indent + addIndentCount, context).TrimEnd()).ToArray());
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
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(IEnumerable<ICode> texts)
            => _texts.AddRange(texts.Where(e => !e.IsEmpty));

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(params ICode[] texts)
            => _texts.AddRange(texts.Where(e => !e.IsEmpty));

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

        HCode CopyProperty(params ICode[] texts)
             => new HCode(texts) { Indent = Indent, IsFunctional = IsFunctional, EnableChangeLine = EnableChangeLine, Separator = Separator };
    }
}
