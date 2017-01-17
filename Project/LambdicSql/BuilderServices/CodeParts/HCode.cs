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

        /// <summary>
        /// Add indent on line feed.
        /// </summary>
        public bool AddIndentNewLine { get; set; }

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
        /// <param name="context">Context.</param>
        /// <returns>Text.</returns>
        public string ToString(BuildingContext context)
        {
            var firstLineContext = context.ChangeIndent(context.Indent + Indent);

            if (_texts.Count == 0) return string.Empty;
            if (_texts.Count == 1) return _texts[0].ToString(firstLineContext);

            if (IsSingleLine(context) || !EnableChangeLine)
            {
                var nonIndent = context.ChangeIndent(0);
                return _texts[0].ToString(firstLineContext) + Separator
                    + string.Join(Separator, _texts.Skip(1).Select(e => e.ToString(nonIndent)).ToArray());
            }

            //if IsFunctional is true, add Indent other than the first line.
            var nextLineContext = AddIndentNewLine ? firstLineContext.ChangeIndent(firstLineContext.Indent + 1): firstLineContext;
            var sep = Separator.TrimEnd();
            return _texts[0].ToString(firstLineContext) + sep + Environment.NewLine +
                string.Join(sep + Environment.NewLine, _texts.Skip(1).Select(e => e.ToString(nextLineContext).TrimEnd()).ToArray());
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
             => new HCode(texts) { Indent = Indent, AddIndentNewLine = AddIndentNewLine, EnableChangeLine = EnableChangeLine, Separator = Separator };
    }
}
