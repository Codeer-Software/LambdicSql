using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.BuilderServices.CodeParts
{
    /// <summary>
    /// Arrange the code horizontally.
    /// </summary>
    public class HCode : ICode
    {
        List<ICode> _core = new List<ICode>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Horizontal texts.</param>
        public HCode(params ICode[] texts)
        {
            _core.AddRange(texts.Where(e => !e.IsEmpty));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Horizontal texts.</param>
        public HCode(IEnumerable<ICode> texts)
        {
            _core.AddRange(texts.Where(e => !e.IsEmpty));
        }

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
        /// Is empty.
        /// </summary>
        public bool IsEmpty => !_core.Any(e => !e.IsEmpty);

        /// <summary>
        /// Is single line.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Is single line.</returns>
        public bool IsSingleLine(BuildingContext context) => !_core.Any(e => !e.IsSingleLine(context));

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Text.</returns>
        public string ToString(BuildingContext context)
        {
            var firstLineContext = context.ChangeIndent(context.Indent + Indent);

            if (_core.Count == 0) return string.Empty;
            if (_core.Count == 1) return _core[0].ToString(firstLineContext);

            if (IsSingleLine(context) || !EnableChangeLine)
            {
                var nonIndent = context.ChangeIndent(0);
                return _core[0].ToString(firstLineContext) + Separator
                    + string.Join(Separator, _core.Skip(1).Select(e => e.ToString(nonIndent)).ToArray());
            }

            //if AddIndentNewLine is true, add Indent other than the first line.
            var nextLineContext = AddIndentNewLine ? firstLineContext.ChangeIndent(firstLineContext.Indent + 1): firstLineContext;

            var sep = Separator.TrimEnd();
            return _core[0].ToString(firstLineContext) + sep + Environment.NewLine +
                string.Join(sep + Environment.NewLine, _core.Skip(1).Select(e => e.ToString(nextLineContext).TrimEnd()).ToArray());
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
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(IEnumerable<ICode> texts)
            => _core.AddRange(texts.Where(e => !e.IsEmpty));

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(params ICode[] texts)
            => _core.AddRange(texts.Where(e => !e.IsEmpty));

        /// <summary>
        /// Accept customizer.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Destination.</returns>
        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;

            var dstCore = _core.Select(e => e.Accept(customizer));
            return CopyProperty(dstCore.ToArray());
        }

        HCode CopyProperty(params ICode[] texts)
             => new HCode(texts) { Indent = Indent, AddIndentNewLine = AddIndentNewLine, EnableChangeLine = EnableChangeLine, Separator = Separator };
    }
}
