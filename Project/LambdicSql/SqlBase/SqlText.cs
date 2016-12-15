using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Text.
    /// </summary>
    public abstract class SqlText
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
        /// <param name="isTopLevel">Is top level.</param>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public abstract string ToString(bool isTopLevel, int indent);

        //TODO ないわー
        //-------------------------------------------------------------------
        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public abstract SqlText ConcatAround(string front, string back);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public abstract SqlText ConcatToFront(string front);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public abstract SqlText ConcatToBack(string back);
        //-------------------------------------------------------------------

        /// <summary>
        /// Convert string to IText.
        /// </summary>
        /// <param name="text">string.</param>
        public static implicit operator SqlText (string text) => new SingleText(text);
    }

    /// <summary>
    /// Wrapper.
    /// </summary>
    public abstract class TextWrapper : SqlText
    {
        /// <summary>
        /// 
        /// </summary>
        protected SqlText Core { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="core">Core.</param>
        public TextWrapper(SqlText core)
        {
            Core = core;
        }

        /// <summary>
        /// Is single line.
        /// </summary>
        public override bool IsSingleLine => Core.IsSingleLine;

        /// <summary>
        /// Is empty.
        /// </summary>
        public override bool IsEmpty => Core.IsEmpty;

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="isTopLevel">Is top level.</param>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public override string ToString(bool isTopLevel, int indent) => Core.ToString(isTopLevel, indent);
    }

    internal class SelectClauseText : TextWrapper
    {
        internal SelectClauseText(SqlText core) : base(core) { }

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override SqlText ConcatAround(string front, string back) => new SelectClauseText(Core.ConcatAround(front, back));

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override SqlText ConcatToFront(string front) => new SelectClauseText(Core.ConcatToFront(front));

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override SqlText ConcatToBack(string back) => new SelectClauseText(Core.ConcatToBack(back));
    }

    internal class SelectQueryText : TextWrapper
    {
        internal SelectQueryText(SqlText core) : base(core) { }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="isTopLevel">Is top level.</param>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public override string ToString(bool isTopLevel, int indent)
        {
            if (isTopLevel) return base.ToString(false, indent);
            return Core.ConcatAround("(", ")").ToString(false, indent);
        }

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override SqlText ConcatAround(string front, string back) => new SelectQueryText(Core.ConcatAround(front, back));

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override SqlText ConcatToFront(string front) => new SelectQueryText(Core.ConcatToFront(front));

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override SqlText ConcatToBack(string back) => new SelectQueryText(Core.ConcatToBack(back));
    }

    internal class QueryText : TextWrapper
    {
        internal QueryText(SqlText core) : base(core) { }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="isTopLevel">Is top level.</param>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public override string ToString(bool isTopLevel, int indent)=> base.ToString(false, indent);

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override SqlText ConcatAround(string front, string back) => new QueryText(Core.ConcatAround(front, back));

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override SqlText ConcatToFront(string front) => new QueryText(Core.ConcatToFront(front));

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override SqlText ConcatToBack(string back) => new QueryText(Core.ConcatToBack(back));
    }
}
