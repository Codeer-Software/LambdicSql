using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.SqlBuilder.Parts
{
    /// <summary>
    /// Horizontal text.
    /// </summary>
    public class HBuildingParts : BuildingParts
    {
        List<BuildingParts> _texts = new List<BuildingParts>();

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
        public override bool IsSingleLine(SqlBuildingContext context) => !_texts.Any(e => !e.IsSingleLine(context));

        /// <summary>
        /// Is empty.
        /// </summary>
        public override bool IsEmpty => !_texts.Any(e => !e.IsEmpty);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Horizontal texts.</param>
        public HBuildingParts(params BuildingParts[] texts)
        {
            _texts.AddRange(texts.Where(e => !e.IsEmpty));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Horizontal texts.</param>
        public HBuildingParts(IEnumerable<BuildingParts> texts)
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
        public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
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
                string.Join(sep + Environment.NewLine, _texts.Skip(1).Select(e => e.ToString(isTopLevel, indent + addIndentCount, context)).ToArray());
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="indent">Indent.</param>
        public void Add(string text, int indent)
        {
            if (string.IsNullOrEmpty(text.Trim())) return;
            _texts.Add(new SingleTextBuildingParts(text, indent));
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void Add(BuildingParts text)
        {
            if (text.IsEmpty) return;
            _texts.Add(text);
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(IEnumerable<BuildingParts> texts)
            => _texts.AddRange(texts.Where(e => !e.IsEmpty));

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(params BuildingParts[] texts)
            => _texts.AddRange(texts.Where(e => !e.IsEmpty));

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override BuildingParts ConcatAround(string front, string back)
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
        public override BuildingParts ConcatToFront(string front)
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
        public override BuildingParts ConcatToBack(string back)
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
        public override BuildingParts Customize(ISqlTextCustomizer customizer)
        {
            var dst = _texts.Select(e => customizer.Custom(e));
            return CopyProperty(dst.ToArray());
        }

        HBuildingParts CopyProperty(params BuildingParts[] texts)
             => new HBuildingParts(texts) { Indent = Indent, IsFunctional = IsFunctional, EnableChangeLine = EnableChangeLine, Separator = Separator };
    }
}
