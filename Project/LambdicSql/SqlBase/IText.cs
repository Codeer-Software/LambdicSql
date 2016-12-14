﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Text.
    /// </summary>
    public abstract class IText
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
        public abstract IText ConcatAround(string front, string back);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public abstract IText ConcatToFront(string front);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public abstract IText ConcatToBack(string back);

        /// <summary>
        /// Convert string to IText.
        /// </summary>
        /// <param name="text">string.</param>
        public static implicit operator IText (string text) => new SingleText(text);
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
        public override IText ConcatAround(string front, string back) => new SingleText(front + _text + back, _indent);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override IText ConcatToFront(string front) => new SingleText(front + _text, _indent);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override IText ConcatToBack(string back) => new SingleText(_text + back, _indent);
    }

    /// <summary>
    /// Horizontal text.
    /// </summary>
    public class HorizontalText : IText
    {
        List<IText> _texts = new List<IText>();

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
        public HorizontalText() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texts">Horizontal texts.</param>
        public HorizontalText(params IText[] texts)
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
        public void Add(IText text)
        {
            if (text.IsEmpty) return;
            _texts.Add(text);
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(IEnumerable<IText> texts)
            => _texts.AddRange(texts.Where(e=>!e.IsEmpty));

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override IText ConcatAround(string front, string back)
        {
            if (_texts.Count == 0) return new HorizontalText(new SingleText(front + back)) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange, Separator = Separator };
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new HorizontalText(dst) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange, Separator = Separator };
        }

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override IText ConcatToFront(string front)
        {
            if (_texts.Count == 0) return new HorizontalText(new SingleText(front)) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange, Separator = Separator };
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            return new HorizontalText(dst) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange, Separator = Separator };
        }

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override IText ConcatToBack(string back)
        {
            if (_texts.Count == 0) return new HorizontalText(new SingleText(back)) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange, Separator = Separator };
            var dst = _texts.ToArray();
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new HorizontalText(dst) { Indent = Indent, IsFunctional = IsFunctional, IsNotLineChange = IsNotLineChange, Separator = Separator };
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
            if (r.IsEmpty) return l;
            var dst = new HorizontalText() { Indent = l.Indent, IsFunctional = l.IsFunctional, IsNotLineChange = l.IsNotLineChange, Separator = l.Separator };
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
            if (string.IsNullOrEmpty(r.Trim())) return l;
            var dst = new HorizontalText() { Indent = l.Indent, IsFunctional = l.IsFunctional, IsNotLineChange = l.IsNotLineChange, Separator = l.Separator };
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
        public override bool IsSingleLine => _texts.Count <= 1 && !_texts.Any(e => !e.IsSingleLine);

        /// <summary>
        /// Is empty.
        /// </summary>
        public override bool IsEmpty => !_texts.Any(e => !e.IsEmpty);

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
            _texts.AddRange(texts.Where(e=>!e.IsEmpty));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="separator">Separator.</param>
        /// <param name="texts">Vertical texts.</param>
        public VerticalText(string separator, params IText[] texts)
        {
            _separator = separator;
            _texts.AddRange(texts.Where(e => !e.IsEmpty));
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
            return _separator;
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
        public void Add(IText text)
        {
            if (text.IsEmpty) return;
            _texts.Add(text);
        }

        /// <summary>
        /// Add texts.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <param name="texts">Texts.</param>
        public void AddRange(int indent, IEnumerable<IText> texts)
        {
            foreach (var e in texts.Where(e=>!e.IsEmpty))
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
        public override IText ConcatAround(string front, string back)
        {
            if (_texts.Count == 0) return new VerticalText(_separator, new SingleText(front + back)) { Indent = Indent };
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new VerticalText(_separator, dst) { Indent = Indent };
        }

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override IText ConcatToFront(string front)
        {
            if (_texts.Count == 0) return new VerticalText(_separator, new SingleText(front)) { Indent = Indent };
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            return new VerticalText(_separator, dst) { Indent = Indent };
        }

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override IText ConcatToBack(string back)
        {
            if (_texts.Count == 0) return new VerticalText(_separator, new SingleText(back)) { Indent = Indent };
            var dst = _texts.ToArray();
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new VerticalText(_separator, dst) { Indent = Indent };
        }
    }
}
