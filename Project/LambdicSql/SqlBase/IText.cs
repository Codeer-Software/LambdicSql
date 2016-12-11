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

        /// <summary>
        /// Is single line.
        /// </summary>
        public bool IsSingleLine => !_texts.Any(e => !e.IsSingleLine);

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
        /// To string.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public string ToString(int indent)
        {
            if (_texts.Count == 0) return string.Empty;
            if (_texts.Count == 1) return _texts[0].ToString(indent);

            if (IsSingleLine)
            {
                return _texts[0].ToString(indent) + string.Join(string.Empty, _texts.Skip(1).Select(e => e.ToString(0)).ToArray());
            }
            return _texts[0].ToString(indent) + Environment.NewLine + string.Join(Environment.NewLine, _texts.Skip(1).Select(e => e.ToString(indent + 1)).ToArray());
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
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public IText ConcatAround(string front, string back)
        {
            if (_texts.Count == 0) return new HorizontalText(new SingleText(front + back));
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new HorizontalText(dst);
        }

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public IText ConcatToFront(string front)
        {
            if (_texts.Count == 0) return new HorizontalText(new SingleText(front));
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            return new HorizontalText(dst);
        }

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public IText ConcatToBack(string back)
        {
            if (_texts.Count == 0) return new HorizontalText(new SingleText(back));
            var dst = _texts.ToArray();
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new HorizontalText(dst);
        }
    }

    /// <summary>
    /// Vertical text.
    /// </summary>
    public class VerticalText : IText
    {
        List<IText> _texts = new List<IText>();

        /// <summary>
        /// Is single line.
        /// </summary>
        public bool IsSingleLine => false;

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
        /// To string.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public string ToString(int indent)
            => string.Join(Environment.NewLine, _texts.Select(e => e.ToString(indent)).ToArray());

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
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public IText ConcatAround(string front, string back)
        {
            if (_texts.Count == 0) return new VerticalText(new SingleText(front + back));
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new VerticalText(dst);
        }

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public IText ConcatToFront(string front)
        {
            if (_texts.Count == 0) return new VerticalText(new SingleText(front));
            var dst = _texts.ToArray();
            dst[0] = dst[0].ConcatToFront(front);
            return new VerticalText(dst);
        }

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public IText ConcatToBack(string back)
        {
            if (_texts.Count == 0) return new VerticalText(new SingleText(back));
            var dst = _texts.ToArray();
            dst[dst.Length - 1] = dst[dst.Length - 1].ConcatToBack(back);
            return new VerticalText(dst);
        }
    }
}
