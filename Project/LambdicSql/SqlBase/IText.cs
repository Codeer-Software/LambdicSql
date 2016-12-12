using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Text.
    /// </summary>
    public interface IText
    {
        /// <summary>
        /// Is single line.
        /// </summary>
        bool IsSingleLine { get; }

        /// <summary>
        /// Is empty.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        string ToString(int indent);

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        IText ConcatAround(string front, string back);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        IText ConcatToFront(string front);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        IText ConcatToBack(string back);
    }

    /// <summary>
    /// Single text.
    /// </summary>
    public class SingleText : IText
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
        public bool IsSingleLine => true;

        /// <summary>
        /// Is empty.
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(_text);

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public string ToString(int indent)
            => string.Join(string.Empty, Enumerable.Range(0, _indent + indent).Select(e => "\t").ToArray()) + _text;

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public IText ConcatAround(string front, string back) => new SingleText(front + _text + back, _indent);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public IText ConcatToFront(string front) => new SingleText(front + _text, _indent);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public IText ConcatToBack(string back) => new SingleText(_text + back, _indent);
    }

    /// <summary>
    /// Horizontal text.
    /// </summary>
    public class HorizontalText : IText
    {
        List<IText> _texts = new List<IText>();
        string _separator = string.Empty;

        /// <summary>
        /// Indent
        /// </summary>
        public int Indent { get; set; }

        /// <summary>
        /// Is single line.
        /// </summary>
        public bool IsSingleLine => !_texts.Any(e => !e.IsSingleLine);

        /// <summary>
        /// Is empty.
        /// </summary>
        public bool IsEmpty => !_texts.Any(e => !e.IsEmpty);

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
        public HorizontalText() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Horizontal texts.</param>
        public HorizontalText(params IText[] texts)
        {
            _texts.AddRange(texts);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="separator">Separator.</param>
        /// <param name="texts">Horizontal texts.</param>
        public HorizontalText(string separator, params IText[] texts)
        {
            _separator = separator;
            _texts.AddRange(texts);
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public string ToString(int indent)
        {
            indent += Indent;
            if (_texts.Count == 0) return string.Empty;
            if (_texts.Count == 1) return _texts[0].ToString(indent);

            if (IsSingleLine || IsNotLineChange)
            {
                var sep = 1 < _texts.Count ? _separator : string.Empty;
                return _texts[0].ToString(indent) + sep + string.Join(_separator, _texts.Skip(1).Select(e => e.ToString(0)).ToArray());
            }
            //TODO あー指定があれば、ここもセパレータ入れた方がいいな
            var addIndentCount = IsFunctional ? 1 : 0;
            return _texts[0].ToString(indent) + Environment.NewLine + string.Join(Environment.NewLine, _texts.Skip(1).Select(e => e.ToString(indent + addIndentCount)).ToArray());
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void Add(string text) => _texts.Add(new SingleText(text));

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="indent">Indent.</param>
        public void Add(string text, int indent) => _texts.Add(new SingleText(text, indent));

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void Add(IText text) => _texts.Add(text);

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(IEnumerable<IText> texts)
            => _texts.AddRange(texts);

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public IText ConcatAround(string front, string back)
        {
            if (_texts.Count == 0) return new HorizontalText(_separator, new SingleText(front + back)) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange };
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new HorizontalText(_separator, dst);
        }

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public IText ConcatToFront(string front)
        {
            if (_texts.Count == 0) return new HorizontalText(_separator, new SingleText(front)) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange };
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            return new HorizontalText(_separator, dst);
        }

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public IText ConcatToBack(string back)
        {
            if (_texts.Count == 0) return new HorizontalText(_separator, new SingleText(back)) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange };
            var dst = _texts.ToArray();
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new HorizontalText(_separator, dst);
        }

        //あー、この結合で内部的な事情をガッツリしったらなんとかなるな・・・
        //ていうかHorizontalどうしの結合とかよくない事起こるよね

        /// <summary>
        /// + operator.
        /// </summary>
        /// <param name="l">left value.</param>
        /// <param name="r">right value.</param>
        /// <returns>result value.</returns>
        public static HorizontalText operator +(HorizontalText l, IText r)
        {
            var dst = new HorizontalText(l._separator) { Indent = l.Indent, IsFunctional = l.IsFunctional, IsNotLineChange = l.IsNotLineChange };
            dst._texts.AddRange(l._texts);

            /*
            var h = r as HorizontalText;
            if (h != null)
            {
                dst._texts.AddRange(h._texts);
            }
            else*/
            {
                dst._texts.Add(r);
            }
            return dst;
        }

        /// <summary>
        /// + operator.
        /// </summary>
        /// <param name="l">left value.</param>
        /// <param name="r">right value.</param>
        /// <returns>result value.</returns>
        public static HorizontalText operator +(HorizontalText l, string r)
        {
            var dst = new HorizontalText(l._separator) { Indent = l.Indent, IsFunctional = l.IsFunctional, IsNotLineChange = l.IsNotLineChange };
            dst._texts.AddRange(l._texts);
            dst._texts.Add(new SingleText(r));
            return dst;
        }
    }

    /// <summary>
    /// Vertical text.
    /// </summary>
    public class VerticalText : IText
    {
        List<IText> _texts = new List<IText>();
        string _separator = string.Empty;

        /// <summary>
        /// Indent
        /// </summary>
        public int Indent { get; set; }

        /// <summary>
        /// Is single line.
        /// </summary>
        public bool IsSingleLine => _texts.Count <= 1 && !_texts.Any(e => !e.IsSingleLine);

        /// <summary>
        /// Is empty.
        /// </summary>
        public bool IsEmpty => !_texts.Any(e => !e.IsEmpty);

        /// <summary>
        /// Constructor.
        /// </summary>
        public VerticalText() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Vertical texts.</param>
        public VerticalText(params IText[] texts)
        {
            _texts.AddRange(texts);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="separator">Separator.</param>
        /// <param name="texts">Vertical texts.</param>
        public VerticalText(string separator, params IText[] texts)
        {
            _separator = separator;
            _texts.AddRange(texts);
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public string ToString(int indent)
            => string.Join(Environment.NewLine, _texts.Select((e, i) => e.ToString(Indent + indent) + GetSeparator(i)).ToArray());

        string GetSeparator(int i)
        {
            if (_texts.Count == 1 || i == _texts.Count - 1) return string.Empty;
            return _separator;
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void Add(string text) => _texts.Add(new SingleText(text));

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="indent">Indent.</param>
        public void Add(string text, int indent) => _texts.Add(new SingleText(text, indent));

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void Add(IText text) => _texts.Add(text);

        /// <summary>
        /// Add texts.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <param name="texts">Texts.</param>
        public void AddRange(int indent, IEnumerable<IText> texts)
        {
            foreach (var e in texts)
            {
                _texts.Add(new HorizontalText(e) { Indent = 1 });
            }
        }

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public IText ConcatAround(string front, string back)
        {
            if (_texts.Count == 0) return new VerticalText(_separator, new SingleText(front + back));
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new VerticalText(_separator, dst);
        }

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public IText ConcatToFront(string front)
        {
            if (_texts.Count == 0) return new VerticalText(_separator, new SingleText(front));
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            return new VerticalText(_separator, dst);
        }

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public IText ConcatToBack(string back)
        {
            if (_texts.Count == 0) return new VerticalText(_separator, new SingleText(back));
            var dst = _texts.ToArray();
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new VerticalText(_separator, dst);
        }
    }
}
