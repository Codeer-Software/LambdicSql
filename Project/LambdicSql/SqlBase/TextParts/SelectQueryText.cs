namespace LambdicSql.SqlBase.TextParts
{
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
}
