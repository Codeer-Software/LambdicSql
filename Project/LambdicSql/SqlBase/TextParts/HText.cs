using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.SqlBase.TextParts
{
    /// <summary>
    /// Horizontal text.
    /// </summary>
    public class HText : SqlText
    {
        List<SqlText> _texts = new List<SqlText>();

        /// <summary>
        /// Separator.
        /// </summary>
        public string Separator { get; set; } = string.Empty;

        /// <summary>
        /// Indent
        /// </summary>
        public int Indent { get; set; }

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
        public override bool IsSingleLine => !_texts.Any(e => !e.IsSingleLine);

        /// <summary>
        /// Is empty.
        /// </summary>
        public override bool IsEmpty => !_texts.Any(e => !e.IsEmpty);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Horizontal texts.</param>
        public HText(params SqlText[] texts)
        {
            _texts.AddRange(texts.Where(e => !e.IsEmpty));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Horizontal texts.</param>
        public HText(IEnumerable<SqlText> texts)
        {
            _texts.AddRange(texts.Where(e => !e.IsEmpty));
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="isTopLevel">Is top level.</param>
        /// <param name="indent">Indent.</param>
        /// <param name="option">Option.</param>
        /// <param name="paramterInfo">ParamterInfo.</param>
        /// <returns>Text.</returns>
        public override string ToString(bool isTopLevel, int indent, SqlConvertOption option, ParameterInfo paramterInfo)
        {
            indent += Indent;
            if (_texts.Count == 0) return string.Empty;
            if (_texts.Count == 1) return _texts[0].ToString(isTopLevel, indent, option, paramterInfo);

            if (IsSingleLine || !EnableChangeLine)
            {
                return _texts[0].ToString(isTopLevel, indent, option, paramterInfo) + Separator
                    + string.Join(Separator, _texts.Skip(1).Select(e => e.ToString(isTopLevel, 0, option, paramterInfo)).ToArray());
            }

            //if IsFunctional is true, add Indent other than the first line.
            var addIndentCount = IsFunctional ? 1 : 0;
            var sep = Separator.TrimEnd();
            return _texts[0].ToString(isTopLevel, indent, option, paramterInfo) + sep + Environment.NewLine +
                string.Join(sep + Environment.NewLine, _texts.Skip(1).Select(e => e.ToString(isTopLevel, indent + addIndentCount, option, paramterInfo)).ToArray());
        }

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
        public void Add(SqlText text)
        {
            if (text.IsEmpty) return;
            _texts.Add(text);
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(IEnumerable<SqlText> texts)
            => _texts.AddRange(texts.Where(e => !e.IsEmpty));

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(params SqlText[] texts)
            => _texts.AddRange(texts.Where(e => !e.IsEmpty));

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override SqlText ConcatAround(string front, string back)
        {
            if (_texts.Count == 0) return CopyProperty(front + back);

            var newTexts = _texts.ToArray();
            newTexts[0] = newTexts[0].ConcatToFront(front);
            newTexts[newTexts.Length - 1] = newTexts[newTexts.Length - 1].ConcatToBack(back);
            return CopyProperty(newTexts);
        }
        
        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override SqlText ConcatToFront(string front)
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
        /// <returns></returns>
        public override SqlText ConcatToBack(string back)
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
        public override SqlText Customize(ISqlTextCustomizer customizer)
        {
            var dst = _texts.Select(e => customizer.Custom(e));
            return CopyProperty(dst.ToArray());
        }

        HText CopyProperty(params SqlText[] texts)
             => new HText(texts) { Indent = Indent, IsFunctional = IsFunctional, EnableChangeLine = EnableChangeLine, Separator = Separator };
    }
}
