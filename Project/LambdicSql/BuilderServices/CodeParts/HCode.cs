using System;
using System.Collections.Generic;

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
            AddRange(texts);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Horizontal texts.</param>
        public HCode(IEnumerable<ICode> texts)
        {
            AddRange(texts);
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
        public bool IsEmpty => _core.Count == 0;

        /// <summary>
        /// Is single line.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Is single line.</returns>
        public bool IsSingleLine(BuildingContext context)
        {
            foreach (var e in _core)
            {
                if (!e.IsSingleLine(context)) return false;
            }
            return true;
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
        {
            foreach (var e in texts)
            {
                if (!e.IsEmpty)
                {
                    _core.Add(e);
                }
            }
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(params ICode[] texts)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                if (!texts[i].IsEmpty)
                {
                    _core.Add(texts[i]);
                }
            }
        }

        /// <summary>
        /// Accept customizer.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Destination.</returns>
        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;

            var hDst = new HCode { Indent = Indent, AddIndentNewLine = AddIndentNewLine, EnableChangeLine = EnableChangeLine, Separator = Separator };
            foreach (var e in _core)
            {
                hDst._core.Add(e.Accept(customizer));
            }
            return hDst;
        }

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
                return BuildSingleLine(context, firstLineContext);
            }
            return BuildMultiLine(firstLineContext);
        }

        string BuildMultiLine(BuildingContext firstLineContext)
        {
            //if AddIndentNewLine is true, add Indent other than the first line.
            var nextLineContext = AddIndentNewLine ? firstLineContext.ChangeIndent(firstLineContext.Indent + 1) : firstLineContext;

            var texts = new string[_core.Count];
            texts[0] = _core[0].ToString(firstLineContext);
            for (int i = 1; i < _core.Count; i++)
            {
                texts[i] = _core[i].ToString(nextLineContext).TrimEnd();
            }

            var sep = Separator.TrimEnd();
            return string.Join(sep + Environment.NewLine, texts);
        }

        string BuildSingleLine(BuildingContext context, BuildingContext firstLineContext)
        {
            var texts = new string[_core.Count];
            texts[0] = _core[0].ToString(firstLineContext);
            var nonIndent = context.ChangeIndent(0);
            for (int i = 1; i < _core.Count; i++)
            {
                texts[i] = _core[i].ToString(nonIndent);
            }
            return string.Join(Separator, texts);
        }
    }
}
