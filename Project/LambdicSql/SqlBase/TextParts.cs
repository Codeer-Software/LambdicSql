using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.SqlBase
{
    //TODO トークンとかそれ系の名前の方がいいな
    //そもそもSQLから離れてよくね？

    /// <summary>
    /// Text.
    /// </summary>
    public abstract class TextParts
    {
        /// <summary>
        /// Is single line.
        /// </summary>
        public abstract bool IsSingleLine { get; }

        /// <summary>
        /// Is empty.
        /// </summary>
        public abstract bool IsEmpty { get; }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public abstract string ToString(int indent);

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public abstract TextParts ConcatAround(string front, string back);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public abstract TextParts ConcatToFront(string front);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public abstract TextParts ConcatToBack(string back);

        /// <summary>
        /// Convert string to IText.
        /// </summary>
        /// <param name="text">string.</param>
        public static implicit operator TextParts (string text) => new SingleText(text);
    }


    //TODO これは隠しクラス的にしてnewさせない

    /// <summary>
    /// Single text.
    /// </summary>
    public class SingleText : TextParts
    {
        string _text;
        int _indent;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Text.</param>
        public SingleText(string text)
        {
            _text = text;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="indent">Indent.</param>
        public SingleText(string text, int indent)
        {
            _text = text;
            _indent = indent;
        }

        /// <summary>
        /// Is single line.
        /// </summary>
        public override bool IsSingleLine => true;

        /// <summary>
        /// Is empty.
        /// </summary>
        public override bool IsEmpty => string.IsNullOrEmpty(_text.Trim());

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public override string ToString(int indent)
            => string.Join(string.Empty, Enumerable.Range(0, _indent + indent).Select(e => "\t").ToArray()) + _text;

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override TextParts ConcatAround(string front, string back) => new SingleText(front + _text + back, _indent);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override TextParts ConcatToFront(string front) => new SingleText(front + _text, _indent);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override TextParts ConcatToBack(string back) => new SingleText(_text + back, _indent);
    }

    /// <summary>
    /// Horizontal text.
    /// </summary>
    public class HText : TextParts
    {
        List<TextParts> _texts = new List<TextParts>();

        /// <summary>
        /// Separator.
        /// </summary>
        public string Separator { get; set; } = string.Empty;

        /// <summary>
        /// Indent
        /// </summary>
        public int Indent { get; set; }

        /// <summary>
        /// Is single line.
        /// </summary>
        public override bool IsSingleLine => !_texts.Any(e => !e.IsSingleLine);

        /// <summary>
        /// Is empty.
        /// </summary>
        public override bool IsEmpty => !_texts.Any(e => !e.IsEmpty);

        //内部的にはこれでいいけど
        //protectedにして
        //継承クラスだけがこれをonにできる方がいいか
        //でその名前がfunctionalText
        /// <summary>
        /// Is functional.
        /// </summary>
        public bool IsFunctional { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        public bool IsNotLineChange { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public HText() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Horizontal texts.</param>
        public HText(params TextParts[] texts)
        {
            _texts.AddRange(texts.Where(e=>!e.IsEmpty));
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public override string ToString(int indent)
        {
            indent += Indent;
            if (_texts.Count == 0) return string.Empty;
            if (_texts.Count == 1) return _texts[0].ToString(indent);

            if (IsSingleLine || IsNotLineChange)
            {
                var sep = 1 < _texts.Count ? Separator : string.Empty;
                return _texts[0].ToString(indent) + sep + string.Join(Separator, _texts.Skip(1).Select(e => e.ToString(0)).ToArray());
            }
            //TODO あー指定があれば、ここもセパレータ入れた方がいいな
            var addIndentCount = IsFunctional ? 1 : 0;
            return _texts[0].ToString(indent) + Environment.NewLine + string.Join(Environment.NewLine, _texts.Skip(1).Select(e => e.ToString(indent + addIndentCount)).ToArray());
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void Add(string text)
        {
            if (string.IsNullOrEmpty(text.Trim())) return;
            _texts.Add(new SingleText(text));
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
        public void Add(TextParts text)
        {
            if (text.IsEmpty) return;
            _texts.Add(text);
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(IEnumerable<TextParts> texts)
            => _texts.AddRange(texts.Where(e => !e.IsEmpty));

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(params TextParts[] texts)
            => _texts.AddRange(texts.Where(e => !e.IsEmpty));

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override TextParts ConcatAround(string front, string back)
        {
            if (_texts.Count == 0) return new HText(new SingleText(front + back)) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange, Separator = Separator };
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new HText(dst) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange, Separator = Separator };
        }

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override TextParts ConcatToFront(string front)
        {
            if (_texts.Count == 0) return new HText(new SingleText(front)) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange, Separator = Separator };
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            return new HText(dst) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange, Separator = Separator };
        }

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override TextParts ConcatToBack(string back)
        {
            if (_texts.Count == 0) return new HText(new SingleText(back)) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange, Separator = Separator };
            var dst = _texts.ToArray();
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new HText(dst) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange, Separator = Separator };
        }
    }

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
        public VText() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Vertical texts.</param>
        public VText(params TextParts[] texts)
        {
            _texts.AddRange(texts.Where(e=>!e.IsEmpty));
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public override string ToString(int indent)
            => string.Join(Environment.NewLine, _texts.Select((e, i) => e.ToString(Indent + indent) + GetSeparator(i)).ToArray());

        string GetSeparator(int i)
        {
            if (_texts.Count == 1 || i == _texts.Count - 1) return string.Empty;
            return Separator;
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void Add(string text)
        {
            if (string.IsNullOrEmpty(text.Trim())) return;
            _texts.Add(new SingleText(text));
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
        {
            foreach (var e in texts.Where(e=>!e.IsEmpty))
            {
                _texts.Add(new HText(e) { Indent = 1 });
            }
        }

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override TextParts ConcatAround(string front, string back)
        {
            if (_texts.Count == 0) return new VText(new SingleText(front + back)) { Indent = Indent, Separator = Separator };
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new VText(dst) { Indent = Indent };
        }

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override TextParts ConcatToFront(string front)
        {
            if (_texts.Count == 0) return new VText(new SingleText(front)) { Indent = Indent, Separator = Separator };
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            return new VText(dst) { Indent = Indent, Separator = Separator };
        }

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override TextParts ConcatToBack(string back)
        {
            if (_texts.Count == 0) return new VText(new SingleText(back)) { Indent = Indent, Separator = Separator };
            var dst = _texts.ToArray();
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new VText(dst) { Indent = Indent, Separator = Separator };
        }
    }
}
